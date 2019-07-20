using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Interface
{
    public interface IAdminLoginReader
    {
        Task<OperationOutcome> ValidateAdmin(string username, string password, string orgID);
    }
}
