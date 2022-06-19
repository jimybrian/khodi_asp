using KhodiAsp.Data;
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

namespace KhodiAsp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TokenController : ApiController
    {
        readonly TokenRepository tRepo = new TokenRepository();

        [HttpPost]
        [Route("api/tokens/invalidateAllTokens")]
        [ResponseType(typeof(Response<string>))]
        async public Task<IHttpActionResult> invalidateAllTokens(Guid userId)
        {
            var data = await tRepo.invalidateAllUserTokens(userId);

            return Ok(data);
        }
    }
}
