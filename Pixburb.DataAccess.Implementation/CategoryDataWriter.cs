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

            cmd.Parameters.Add(new SqlParameter("@Id", category.Id));
            cmd.Parameters.Add(new SqlParameter("@ParentId", category.ParentId));
            cmd.Parameters.Add(new SqlParameter("@Category", category.CategoryName));
            cmd.Parameters.Add(new SqlParameter("@RecordStatus", category.RecordStatus));
            var status = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var message = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            cmd.ExecuteNonQuery();

            return Task.FromResult(outcome);
        }
    }
}
