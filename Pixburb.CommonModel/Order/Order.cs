using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Customername { get; set; }
        public string Payment { get; set; }
        public decimal Amount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
