using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesAutomation.model
{
    public class SalesDetails
    {
         int salesId;
         string customerName;
         int noOfUnits;
         double netAmount;
        
        public SalesDetails()
        {
        }

        public SalesDetails(string customerName, int noOfUnits)
        {
            CustomerName = customerName;
            NoOfUnits = noOfUnits;
        }

        public int SalesId { get => salesId; set => salesId = value; }
       

        public  int NoOfUnits { get=>noOfUnits ; set=>noOfUnits =value; }
        public double NetAmount { get => netAmount; set => netAmount = value; }
        public string CustomerName { get => customerName; set => customerName = value; }

        public override string ToString()
        {
            return $"Sales ID:{SalesId}\n Customer Name:{CustomerName} \n Number of Units:{NoOfUnits} \n Discount Amount:{NetAmount}";
        } 
    }
}
