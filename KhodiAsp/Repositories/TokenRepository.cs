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
    interface iTokenRepository
    {
        bool validateToken(Guid tokenId);
        Task<Tokens> createToken(Guid userId);
        Task<Response<bool>> invalidateAllUserTokens(Guid userId);
        Tokens getUserToken(Guid userId);
    }

    public class TokenRepository : iTokenRepository
    {
        khodi_ef db = new khodi_ef();

        public TokenRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        async public Task<Tokens> createToken(Guid userId)
        {
            var tokens = db.tokens.Where(t => t.userId == userId && t.validity == true);
            try
            {
                foreach(var t in tokens)
                {
                    t.validity = false;
                    t.updatedAt = DateTime.Now;
                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;                   
                }

                var token = new Tokens()
                {
                    tokenId = Guid.NewGuid(),
                    userId = userId,
                    validity = true,
                    createdAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                };

                db.tokens.Add(token);
                await db.SaveChangesAsync();
                return token;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }            
        }

        public Tokens getUserToken(Guid userId)
        {
            var token = db
                .tokens.Where(t => t.userId == userId && t.validity == true).FirstOrDefault();

            if (token != null)
                return token;
            else
                return null;            
        }

        async public Task<Response<bool>> invalidateAllUserTokens(Guid userId)
        {
            var response = new Response<bool>();
            try
            {
                var tokens = db.tokens.Where(t => t.userId == userId && t.validity == true);
                foreach (var t in tokens)
                {
                    t.validity = false;
                    t.updatedAt = DateTime.Now;
                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                }

                await db.SaveChangesAsync();
                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = false;
            }
            return response;
        }

        public bool validateToken(Guid tokenId)
        {
            var token = db.tokens
                .Where(t => t.tokenId == tokenId && t.validity == true)
                .FirstOrDefault();

            return (token != null);                 
        }
    }
}