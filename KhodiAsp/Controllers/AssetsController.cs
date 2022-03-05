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
    public class AssetsController : ApiController
    {
        readonly AssetsRepository aRepo = new AssetsRepository();


        [Authorize]
        [HttpPost]
        [Route("api/propertyAssets/addPropertyAsset")]
        [ResponseType(typeof(Response<Assets>))]
        public async Task<IHttpActionResult> addProperyAsset(Guid propertyId, Assets asset)
        {
            var data = await aRepo.addPropertyAsset(propertyId, asset);

            return Ok(data);
        }

        [Authorize]
        [HttpPatch]
        [Route("api/propertyAssets/updatePropertyAssets")]
        [ResponseType(typeof(Response<Assets>))]
        public async Task<IHttpActionResult> updatePropertyAsset(Assets asset)
        {
            var data = await aRepo.updatePropertyAsset(asset);

            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        [Route("api/properyAssets/getPropertyAssets")]
        [ResponseType(typeof(Response<List<Assets>>))]
        public IHttpActionResult getPropertyAssets(string propertyId)
        {
            var data = aRepo.getProperyAssets(propertyId);

            return Ok(data);
        }

    }
}
