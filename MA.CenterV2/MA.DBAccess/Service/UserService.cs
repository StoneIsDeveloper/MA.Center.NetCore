using MA.DBAccess.DP;
using MA.DBAccess.IService;
using MA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MA.DBAccess
{
    public class UserService : IUserService
    {
        public List<UserInfo> GetUseInfos()
        {
            var results = UserDP.GetUseInfos();
            return results;

        }
    }
}
