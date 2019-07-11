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
        public async Task<HttpResponseMessage> ValidateAdmin(string email, string password)
        {
            if ((email == null || email == "") || (password == null || password == ""))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            OperationOutcome outcome = await this.adminLoginReader.ValidateAdmin(email, password);

            return Request.CreateResponse(HttpStatusCode.OK, outcome);
        }
    }
}
