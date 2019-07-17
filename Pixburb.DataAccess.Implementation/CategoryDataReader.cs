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
    public class CategoryDataReader : ICategoryDataReader
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<object> GetCategory()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GET_CATEGORY";

            var success = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var message = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
            var reader = cmd.ExecuteReader();

            while(reader.HasRows)
            {

            }

            return Task.FromResult(object);
        }
    }
}
