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
    public class OrderController : ApiController
    {
        private readonly IOrderWriter orderWriter;

        public OrderController(IOrderWriter orderWriter)
        {
            this.orderWriter = orderWriter;
        }

        [Route("placeorder")]
        [HttpPost]
        public async Task<HttpResponseMessage> PlaceOrder(object jsonValue)
        {
            List<PlaceOrder> placeOrder = JsonConvert.DeserializeObject<List<PlaceOrder>>(Convert.ToString(jsonValue));
            OperationOutcome outcome = await this.orderWriter.PlaceOrder(placeOrder);
            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
