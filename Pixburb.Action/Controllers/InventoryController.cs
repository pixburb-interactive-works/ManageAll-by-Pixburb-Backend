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
        public async Task<HttpResponseMessage> SaveInventory(object jsonValue)
        {
            List<Inventory> inventory = JsonConvert.DeserializeObject<List<Inventory>>(Convert.ToString(jsonValue));
            OperationOutcome outcome = await this.inventoryWriter.SaveInventory(inventory);
            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
