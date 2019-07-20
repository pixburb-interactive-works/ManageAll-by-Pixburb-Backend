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
        [HttpGet]
        public async Task<HttpResponseMessage> ValidateAdmin(string username, string password, string orgID)
        {
            if ((username == null || username == "") || (password == null || password == "") || (orgID == null || orgID == ""))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            OperationOutcome outcome = await this.adminLoginReader.ValidateAdmin(username,password,orgID);

            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
