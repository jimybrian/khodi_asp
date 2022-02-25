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
    public class TenantController : ApiController
    {

        readonly TenantRepository tenantRepo = new TenantRepository();

        [Authorize]
        [HttpPost]
        [Route("api/tenants/createTenant")]
        [ResponseType(typeof(Response<Tenants>))]
        public async Task<IHttpActionResult> createTenant(Guid userId)
        {
            var data = await tenantRepo.createTenant(userId);
            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("api/tenants/getTenant")]
        [ResponseType(typeof(Response<Tenants>))]
        public IHttpActionResult getTenant(Guid userId)
        {
            var data = tenantRepo.getTenantInfo(userId);
            return Ok(data);
        }

        [Authorize]
        [HttpDelete]
        [Route("api/tenants/deleteTenant")]
        public async Task<IHttpActionResult> deleteTenant(Guid tenantId)
        {
            var data = await tenantRepo.deleteTenant(tenantId);
            return Ok(data);
        }

    }
}
