using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class PlaceOrder
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double PinCode { get; set; }
        public double PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RecordStatus { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

    }
}
