﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class Category
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }

        public FileAttachment Image { get; set; }

        public string RecordStatus { get; set; }
    }
}
