using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Interface
{
    public interface ICategoryDataWriter
    {
        Task<OperationOutcome> Category(Category category);
    }
}
