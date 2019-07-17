using Pixburb.Business.Interface;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Implementation
{
    public class CategoryReader : ICategoryReader
    {
        private readonly ICategoryDataReader categoryDataReader;

        public CategoryReader(ICategoryDataReader categoryDataReader)
        {
            this.categoryDataReader = categoryDataReader;
        }

        public async Task<object> GetCategory()
        {
            return await this.categoryDataReader.GetCategory();
        }
    }
}
