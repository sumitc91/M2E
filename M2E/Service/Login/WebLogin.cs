using System.Data.Entity.Validation;
using System.Globalization;
using System.Reflection;
using M2E.Common.Logger;
using M2E.CommonMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models;
using M2E.Models.DataResponse;

namespace M2E.Service.Login
{
    public class WebLogin
    {
        private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EEntities _db = new M2EEntities();

        public LoginResponse Login(string userName, string passwrod, string returnUrl, string keepMeSignedIn)
        {            
            var userData = new LoginResponse();
            if (_db.Users.Any(x => x.Username == userName && x.Password == passwrod))
            {
                var user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");
                if (user != null)
                {
                    userData.AuthToken = "auth-token";
                    userData.TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    userData.Code = "200";
                    try
                    {
                        user.KeepMeSignedIn = keepMeSignedIn == "true" ? "true" : "false";
                        _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        var dbContextException = new DbContextException();
                        DbContextException.LogDbContextException(e);
                    }
                }
                else
                    userData.Code = "403";
            }
            else
                userData.Code = "401";            
            return userData;
        }
    }
}