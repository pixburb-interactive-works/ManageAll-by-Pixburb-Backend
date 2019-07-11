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
            cmd.Parameters.Add(new SqlParameter("@IsSuccess", null));
            cmd.Parameters.Add(new SqlParameter("@Message", null));
            var reader = cmd.ExecuteReader();

            return Task.FromResult(outcome);
        }
    }
}
