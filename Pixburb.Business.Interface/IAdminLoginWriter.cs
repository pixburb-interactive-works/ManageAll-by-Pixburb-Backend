using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Interface
{
    public interface IAdminLoginWriter
    {
        Task<OperationOutcome> ValidateAdmin(Admin admin);

        Task<OperationOutcome> ForgotPassword(string email);
    }
}
