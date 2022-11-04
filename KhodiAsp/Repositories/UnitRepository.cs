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
    interface iUnitRepository
    {
        Task<Response<Units>> addUnits(Guid propertyId, Units unit);
        Task<Response<Units>> updateUnits(Units unit);
        Response<List<Units>> getPropertyUnits(Guid propertyId);
        Task<Response<bool>> deleteUnit(Guid unitId);
    }

    public class UnitRepository : iUnitRepository
    {
        khodi_ef db = new khodi_ef();

        async public Task<Response<Units>> addUnits(Guid propertyId, Units unit)
        {
            var response = new Response<Units>();
            try
            {
                unit.unitId = Guid.NewGuid();
                unit.createdAt = DateTime.UtcNow;
                unit.updatedAt = DateTime.UtcNow;

                db.units.Add(unit);

                var propertyUnits = new PropertyUnits()
                {
                    unitId = unit.unitId,
                    propertyId = propertyId,
                };

                db.propertyUnits.Add(propertyUnits);

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = unit;
                
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        async public Task<Response<bool>> deleteUnit(Guid unitId)
        {
            var response = new Response<bool>();
            try
            {
                var unit = db.units.Where(u => u.unitId == unitId).FirstOrDefault();
                var propertyUnits = db.propertyUnits.Where(u => u.unitId == unitId).FirstOrDefault();

                db.units.Remove(unit);
                db.propertyUnits.Remove(propertyUnits);

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = true;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = false;
            }
            return response;
        }

        public Response<List<Units>> getPropertyUnits(Guid propertyId)
        {
            var response = new Response<List<Units>>();

            var units = (from u in db.units
                         join p in db.propertyUnits on u.unitId equals p.unitId
                         where p.propertyId == propertyId
                         select new { u });

            var lsUnits = new List<Units>();

            foreach (var unit in units)
                lsUnits.Add(unit.u);
 
            response.responseMessage = ResponseMessages.SUCCESS;
            response.response = lsUnits;

            return response;
        }

        async public Task<Response<Units>> updateUnits(Units unit)
        {
            var response = new Response<Units>();
            try
            {               
                unit.updatedAt = DateTime.UtcNow;


                db.Entry(unit).State = System.Data.Entity.EntityState.Modified;

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = unit;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }
    }
}