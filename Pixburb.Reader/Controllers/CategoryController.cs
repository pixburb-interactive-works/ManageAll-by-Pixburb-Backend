using Pixburb.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pixburb.Reader.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryReader categoryReader;

        public CategoryController(ICategoryReader categoryReader)
        {
            this.categoryReader = categoryReader;
        }

        public async Task<IHttpActionResult> GetCategory()
        {
            return await this.categoryReader.GetCategory();
        }
    }
}
