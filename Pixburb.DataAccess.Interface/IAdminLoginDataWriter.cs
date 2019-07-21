using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Interface
{
    public interface IAdminLoginDataWriter
    {
        Task<OperationOutcome> ValidateOrganization(Admin admin);
    }
}
