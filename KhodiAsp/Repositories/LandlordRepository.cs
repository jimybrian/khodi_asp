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
    interface iLandlordRepository
    {
        Task<Response<Landlords>> createLandlord(Guid userId);
        Response<Landlords> getLandlordInfo(Guid userId);
        Task<Response<bool>> deleteLandlord(Guid landlordId);
    }

    public class LandlordRepository : iLandlordRepository
    {
        khodi_ef db = new khodi_ef();

        async public Task<Response<Landlords>> createLandlord(Guid userId)
        {
            var response = new Response<Landlords>();
            try
            {
                var landlord = new Landlords()
                {
                    landordId = Guid.NewGuid(),
                    userId = userId
                };

                db.landlords.Add(landlord);
                await db.SaveChangesAsync();

                response.response = landlord;
                response.responseMessage = ResponseMessages.SUCCESS;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        public async Task<Response<bool>> deleteLandlord(Guid landlordId)
        {
            var response = new Response<bool>();
            try
            {
                var landlord = db.landlords.Where(t => t.landordId == landlordId).FirstOrDefault();
                if (landlord != null)
                {
                    db.landlords.Remove(landlord);
                    await db.SaveChangesAsync();
                    response.response = true;
                    response.responseMessage = ResponseMessages.SUCCESS;
                }
                else
                {
                    response.response = false;
                    response.responseMessage = ResponseMessages.ERROR;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.response = false;
                response.responseMessage = ResponseMessages.ERROR;
            }

            return response;
        }

        public Response<Landlords> getLandlordInfo(Guid userId)
        {
            var response = new Response<Landlords>();

            var landlord = db.landlords.Where(l => l.userId == userId).FirstOrDefault();

            if (landlord != null)
            {
                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = landlord;
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