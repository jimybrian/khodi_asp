using KhodiAsp.Models;
using KhodiAsp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KhodiAsp.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserRepository userRepo = new UserRepository();

        [HttpPost]
        [Route("api/users/register")]
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> registerUser(Users user)
        {
            var data = await userRepo.createUser(user);

            return data != null ? 
                (IHttpActionResult)Ok(data) : BadRequest();
        }
    }
}
