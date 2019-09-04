using MA.DBAccess.DP;
using MA.DBAccess.IService;
using MA.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MA.DBAccess.Service
{
    public class AdminUserService : IUserService
    {
        public AdminUserService(IConfiguration config)
        {
            var dbType = config["AdminService"];
        }

        public List<UserInfo> GetUseInfos()
        {
            var results = UserDP.GetUseInfos();
            results.RemoveAt(0);
            return results;

        }
    }
}
