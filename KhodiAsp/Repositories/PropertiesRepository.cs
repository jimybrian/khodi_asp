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
        List<Properties> getLandlordProperties(Guid landlordId);
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
                

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        public Task<Response<bool>> deleteProperty(Guid propertyId)
        {
            throw new NotImplementedException();
        }

        public List<Properties> getLandlordProperties(Guid landlordId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Properties>> updateProperty(Properties property)
        {
            throw new NotImplementedException();
        }
    }
}