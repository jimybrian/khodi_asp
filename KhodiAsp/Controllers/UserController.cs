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
using System.Web.Http.Cors;
using KhodiAsp.Emails;
using System.Net.Http.Headers;

namespace KhodiAsp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly UserRepository userRepo = new UserRepository();
        private readonly EmailHandler emailHander = new EmailHandler();

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
        [Route("api/users/authenticate")]
        [ResponseType(typeof(Response<UserItems>))]
        public async Task<IHttpActionResult> authenticateUser(LoginItems login)
        {
            var data = await userRepo.authenticateUser(login);

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

        [HttpGet]
        [Route("api/users/verfiyUser")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<HttpResponseMessage> verifyUser(string email, Guid userId)
        {
            var data = await userRepo.verifyUser(email, userId);

            var response = new HttpResponseMessage();
            response.Content = new StringContent(data.response);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        [HttpPost]
        [Route("api/users/unlockAccount")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> unlockAccount(LoginItems login)
        {
            var data = await userRepo.unlockAccount(login);

            return Ok(data);
        }

        //[Authorize]
        [HttpPatch]
        [Route("api/users/updateUser")]
        [ResponseType(typeof(Response<Users>))]
        public async Task<IHttpActionResult> updateUser(Users user)
        {
            var data = await userRepo.updateUser(user);
            return Ok(data);
        }

        [HttpPost]
        [Route("api/users/changePassword")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> changePassword(ForgotPasswordItems creds)
        {
            var data = await userRepo.changePassword(creds);

            return Ok(data);
        }

        [HttpPost]
        [Route("api/users/deleteEmails")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> deleteEmails(LoginItems logins)
        {
            var data = await userRepo.deleteEmails(logins);

            return Ok(data);
        }

        //[HttpPost]
        //[Route("api/users/sendEmail")]
        //public IHttpActionResult sendTestEmail()
        //{
        //    var email = emailHander.createVerificationEmail("Brian Kiwaa", "briankiwaa@gmail.com", Guid.NewGuid());
        //    emailHander.sendEmail("briankiwaa@gmail.com", email, "Verify Account");

        //    return Ok();
        //}
    }
}
