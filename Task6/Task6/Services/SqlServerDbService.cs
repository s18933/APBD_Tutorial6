using APBD_Task4.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Task6.Services
{
    public class SqlServerDbService : IDbService
    {
        string _connString = "Data Source=db-mssql;Initial Catalog=s18933;Integrated Security=True";
        public int GetStudentByIndex(string index)
        {
            var newProdID = 0;
            using (var con = new SqlConnection(_connString))
            {
                con.Open();

                    using (var com = new SqlCommand())
                    {
                    com.Connection = con;
                    com.CommandText = "Select Count(1) from Student where IndexNumber = @index;";
                    com.Parameters.AddWithValue("index", index);
                    newProdID = (int)com.ExecuteScalar();

                }
               
            }

            return newProdID;
                    
        }

    }
}
 