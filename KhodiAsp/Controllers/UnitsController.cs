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

namespace KhodiAsp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UnitsController : ApiController
    {

        readonly UnitRepository uRepo = new UnitRepository();

        //[Authorize]
        [HttpPost]
        [Route("api/units/addUnit")]
        [ResponseType(typeof(Response<Units>))]
        public async Task<IHttpActionResult> addUnit(Guid propertyId, Units unit)
        {
            var data = await uRepo.addUnits(propertyId, unit);
            return Ok(data);
        }

        //[Authorize]
        [HttpPatch]
        [Route("api/units/updateUnit")]
        [ResponseType(typeof(Response<Units>))]
        public async Task<IHttpActionResult> updateUnits(Units unit)
        {
            var data = await uRepo.updateUnits(unit);
            return Ok(data);
        }

        //[Authorize]
        [HttpGet]
        [Route("api/units/getPropertyUnits")]
        [ResponseType(typeof(Response<List<Units>>))]
        public IHttpActionResult getPropertyUnits(Guid propertyId)
        {
            var data = uRepo.getPropertyUnits(propertyId);

            return Ok(data);
        }

        //[Authorize]
        [HttpDelete]
        [Route("api/units/deleteUnit")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> deleteUnit(Guid unitId)
        {
            var data = await uRepo.deleteUnit(unitId);

            return Ok(data);
        }

    }
}
