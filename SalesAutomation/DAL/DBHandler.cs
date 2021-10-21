using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAutomation.DAL
{
    public class DBHandler
    {
        SqlConnection connection = null;

        

        string connectionString= @"Data Source=DEEPKUMARC;Initial Catalog=SalesAutomation;Integrated Security=True";
        public SqlConnection Connection { get => connection; set => connection = value; }

        
        public SqlConnection GetConnection()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error in connection" + ex.Message);
            }

            return connection;
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null)
                    connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
