using MA.DBAccess;
using MA.Web.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MA.CenterV2
{
    class Program
    {
        private static readonly string connString = DBConfiguration.ConnectionString;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GetContact();

        }

        public static void GetContact()
        {
            var results = new List<Contact>();
            try
            {
                int[] idList = new int[] { 1, 2 };

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
                        Status = (ContactStatus) Convert.ToInt64(reader["Status"].ToString()),

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
        }
    }
}
