using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class Categories
    {
        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }

        public FileAttachment Image { get; set; }
        public List<Categories> SubCategories { get; set; }
    }
}
