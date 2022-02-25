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
    interface iTenantRepository
    {
        Task<Response<Tenants>> createTenant(Guid userId);
        Response<Tenants> getTenantInfo(Guid userId);
        Task<Response<bool>> deleteTenant(Guid tenantId);
    }
    public class TenantRepository : iTenantRepository
    {
        khodi_ef db = new khodi_ef();

        public async Task<Response<Tenants>> createTenant(Guid userId)
        {
            var response = new Response<Tenants>();
            try
            {
                var tenant = new Tenants()
                {
                    tenantId = Guid.NewGuid(),
                    userId = userId
                };

                db.tenants.Add(tenant);
                await db.SaveChangesAsync();

                response.response = tenant;
                response.responseMessage = ResponseMessages.SUCCESS;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        async public Task<Response<bool>> deleteTenant(Guid tenantId)
        {
            var response = new Response<bool>();
            try
            {
                var tenant = db.tenants.Where(t => t.tenantId == tenantId).FirstOrDefault();
                if(tenant != null)
                {
                    db.tenants.Remove(tenant);
                    await db.SaveChangesAsync();
                    response.response = true;
                    response.responseMessage = ResponseMessages.SUCCESS;
                }
                else
                {
                    response.response = false;
                    response.responseMessage = ResponseMessages.ERROR;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.response = false;
                response.responseMessage = ResponseMessages.ERROR;
            }

            return response;
        }

        public Response<Tenants> getTenantInfo(Guid userId)
        {
            var response = new Response<Tenants>();

            var tenant = db.tenants.Where(t => t.userId == userId).FirstOrDefault();

            if(tenant != null)
            {
                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = tenant;
            }
            else
            {
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }
    }
}