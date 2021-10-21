using SalesAutomation.DAL;
using SalesAutomation.model;
using SalesAutomation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAutomation.BusinessLayer
{
    public class SmartAppliancesUtility
    {
        SmartAppliances appliances = new SmartAppliances();

        public int InsertSalesDetails(SalesDetails s)
        {
            return appliances.AddSalesDetails(s);
        }

        public SalesDetails GetDetailsByid(int id)
        {
            return appliances.GetSalesDetailsById(id);
        }

        public IEnumerable<object> GetAllDetails()
        {
            return appliances.GetAllSaleDetails();
        }

        public int UpdateNumberOfUnits(int id,int number)
        {
            return appliances.UpdateNoOfUnitsOfCustomer(id, number);
        }

        public int UpdateNameOfSalesCustomer(int id, string name)
        {
            return appliances.UpdateNameOfCustomer(id, name);
        }

        public int GetCustomerId()
        {
            return appliances.GetId();
        }
        public int DeleteCustomerRecord(int id)
        {
            return appliances.DeleteSalesDetails(id);
        }
    }
}
