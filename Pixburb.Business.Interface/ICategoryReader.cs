﻿using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Interface
{
    public interface ICategoryReader
    {
        Task<List<Categories>> GetCategory();
        Task<List<CategoryBase>> GetCategoryLOV();
    }
}
