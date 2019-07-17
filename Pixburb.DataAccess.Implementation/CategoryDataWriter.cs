using Pixburb.CommonModel;
using Pixburb.DataAccess.Implementation.Helpers;
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
    public class CategoryDataWriter : ICategoryDataWriter
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<OperationOutcome> Category(Category category)
        {
            OperationOutcome outcome = new OperationOutcome();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PUT_Category";

            cmd.Parameters.Add(new SqlParameter("@Id", CommonHelper.RemoveDefaultValue(category.Id)));
            cmd.Parameters.Add(new SqlParameter("@ParentId", CommonHelper.RemoveDefaultValue(category.ParentId)));
            cmd.Parameters.Add(new SqlParameter("@Category", CommonHelper.RemoveDefaultValue(category.CategoryName)));
            cmd.Parameters.Add(new SqlParameter("@RecordStatus", CommonHelper.RemoveDefaultValue(category.RecordStatus)));
            var success = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var message = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            cmd.ExecuteNonQuery();

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
