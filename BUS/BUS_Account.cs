using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BUS
{
    public class BUS_Account
    {
        DAL_Account dalAccount = new DAL_Account();

        public int AddAccount(Account acc)
        {
            DAL_Account dalAcc = new DAL_Account();
            if (dalAcc.CheckExitUsername(acc.Username) == true)
                return 2;   //Exited username
            else
            {
                if (dalAcc.AddAccount(acc) == true)
                    return 1;   // ADD SUCCESS
                else
                    return 0; // ADD FAIL
            }
        }

        public int UpdateAccount(Account acc) {
           
            return 0;
        }
        public int DeleteAccount(int employeeID)
        {
            DAL_Account dalAcc = new DAL_Account();
            if (dalAcc.DeleteAccount(employeeID) == true)
                return 1;
            else
                return 0;
        }

        public Account isAutheticated(string username, string password) { 
            return dalAccount.LoginTo(username, password);
        }
    }
}
