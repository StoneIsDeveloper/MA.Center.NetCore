using MA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static readonly string ConnectionString = "Data source =DESKTOP-PTR0L8C; Initial Catalog =macenterv2; User ID =stone; Password=stone123";

        static void Main(string[] args)
        {
            int[] idList = new int[] { 1, 3 };

            var results =  GetContact(idList);
        }

        public static List<Contact> GetContact(int[] idList)
        {
            var results = new List<Contact>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("dbo.GetContactByIdRange", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable tvp = new DataTable();
                    tvp.Columns.Add(new DataColumn("ID", typeof(int)));
                    foreach (var id in idList)
                        tvp.Rows.Add(id);
                    SqlParameter tvparam = cmd.Parameters.AddWithValue("@IDList", tvp);
                    tvparam.SqlDbType = SqlDbType.Structured;
                    tvparam.TypeName = "dbo.ListIDTableType";
                  //  cmd.Parameters.Add(tvparam);

                    //cmd.Parameters.AddWithValue("@IDList", idList);


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
                            //  Status = Convert.ToInt32( reader["Status"].ToString()),

                        };

                        results.Add(item);
                    }

                }
                
              

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
