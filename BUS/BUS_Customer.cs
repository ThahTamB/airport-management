using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_Customer
    {
        DAL_Customer dalCustomer = new DAL_Customer();

        public List<Customer> getCustomerList() => dalCustomer.GetCustomerList();

        public bool addCustomer(Customer customer)
        {
            if (!dalCustomer.checkExistCustomer(-1, customer.NationalID, customer.TeleNumber))
            {
                return dalCustomer.AddCustomer(customer);
            }
            else
            {
                throw new Exception("Customer has existed");
            }
        }

        public bool updateCustomer(Customer updated_customer)
        {
            if (!dalCustomer.checkExistCustomer(updated_customer.CustomerID, updated_customer.NationalID, updated_customer.TeleNumber))
            {
                return dalCustomer.UpdateCustomer(updated_customer);
            }
            else
            {
                throw new Exception("Customer has existed");
            }
        }

        public bool deleteCustomer(int customer_id) => dalCustomer.DeleteCustomer(customer_id);
    }
}
