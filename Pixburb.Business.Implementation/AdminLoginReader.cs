using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Implementation
{
    public class AdminLoginReader : IAdminLoginReader
    {
        private readonly IAdminLoginDataReader adminLoginDataReader;

        public AdminLoginReader(IAdminLoginDataReader adminLoginDataReader)
        {
            this.adminLoginDataReader = adminLoginDataReader;
        }

        public async Task<OperationOutcome> ValidateAdmin(string username, string password, string orgID)
        {
            ConnectionString connectionString = await this.adminLoginDataReader.ValidateOrganization(orgID);
            var ConnectionString = "Data Source=" + connectionString.DataSource + ";PERSIST SECURITY INFO=True;Initial Catalog=" + connectionString.DataBaseName + ";User ID=" + connectionString.UserId + ";Password=" + connectionString.Password + ";POOLING=True";
            return await this.adminLoginDataReader.ValidateAdmin(username, password, ConnectionString);
        }
    }
}
