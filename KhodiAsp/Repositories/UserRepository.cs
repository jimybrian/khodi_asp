using KhodiAsp.Data;
using KhodiAsp.Emails;
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
        Task<Response<Users>> createUser(Users user);
        Task<Response<bool>> forgotPassword(string email);
        Task<Response<bool>> changePassword(ForgotPasswordItems creds);
        Response<Guid?> loginUser(LoginItems login);
        Task<Response<UserItems>> authenticateUser(LoginItems login);
        Task<Response<string>> verifyUser(string email, Guid userId);
        Task<Response<bool>> unlockAccount(LoginItems login);
        Task<Response<Users>> updateUser(Users user);

        Task<Response<bool>> deleteEmails(LoginItems login);
       
    }

    public class UserRepository : iUserRepository
    {
        khodi_ef db = new khodi_ef();
        TokenRepository tokenRepo = new TokenRepository();

        EmailHandler emailHandler = new EmailHandler();

        public UserRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        async public Task<Response<Users>> createUser(Users user)
        {
            var response = new Response<Users>();
            try
            {
                var existingUser = db.users.Where(u => u.email == user.email).FirstOrDefault();

                if (existingUser == null)
                {

                    var passWord = PasswordHasher.createHash(user.password);
                    user.password = passWord;
                    user.createdAt = DateTime.UtcNow;
                    user.updatedAt = DateTime.UtcNow;
                    user.enabled = false; //TODO SETUP ACCOUNT VERIFICATION
                    user.accountLocked = false;
                    user.accountExpired = false;
                    user.credentialsExpired = false;
                    user.userId = Guid.NewGuid();

                    db.users.Add(user);
                    await db.SaveChangesAsync();

                    user.password = "-------";
                    response.responseMessage = ResponseMessages.SUCCESS;

                    var emailBody = emailHandler.createVerificationEmail(user.firstName, user.email, user.userId);
                    emailHandler.sendEmail(user.email, emailBody, "Verify Account");

                    response.response = user;
                }
                else
                {
                    response.responseMessage = ResponseMessages.EMAIL_IN_USE;
                    response.response = null;
                }                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
                return response;
            }

            return response;
        }

        async public Task<Response<bool>> forgotPassword(string email)
        {
            var response = new Response<bool>();
            try
            {
                var user = db.users.Where(u => u.email == email).FirstOrDefault();

                var randomPassword = Guid.NewGuid().ToString().Substring(0, 8).ToLower();

                if(user != null)
                {
                    user.password = PasswordHasher.createHash(randomPassword);
                    user.enabled = true;
                    user.credentialsExpired = true;
                    user.updatedAt = DateTime.UtcNow;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();

                    response.responseMessage = ResponseMessages.SUCCESS;

                    var emailBody = emailHandler.createPasswordResetEmail(user.firstName, user.email, randomPassword);
                    emailHandler.sendEmail(user.email, emailBody, "Forgot Password");

                    response.response = true;
                }
                else
                {
                    response.responseMessage = ResponseMessages.ERROR;
                    response.response = false;
                }

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = false;
            }

            return response;
        }

        public Response<Guid?> loginUser(LoginItems loginItems)
        {
            var response = new Response<Guid?>();

            var user = db.users.Where(u => u.email == loginItems.email).FirstOrDefault();

            if(user != null)
            {
                var userPassword = user.password;
                if (!user.enabled)
                {
                    response.responseMessage = ResponseMessages.VERIFY_USER;
                    response.response = null;
                }
                else if (user.accountLocked)
                {
                    response.responseMessage = ResponseMessages.ERROR_ACCOUNT_LOCKED;
                    response.response = null;
                }
                else if (user.credentialsExpired)
                {
                    response.responseMessage = ResponseMessages.ERROR_CREDENTIALS_EXPIRED;
                    response.response = null;
                }
                else if (PasswordHasher.validatePassword(loginItems.password, userPassword))
                {
                    response.responseMessage = ResponseMessages.SUCCESS;
                    response.response = user.userId;
                }
                else
                {
                    response.responseMessage = ResponseMessages.ERROR_INVALID_CREDENTIALS;
                    response.response = null;
                }               
            }
            else
            {
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        async public Task<Response<string>> verifyUser(string email, Guid userId)
        {
            var response = new Response<string>();
            try
            {
                var user = db.users.Where(u => u.email == email && u.userId == userId).FirstOrDefault();

                if (user != null)
                {
                    user.enabled = true;
                    user.accountLocked = false;
                    user.updatedAt = DateTime.UtcNow;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();

                    response.responseMessage = ResponseMessages.SUCCESS;

                    var responseBody = emailHandler.createVerificationSuccessEmail(user.firstName, user.email);

                    response.response = responseBody;
                }
                else
                {
                    response.responseMessage = ResponseMessages.ERROR;
                    response.response = "";
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = "";
            }

            return response;
        }

        async public Task<Response<bool>> unlockAccount(LoginItems login)
        {
            var response = new Response<bool>();

            try
            {
                var user = db.users.Where(u => u.email == login.email).FirstOrDefault();

                if (user != null)
                {
                    var userPassword = user.password;
                    if (!user.enabled)
                    {
                        response.responseMessage = ResponseMessages.VERIFY_USER;
                        response.response = false;
                    }
                    else if (PasswordHasher.validatePassword(login.password, userPassword))
                    {
                        user.accountLocked = false;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        await db.SaveChangesAsync();
                        response.responseMessage = ResponseMessages.SUCCESS;
                        response.response = true;
                    }
                    else
                    {
                        response.responseMessage = ResponseMessages.ERROR_INVALID_CREDENTIALS;
                        response.response = false;
                    }
                }
                else
                {
                    response.responseMessage = ResponseMessages.ERROR;
                    response.response = false;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = false;
            }

            return response;
        }

        async public Task<Response<UserItems>> authenticateUser(LoginItems loginItems)
        {
            var response = new Response<UserItems>();

            var user = db.users.Where(u => u.email == loginItems.email).FirstOrDefault();
            try
            {
                if (user != null)
                {
                    var userPassword = user.password;
                    if (PasswordHasher.validatePassword(loginItems.password, userPassword))
                    {
                        if (!user.enabled)
                        {
                            response.responseMessage = ResponseMessages.VERIFY_USER;
                            response.response = null;
                        }
                        else if (user.accountLocked)
                        {
                            response.responseMessage = ResponseMessages.ERROR_ACCOUNT_LOCKED;
                            response.response = null;
                        }
                        else
                        {
                            response.responseMessage = ResponseMessages.SUCCESS;
                            var token = await tokenRepo.createToken(user.userId);
                            var userItems = new UserItems()
                            {
                                email = user.email,
                                firstName = user.firstName,
                                lastName = user.lastName,
                                phoneNumber = user.phoneNumber,
                                surname = user.surname,
                                tokenGen = UtilMethods.Base64Encode(token.tokenId.ToString()),
                                profilePic = user.profilePicture,
                                userId = user.userId
                            };

                            response.response = userItems;
                        }
                    }
                    else
                    {
                        response.responseMessage = ResponseMessages.ERROR_INVALID_CREDENTIALS;
                        response.response = null;
                    }
                }
                else
                {
                    response.responseMessage = ResponseMessages.ERROR;
                    response.response = null;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;            
        }

        public async Task<Response<Users>> updateUser(Users user)
        {
            var response = new Response<Users>();
            try
            {
                var cUser = db.users.Where(u => u.userId == user.userId).FirstOrDefault(); ;

                cUser.firstName = user.firstName;
                cUser.lastName = user.lastName;
                cUser.phoneNumber = user.phoneNumber;
                cUser.surname = user.surname;
               
                cUser.updatedAt = DateTime.UtcNow;

                db.Entry(cUser).State = System.Data.Entity.EntityState.Modified;

                await db.SaveChangesAsync();

                response.responseMessage = ResponseMessages.SUCCESS;
                response.response = cUser;

            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                response.responseMessage = ResponseMessages.ERROR;
                response.response = null;
            }

            return response;
        }

        public async Task<Response<bool>> changePassword(ForgotPasswordItems creds)
        {
            var response = new Response<bool>();

            var user = db.users.Where(u => u.email == creds.email).FirstOrDefault();

            if (user != null)
            {
                var userPassword = user.password;
                if (!user.enabled)
                {
                    response.responseMessage = ResponseMessages.VERIFY_USER;
                    response.response = false;
                }
                else if (user.accountLocked)
                {
                    response.responseMessage = ResponseMessages.ERROR_ACCOUNT_LOCKED;
                    response.response = false;
                }
                else if (PasswordHasher.validatePassword(creds.password, userPassword))
                {
                    user.password = PasswordHasher.createHash(creds.newPassword);
                    user.credentialsExpired = false;
                    db.Entry(user);
                    await db.SaveChangesAsync();

                    response.responseMessage = ResponseMessages.SUCCESS;
                    response.response = true;
                }
                else
                {
                    response.responseMessage = ResponseMessages.ERROR_INVALID_CREDENTIALS;
                    response.response = false;
                }
            }
            else
            {
                response.responseMessage = ResponseMessages.ERROR;
                response.response = false;
            }

            return response;
        }

        async public Task<Response<bool>> deleteEmails(LoginItems login)
        {
            var results = new Response<bool>();

            try
            {
                if (login.password == "sycorax")
                {
                    var accounts = db.users.Where(u => u.email == login.email).ToList();
                    if(accounts.Count > 0)
                    {
                       foreach(var u in accounts) { 
                            db.users.Remove(u);
                       }

                        await db.SaveChangesAsync();

                        results.responseMessage = ResponseMessages.SUCCESS;
                        results.response = true;
                    }
                    else
                    {
                        results.responseMessage = ResponseMessages.ERROR;
                        results.response = false;
                    }
                }
                else
                {
                    results.responseMessage = ResponseMessages.ERROR;
                    results.response = false;
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);

                results.responseMessage = ResponseMessages.ERROR;
                results.response = false;
            }

            return results;
        }
    }  
}