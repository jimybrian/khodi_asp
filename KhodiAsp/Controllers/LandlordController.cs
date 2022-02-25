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
    public class LandlordController : ApiController
    {

        readonly LandlordRepository landlordRepo = new LandlordRepository();

        [Authorize]
        [HttpPost]
        [Route("api/landlords/createLandlord")]
        [ResponseType(typeof(Response<Landlords>))]
        public async Task<IHttpActionResult> createLandlord(Guid userId)
        {
            var data = await landlordRepo.createLandlord(userId);
            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("api/landlords/getLandlord")]
        [ResponseType(typeof(Response<Landlords>))]
        public IHttpActionResult getLandlord(Guid userId)
        {
            var data = landlordRepo.getLandlordInfo(userId);
            return Ok(data);
        }

        [Authorize]
        [HttpDelete]
        [Route("api/landlord/deleteLandlord")]
        public async Task<IHttpActionResult> deleteLandlord(Guid landlordId)
        {
            var data = await landlordRepo.deleteLandlord(landlordId);
            return Ok(data);
        }

    }
}
