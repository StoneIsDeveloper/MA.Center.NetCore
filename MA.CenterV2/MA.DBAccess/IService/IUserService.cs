using MA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MA.DBAccess.IService
{
    public interface IUserService
    {
        List<UserInfo> GetUseInfos();
    }
}
