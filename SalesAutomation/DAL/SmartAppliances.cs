using SalesAutomation.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAutomation.DAL
{
    public class SmartAppliances
    {
        DBHandler db = new DBHandler();
        double unitPrice = 75350;
        public static double Amount { get; set; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
        public void CalculateNetAmount(SalesDetails s)
        {
            int units = s.NoOfUnits;
            Amount = units * UnitPrice;
            s.NetAmount = units switch
            {
                (<= 5) => Amount,
                (> 5 and <= 10) => (Amount - (Amount * 0.02)),
                (> 10) and (<= 15) => (Amount - (Amount * 0.05)),
                (> 15) and (<= 20) => (Amount - (Amount * 0.08)),
                (> 20) => (Amount - (Amount * 0.1)),
            };
        }
        public int AddSalesDetails(SalesDetails s)
        {
            CalculateNetAmount(s);
            DBHandler db = new DBHandler();
            SqlConnection connection = db.GetConnection();
            string insertQuery = $"insert into SalesDetails values ('{s.CustomerName}',{s.NoOfUnits},{s.NetAmount},{Amount},{UnitPrice})";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            try
            {
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public SalesDetails GetSalesDetailsById(int id)
        {
            SalesDetails details = null;
            SqlConnection connection = db.GetConnection();
            string selectQuery = $"select * from SalesDetails where Sales_id={id}";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    details = new SalesDetails();
                    while (reader.Read())
                    {
                        details.SalesId = reader.GetInt32(0);
                        details.CustomerName = reader[1].ToString();
                        details.NoOfUnits = reader.GetInt32(2);
                        details.NetAmount = Convert.ToDouble(reader.GetDecimal(3));
                        Amount = Convert.ToDouble(reader.GetDecimal(4));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            db.CloseConnection();
            return details;
        }

        public IEnumerable<Object> GetAllSaleDetails()
        {
            Object details;
            //SalesDetails details;
            SqlConnection connection = db.GetConnection();
            List<Object> salesDetailsList = null;
            string selectQuery = "select * from SalesDetails";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            command.CommandType = CommandType.Text;
            try
            {
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    salesDetailsList = new List<object>();
                    while (reader.Read())
                    {
                        details = new { SalesId = reader.GetInt32(0), CustomerName = reader[1].ToString(), NoOfUnits = reader.GetInt32(2), NetAmount = Convert.ToDouble(reader.GetDecimal(3)), Amount = Convert.ToDouble(reader.GetDecimal(4)) };
                        salesDetailsList.Add(details);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally { 
            db.CloseConnection();
            }
            return salesDetailsList;
        }
        public int UpdateNoOfUnitsOfCustomer(int id, int noOfUnits)
        {
            SalesDetails s = GetSalesDetailsById(id);
            s.NoOfUnits = noOfUnits;
            CalculateNetAmount(s);
            DBHandler db = new DBHandler();
            SqlConnection connection = db.GetConnection();
            string insertQuery = $"update SalesDetails set No_of_units={noOfUnits},Cost_after_discount={s.NetAmount},Total_amount={Amount} where sales_id={id}";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            try
            {
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public int UpdateNameOfCustomer(int id, string name)
        {
            SalesDetails s = GetSalesDetailsById(id);
            s.CustomerName = name;
            DBHandler db = new DBHandler();
            SqlConnection connection = db.GetConnection();
            string insertQuery = $"update SalesDetails set Customer_name='{name}' where sales_id={id}";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            try
            {
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public int DeleteSalesDetails(int id)
        {
            DBHandler db = new DBHandler();
            SqlConnection connection = db.GetConnection();
            string insertQuery = $"delete from SalesDetails where sales_id={id}";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            try
            {
                int res = command.ExecuteNonQuery();
                return res;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public int GetId()
        {
            DBHandler db = new DBHandler();
            SqlConnection connection = db.GetConnection();
            string query = "select max(sales_id) from salesDetails";
            SqlCommand command = new SqlCommand(query, connection);
            int id = 0;
            try
            {
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            db.CloseConnection();
            return id;
        }
    }
}
