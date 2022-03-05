using KhodiAsp.Data;
using KhodiAsp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KhodiAsp.Repositories
{
    interface iAssetsRepository
    {
        Task<Response<Assets>> addPropertyAsset(Guid properyId, Assets asset);
        Task<Response<Assets>> updatePropertyAsset(Assets asset);
        Response<List<Assets>> getProperyAssets(string properyId);
        Task<Response<bool>> deleteProperyAsset(Guid assetId);
    }

    public class AssetsRepository : iAssetsRepository
    {
        khodi_ef db = new khodi_ef();

        async public Task<Response<Assets>> addPropertyAsset(Guid properyId, Assets asset)
        {
            var response = new Response<Assets>();
            try
            {
                asset.assetId = Guid.NewGuid();
                asset.createdAt = DateTime.UtcNow;
                asset.updatedAt = DateTime.UtcNow;
                asset.propertyId = properyId.ToString();

                db.assets.Add(asset);

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = asset;

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }
            return response;
        }

        async public Task<Response<bool>> deleteProperyAsset(Guid assetId)
        {
            var response = new Response<bool>();
            try
            {
                var asset = db.assets.Where(a => a.assetId == assetId).FirstOrDefault();

                db.assets.Remove(asset);

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = true;

            }catch(Exception ex)
            {

                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = false;
            }
            return response;
        }

        public Response<List<Assets>> getProperyAssets(string properyId)
        {
            var response = new Response<List<Assets>>();
            var assets = db.assets.Where(a => a.propertyId == properyId).ToList();

            response.responseMessage = ResponseMessages.SUCCESS;
            response.response = assets;

            return response; 
        }

        async public Task<Response<Assets>> updatePropertyAsset(Assets asset)
        {
            var response = new Response<Assets>();
            try
            {         
                asset.updatedAt = DateTime.UtcNow;

                db.Entry(asset).State = System.Data.Entity.EntityState.Modified;

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = asset;

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }
            return response;
        }
    }
}