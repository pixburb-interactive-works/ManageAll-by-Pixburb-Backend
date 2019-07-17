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

        public Task<OperationOutcome> ValidateAdmin(string email, string password)
        {
            OperationOutcome outcome = new OperationOutcome();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LOGIN_CHECK";

            cmd.Parameters.Add(new SqlParameter("@UserName", email));
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

            return Task.FromResult(outcome);
        }
    }
}
