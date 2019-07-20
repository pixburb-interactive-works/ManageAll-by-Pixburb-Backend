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
    [RoutePrefix("api/v1/adminlogin")]
    public class AdminLoginController : ApiController
    {
        private readonly IAdminLoginReader adminLoginReader;

        public AdminLoginController(IAdminLoginReader adminLoginReader)
        {
            this.adminLoginReader = adminLoginReader;
        }

        [Route("validateAdmin")]
        [HttpPost]
        public async Task<HttpResponseMessage> ValidateAdmin(Admin admin)
        {
            if ((admin.username == null || admin.username == "") || (admin.password == null || admin.password == "") || (admin.orgID == null || admin.orgID == ""))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            OperationOutcome outcome = await this.adminLoginReader.ValidateAdmin(admin);

            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
