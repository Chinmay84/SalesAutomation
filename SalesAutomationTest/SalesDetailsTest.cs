using NUnit.Framework;
using SalesAutomation.DAL;
using SalesAutomation.model;
using System.Collections.Generic;
using SalesAutomation.BusinessLayer;
using System;

namespace SalesAutomationTest
{
    public class Tests
    {
        SalesDetails sales;
        List<SalesDetails> salesList;
        SmartAppliances dal;
        SmartAppliancesUtility util;
        List<Object> list;

        [SetUp]
        public void CreateObject()
        {

            sales = new SalesDetails();
            salesList = new List<SalesDetails>();
            util = new SmartAppliancesUtility();
            dal = new SmartAppliances();
        }

        [Test, Order(1)]
        public void TestAddSalesDetailsBeforeAddingObject()
        {
            int count = salesList.Count;
            Assert.AreEqual(0, count, "Customer list should be empty before adding object");
        }

        [Test, Order(2)]
        public void TestGetAllSalesDetails()
        {
            IEnumerable<Object> d = dal.GetAllSaleDetails();
            list = new List<Object>(d);
            Assert.AreEqual(4, list.Count, "Number of records are different");
        }

        [Test, Order(3)]
        public void TestGetSalesDetailsByID()
        {
            int id = 2;
            sales = dal.GetSalesDetailsById(id);
            //CollectionAssert.Contains(list, new { SalesId = sales.SalesId, CustomerName = sales.CustomerName, NoOfUnits = sales.NoOfUnits, NetAmount = sales.NetAmount, Amount = SmartAppliances.Amount }, "No record Found");
            Assert.AreEqual(id, sales.SalesId, "No record found");
        }

        [Test, Order(4)]
        public void TestAddSalesDetails()
        {
            dal = new SmartAppliances();
            sales.CustomerName = "Bob";
            sales.NoOfUnits = 28;
            int res = dal.AddSalesDetails(sales);
            Assert.AreEqual(1, res, "Number of rows are not same");
        }

        [Test, Order(5)]
        public void TestUpdateNoOfUnitsOfcustomer()
        {
            int id = 6;
            int res1 = sales.NoOfUnits;
            int noOfUnits = 14;
            dal.UpdateNoOfUnitsOfCustomer(id, noOfUnits);
            sales = dal.GetSalesDetailsById(id);
            int res2 = sales.NoOfUnits;
            Assert.AreNotEqual(res1, res2, "Updation failed");
        }

        [Test, Order(6)]

        public void TestUpdateNameOfcustomer()
        {
            int id = 6;
            string res1 = sales.CustomerName;
            string name = "Alice";
            dal.UpdateNameOfCustomer(id, name);
            sales = dal.GetSalesDetailsById(id);
            string res2 = sales.CustomerName;
            Assert.AreNotEqual(res1, res2, "Updation failed");
        }

        [Test, Order(7)]
        public void TestDeleteSalesDetailsByID()
        {
            int id = 6;
            IEnumerable<Object> d = dal.GetAllSaleDetails();
            list = new List<Object>(d);
            dal.DeleteSalesDetails(id);
            IEnumerable<Object> d1 = dal.GetAllSaleDetails();
            List<object> list1 = new List<Object>(d1);
            Assert.AreNotEqual(list.Count, list1.Count, "record Deleted");
        }  
    }
}