﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M2E.Models;
using M2E.Models.DataResponse;
using M2E.Common.Logger;
using System.Reflection;
using M2E.CommonMethods;

namespace M2E.Service.Client
{
    public class ClientDetailService
    {
        private static readonly ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private DbContextException _dbContextException = new DbContextException();
        private readonly M2EContext _db = new M2EContext();

        public ResponseModel<ClientDetailsModel> GetClientDetails(string username)
        {
            var response = new ResponseModel<ClientDetailsModel>();
                        
            try
            {
                var clientDetailDBResult = _db.Users.SingleOrDefault(x => x.Username == username);
                if (clientDetailDBResult != null)
                {
                    var createClientDetailResponse = new ClientDetailsModel
                    {
                        FirstName = clientDetailDBResult.FirstName,
                        LastName = clientDetailDBResult.LastName,
                        Username = clientDetailDBResult.Username
                    };
                    response.Status = 200;
                    response.Message = "success";
                    response.Payload = createClientDetailResponse;
                }
                else
                {
                    response.Status = 404;
                    response.Message = "username not found";
                }
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "exception occured !!!";
            }
            return response;
        }
    }
}