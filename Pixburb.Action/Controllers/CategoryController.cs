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
    [RoutePrefix("api/v1/category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryWriter categoryWriter;

        public CategoryController(ICategoryWriter categoryWriter)
        {
            this.categoryWriter = categoryWriter;
        }

        [Route("savecategory")]
        [HttpPost]
        public async Task<HttpResponseMessage> Category(Category category)
        {
            OperationOutcome outcome = await this.categoryWriter.Category(category);
            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
