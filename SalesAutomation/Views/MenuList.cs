using SalesAutomation.BusinessLayer;
using SalesAutomation.DAL;
using SalesAutomation.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
namespace SalesAutomation.Views
{
    public class MenuList
    {
        public int NoOfUnits { get; set; }
        public string CustomerName { get; set; }

        SalesDetails details = new SalesDetails();
        public void Menu()
        {
            SmartAppliancesUtility appliancesUtility = new SmartAppliancesUtility();
            bool flag = true;
            int choice;
            do
            {
                WriteLine("Enter your choice \n 1:Enter Sales Details \n 2:Get Sales Details by ID \n 3:Get all Sales details \n 4:Update number of units  \n 5:Update Customer name \n 6:Delete Sales Details By ID \nPress 0 to exit");
                try
                {
                    choice = Convert.ToInt32(ReadLine());
                    switch (choice)
                    {
                        case 0:
                            {
                                flag = false;
                                WriteLine("Thank You!!!");
                                break;
                            }
                        case 1:
                            {
                                try
                                {
                                    WriteLine("Enter your name");
                                    details.CustomerName = ReadLine();
                                    string name = details.CustomerName;
                                    if (ValidCustomerName(name))
                                    {
                                        WriteLine("Enter the number of units");
                                        details.NoOfUnits = Convert.ToInt32(ReadLine());
                                        appliancesUtility.InsertSalesDetails(details);
                                        int id = appliancesUtility.GetCustomerId();
                                        WriteLine("Your record has been Inserted succesfully and your id is " + id);
                                    }
                                    else
                                    {
                                        throw new Exception("Name should not contain any number or symbol and  it should not be empty");
                                    }
                                }
                                catch (FormatException)
                                {
                                    throw new FormatException("Enter the number of units,units cannot be empty and its value should be integer");
                                }

                                break;
                            }
                        case 2:
                            {
                                try
                                {
                                    WriteLine("Enter customer id:");
                                    int id = Convert.ToInt32(ReadLine());
                                    SalesDetails s = appliancesUtility.GetDetailsByid(id);
                                    if (s != null)
                                    {
                                        WriteLine(s + "\n" + "Total Amount:" + SmartAppliances.Amount);
                                    }
                                    else
                                    {
                                        WriteLine("No record found");
                                    }
                                }
                                catch (FormatException)
                                {
                                    throw new FormatException("Enter correct id,id cannot be character or empty");
                                }
                                break;
                            }
                        case 3:
                            {
                                if (appliancesUtility.GetAllDetails().Count() != 0)
                                {
                                    DisplayAll(appliancesUtility.GetAllDetails());
                                }
                                else
                                {
                                    int id = appliancesUtility.GetCustomerId();
                                    WriteLine("No record to display.Please insert ID below " + id);
                                }
                                break;
                            }
                        case 4:
                            {
                                try
                                {
                                    WriteLine("Enter the id of the Sales Details to update:");
                                    int id = Convert.ToInt32(ReadLine());
                                    SalesDetails s = appliancesUtility.GetDetailsByid(id);
                                    if (s != null)
                                    {
                                        WriteLine("Enter the number of units:");
                                        int noOfUnits = Convert.ToInt32(ReadLine());
                                        appliancesUtility.UpdateNumberOfUnits(id, noOfUnits);
                                        WriteLine("Your number of units updated succesfully");
                                    }
                                    else
                                    {
                                        WriteLine("No record to update");
                                    }
                                }
                                catch (FormatException)
                                {
                                    throw new FormatException("ID and number of units cannot be empty and its value should be integer");
                                }

                                break;
                            }
                        case 5:
                            {
                                try
                                {
                                    WriteLine("Enter the id of the Sales Details to update:");
                                    int id = Convert.ToInt32(ReadLine());
                                    SalesDetails s = appliancesUtility.GetDetailsByid(id);
                                    if (s != null)
                                    {
                                        WriteLine("Enter the name:");
                                        string name = ReadLine();
                                        if (ValidCustomerName(name))
                                        {
                                            appliancesUtility.UpdateNameOfSalesCustomer(id, name);
                                            WriteLine("Updated name successfully");
                                        }
                                        else
                                        {
                                            throw new Exception("Name should not contain any number or symbol and it should not be empty");
                                        }
                                    }
                                    else
                                    {
                                        WriteLine("No record to update");
                                    }
                                }
                                catch (FormatException)
                                {
                                    throw new FormatException("ID cannot be empty and its value should be integer");
                                }

                                break;
                            }
                        case 6:
                            {
                                try
                                {
                                    WriteLine("Enter the id of the record to delete:");
                                    int id = Convert.ToInt32(ReadLine());
                                    SalesDetails s = appliancesUtility.GetDetailsByid(id);
                                    if (s != null)
                                    {
                                        appliancesUtility.DeleteCustomerRecord(id);
                                        WriteLine("Deleted record successfully");
                                    }
                                    else
                                    {
                                        WriteLine("No record to update");
                                    }
                                }
                                catch (FormatException)
                                {
                                    throw new FormatException("ID cannot be empty and its value should be integer");
                                }

                                break;
                            }
                        default:
                            {
                                WriteLine("Enter a valid choice");
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                }
            } while (flag);

        }

        public static void DisplayAll(IEnumerable<Object> s)
        {
            IEnumerator<Object> details = s.GetEnumerator();
            while (details.MoveNext())
            {
                Console.WriteLine(Convert.ToString(details.Current).Replace('{', ' ').Replace('}', ' '));
            }
        }

        public void GetCustomerName()
        {
            WriteLine("Enter your name");

        }
        public static bool ValidCustomerName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }
    }
}
