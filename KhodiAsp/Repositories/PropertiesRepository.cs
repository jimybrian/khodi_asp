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

    interface iPropertyRepository
    {
        Task<Response<Properties>> createProperty(Guid landlordId, Properties property);
        Task<Response<Properties>> updateProperty(Properties property);
        Response<List<Properties>> getLandlordProperties(Guid landlordId);
        Task<Response<bool>> deleteProperty(Guid propertyId);

    }

    public class PropertiesRepository : iPropertyRepository
    {
        khodi_ef db = new khodi_ef();

        async public Task<Response<Properties>> createProperty(Guid landlordId, Properties property)
        {
            var response = new Response<Properties>();
            try 
            {
                property.propertyId = Guid.NewGuid();
                property.createdAt = DateTime.UtcNow;
                property.updatedAt = DateTime.UtcNow;
                
                db.properties.Add(property);

                var landlordProperties = new LandlordProperties()
                {
                    propertyId = property.propertyId,
                    landlordId = landlordId
                };

                db.landlordProperties.Add(landlordProperties);

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = property;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        async public Task<Response<bool>> deleteProperty(Guid propertyId)
        {
            var response = new Response<bool>();
            try
            {
                var property = db.properties.Where(p => p.propertyId == propertyId).FirstOrDefault();
                var landLordProperty = db.landlordProperties.Where(p => p.propertyId == propertyId).FirstOrDefault();

                db.properties.Remove(property);
                db.landlordProperties.Remove(landLordProperty);

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

        public Response<List<Properties>> getLandlordProperties(Guid landlordId)
        {
            var response = new Response<List<Properties>>();

            var properties = from p in db.properties
                             join l in db.landlordProperties on p.propertyId equals l.propertyId
                             where l.landlordId == landlordId
                             select new { p };

            var propertiesList = new List<Properties>();

            foreach(var p in properties)
                propertiesList.Add(p.p);
            


            response.responseMessage = ResponseMessages.SUCCESS;
            response.response = propertiesList;

            return response;
        }

        public async Task<Response<Properties>> updateProperty(Properties property)
        {
            var response = new Response<Properties>();
            try
            {
                property.updatedAt = DateTime.UtcNow;
                db.Entry(property).State = System.Data.Entity.EntityState.Modified;

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = property;
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