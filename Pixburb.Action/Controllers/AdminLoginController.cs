using Pixburb.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

    }
}
