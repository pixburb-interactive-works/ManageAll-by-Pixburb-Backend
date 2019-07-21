using Newtonsoft.Json;
using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pixburb.Action.Controllers
{
    [RoutePrefix("api/v1/Inventory")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryWriter inventoryWriter;

        public InventoryController(IInventoryWriter inventoryWriter)
        {
            this.inventoryWriter = inventoryWriter;
        }

        [Route("saveInventory")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveInventory(object inventory)
        {
            List<Inventory> inventoryValue = JsonConvert.DeserializeObject<List<Inventory>>(Convert.ToString(inventory));
            OperationOutcome outcome = await this.inventoryWriter.SaveInventory(inventoryValue);
            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
