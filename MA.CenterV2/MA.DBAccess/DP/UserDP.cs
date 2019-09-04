using MA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MA.DBAccess.DP
{
    public class UserDP
    {
        public static List<UserInfo> GetUseInfos()
        {
            var results = new List<UserInfo>();
            try
            {
                SqlDataReader reader = SqlHelper.GetReader("dbo.GetAllUserInfos", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    var item = new UserInfo()
                    {
                        Id = Convert.ToInt64(reader["Id"].ToString()),
                        NickName = reader["NickName"].ToString(),
                        IdentityUserId = reader["IdentityUserId"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                    };

                    results.Add(item);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                // LogStoreHelper.WriteError(ex, $"SqlHelper.GetSingleResult error");
                throw ex;
            }
            return results;

        }
    }
}
