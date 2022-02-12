using KhodiAsp.Models;
using KhodiAsp.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KhodiAsp.Repositories
{

    interface iUserRepository
    {
        Task<Users> createUser(Users user);

        Task<bool> forgotPassword(string email);

        Task<string> loginUser(string username, string password);
    }

    public class UserRepository : iUserRepository
    {
        khodi_ef db = new khodi_ef();       

        public UserRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        public async Task<Users> createUser(Users user)
        {
            try
            {
                var passWord = PasswordHasher.createHash(user.password);
                user.password = passWord;
                user.createdAt = DateTime.UtcNow;
                user.updatedAt = DateTime.UtcNow;
                user.enabled = false;
                user.accountExpired = false;
                user.credentialsExpired = false;
                user.userId = Guid.NewGuid();

                db.users.Add(user);
                await db.SaveChangesAsync();

                user.password = "-------";
                return user;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<bool> forgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> loginUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}