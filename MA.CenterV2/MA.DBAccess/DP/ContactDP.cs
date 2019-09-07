using MA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MA.DBAccess.DP
{
    public class ContactDP
    {
        private static readonly string connString = DBConfiguration.ConnectionString;

        public static ICollection<Contact> GetUseInfos()
        {
            var results = new List<Contact>();
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetContactByIdRange", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                int[] idList = new int[] { 1,2 }; 
             
                cmd.Parameters.AddWithValue("@IDList", idList);

               var reader =  cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var item = new Contact()
                    {
                        ContactId = Convert.ToInt64(reader["ContactId"].ToString()),
                        OwnerID = reader["OwnerID"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        Zip = reader["Zip"].ToString(),
                        Status = reader["Status"].ToString(),

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
        public static ICollection<Contact> GetUseInfosByIds(int[] idList)
        {
            var results = new List<Contact>();
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.GetContactByIdRange", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDList", idList);

                var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var item = new Contact()
                    {
                        ContactId = Convert.ToInt64(reader["ContactId"].ToString()),
                        OwnerID = reader["OwnerID"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        Zip = reader["Zip"].ToString(),
                        Status = reader["Status"].ToString(),

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

        public static bool AddContact()
        {
            var results = new List<Contact>();
            try
            {
                SqlDataReader reader = SqlHelper.GetReader("dbo.GetContacts", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    var item = new Contact()
                    {
                        ContactId = Convert.ToInt64(reader["ContactId"].ToString()),
                        OwnerID = reader["OwnerID"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        Zip = reader["Zip"].ToString(),
                        Status = reader["Status"].ToString(),

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

            return true;

        }


    }
}
