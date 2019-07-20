using Pixburb.CommonModel;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Implementation
{
    public class AdminLoginDataReader : IAdminLoginDataReader
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<ConnectionString> ValidateOrganization(string orgID)
        {
            OperationOutcome outcome = new OperationOutcome();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "OrgValidation";

            cmd.Parameters.Add(new SqlParameter("@OrgId", orgID));
            var success = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var message = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var reader = cmd.ExecuteReader();

            ConnectionString connString = new ConnectionString();
                while(reader.Read())
                {
                    connString.DataSource = Convert.ToString(reader["DataSource"]);
                    connString.UserId = Convert.ToString(reader["UserId"]);
                    connString.Password = Convert.ToString(reader["Password"]);
                    connString.DataBaseName = Convert.ToString(reader["DataBaseName"]);
                }
            conn.Close();
            return Task.FromResult(connString);
        }

        public Task<OperationOutcome> ValidateAdmin(string username, string password, string ConnectionString)
        {
            OperationOutcome outcome = new OperationOutcome();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UserValidation";

            cmd.Parameters.Add(new SqlParameter("@UserName", username));
            cmd.Parameters.Add(new SqlParameter("@Password", password));
            var success = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var message = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            cmd.ExecuteReader();
            if (Convert.ToInt32(success.Value) == 1)
            {
                outcome = new OperationOutcome(OperationOutcomeStatus.Success);
                outcome.Messages.Add(new OperationOutcomeMessage { Message = Convert.ToString(message.Value) });
            }
            else
            {
                outcome = new OperationOutcome(OperationOutcomeStatus.Failure);
                outcome.Messages.Add(new OperationOutcomeMessage { Message = Convert.ToString(message.Value) });
            }
            conn.Close();
            return Task.FromResult(outcome);
        }
    }
}
