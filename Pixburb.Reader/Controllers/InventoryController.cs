using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pixburb.Reader.Controllers
{
    [RoutePrefix("api/v1/Inventory")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryReader inventoryReader;

        public InventoryController(IInventoryReader inventoryReader)
        {
            this.inventoryReader = inventoryReader;
        }

        public async Task<List<InventoryValue>> GetInventory(int? userId)
        {
            return await this.inventoryReader.GetInventory(userId);
        }
    }
}
