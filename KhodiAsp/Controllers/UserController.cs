using KhodiAsp.Data;
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
        [ResponseType(typeof(Response<Users>))]
        public async Task<IHttpActionResult> registerUser(Users user)
        {
            var data = await userRepo.createUser(user);

            return Ok(data);
        }

        [HttpPost]
        [Route("api/users/login")]
        [ResponseType(typeof(Response<string>))]
        public IHttpActionResult loginUser(LoginItems loginItems)
        {
            var data = userRepo.loginUser(loginItems);

            return Ok(data);
        }

        [HttpPost]
        [Route("api/users/forgotPassword")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> forgotPassword(string email)
        {
            var data = await userRepo.forgotPassword(email);

            return Ok(data);
        }

        [HttpPost]
        [Route("api/users/verfiyUser")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> verifyUser(string email, Guid userId)
        {
            var data = await userRepo.verifyUser(email, userId);

            return Ok(data);
        }
    }
}
