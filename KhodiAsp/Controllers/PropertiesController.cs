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
    public class PropertiesController : ApiController
    {

        readonly PropertiesRepository propRepo = new PropertiesRepository();

        //[Authorize]
        [HttpPost]
        [Route("api/properties/createProperty")]
        [ResponseType(typeof(Response<Properties>))]
        public async Task<IHttpActionResult> createProperties(Guid landlordId, Properties property)
        {
            var data = await propRepo.createProperty(landlordId, property);
            return Ok(data);
        }

        //[Authorize]
        [HttpGet]
        [Route("api/properties/getLandlordProperties")]
        [ResponseType(typeof(Response<List<Properties>>))]
        public IHttpActionResult getLandlordProperties(Guid landlordId)
        {
            var data = propRepo.getLandlordProperties(landlordId);
            return Ok(data);
        }

        //[Authorize]
        [HttpPatch]
        [Route("api/properties/updateProperty")]
        [ResponseType(typeof(Response<Properties>))]
        public async Task<IHttpActionResult> updateProperty(Properties property)
        {
            var data = await propRepo.updateProperty(property);
            return Ok(data);
        }

        //[Authorize]
        [HttpDelete]
        [Route("api/properties/deleteProperty")]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> deleteProperty(Guid propertyId)
        {
            var data = await propRepo.deleteProperty(propertyId);
            return Ok(data);
        }
    }
}
