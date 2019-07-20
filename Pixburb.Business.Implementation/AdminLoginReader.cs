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

        public async Task<OperationOutcome> ValidateAdmin(Admin admin)
        {
            return await this.adminLoginDataReader.ValidateOrganization(admin);
        }
    }
}
