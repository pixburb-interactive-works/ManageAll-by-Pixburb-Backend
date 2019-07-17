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
    [RoutePrefix("api/v1/Category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryReader categoryReader;

        public CategoryController(ICategoryReader categoryReader)
        {
            this.categoryReader = categoryReader;
        }

        [Route("GetCategory")]
        [HttpGet]
        public async Task<List<Categories>> GetCategory()
        {
            return await this.categoryReader.GetCategory();
        }

        [Route("GetCategoryLOV")]
        [HttpGet]
        public async Task<List<CategoryBase>> GetCategoryLOV()
        {
            return await this.categoryReader.GetCategoryLOV();
        }
    }
}
