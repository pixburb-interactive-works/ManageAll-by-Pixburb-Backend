using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Location { get; set; }
        public string BatchCode { get; set; }
        public decimal? Price { get; set; }
        public bool Featured { get; set; }
        public string Tags { get; set; }
        public decimal? Stock { get; set; }
        public double? LimitPerUser { get; set; }
        public DateTime? StockDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public FileAttachment Image { get; set; }
        public List<FileAttachment> Gallery { get; set; } = new List<FileAttachment>();
        public string Status { get; set; }
    }
}
