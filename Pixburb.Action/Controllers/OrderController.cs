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

        [Route("savecategory")]
        [HttpPost]
        public async Task<HttpResponseMessage> Category(object jsonValue)
        {
            Category category = JsonConvert.DeserializeObject<Category>(Convert.ToString(jsonValue));
            //OperationOutcome outcome = await this.categoryWriter.Category(category);
            //return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
