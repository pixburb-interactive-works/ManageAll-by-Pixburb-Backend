using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Implementation
{
    public class AdminLoginWriter : IAdminLoginWriter
    {
        private readonly IAdminLoginWriter adminLoginDataWriter;

        public AdminLoginWriter(IAdminLoginWriter adminLoginDataWriter)
        {
            this.adminLoginDataWriter = adminLoginDataWriter;
        }

        public async Task<OperationOutcome> ValidateAdmin(Admin admin)
        {
            return await this.adminLoginDataWriter.ValidateOrganization(admin);
        }
    }
}
