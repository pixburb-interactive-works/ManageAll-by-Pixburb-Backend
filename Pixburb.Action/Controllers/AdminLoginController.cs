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
    [RoutePrefix("api/v1/adminlogin")]
    public class AdminLoginController : ApiController
    {
        private readonly IAdminLoginWriter adminLoginWriter;

        public AdminLoginController(IAdminLoginWriter adminLoginWriter)
        {
            this.adminLoginWriter = adminLoginWriter;
        }

        [Route("validateAdmin")]
        [HttpPost]
        public async Task<HttpResponseMessage> ValidateAdmin(object value)
        {
            Admin admin = JsonConvert.DeserializeObject<Admin>(Convert.ToString(value));
            OperationOutcome outcome = await this.adminLoginWriter.ValidateAdmin(admin);

            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }

        public async Task<HttpResponseMessage> ForgotPassword(object value)
        {
            Admin admin = JsonConvert.DeserializeObject<Admin>(Convert.ToString(value));
            OperationOutcome outcome = await this.adminLoginWriter.ForgotPassword(admin.username);

            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
