using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Account
    {
        public bool AddAccount(Account account)
        {
            using (var db = new AirportManager())
            {
                db.Accounts.Add(account);

                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }

        public bool CheckExitUsername(String username)
        {
            using (var db = new AirportManager())
            {
                var query = from acc in db.Accounts
                            select acc.Username;
                foreach (var id in query)
                {
                    if (id == username)
                        return true;
                }
                return false;
            }
        }

       public int takeEmployeeIDbyEmployeeNationalID(String nationalID)
        {
            using (var db = new AirportManager())
            {
                var query = from emp in db.Employees
                            where emp.NationID == nationalID
                            select emp;
                foreach (var empid in query)
                {
                    if (empid.NationID == nationalID)
                        return empid.EmployeeID ;
                }
                return 0;
            }
        }

        public bool DeleteAccount(int employeeID)
        {
            using (AirportManager db = new AirportManager())
            {
                //var entry = db.Entry(account);
                //if (entry.State == EntityState.Detached)
                //    db.Accounts.Attach(account);
                //db.Accounts.Remove(account);

                //if (db.SaveChanges() > 0)
                //    return true;
                //else
                //    return false;
                var query = (from acc in db.Accounts
                            where acc.EmployeeID == employeeID
                            select acc).Single();
                
               
                    if (query.EmployeeID == employeeID)
                    {
                        db.Accounts.Remove(query);
                        db.SaveChanges();
                        return true;
                    }
               
                return false;
            }
        }
         
        public bool UpdateAccount(Account account)
        {
            using (AirportManager db = new AirportManager())
            {
                {
                    db.Entry(account).State = EntityState.Modified;

                    if (db.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public Account LoginTo(string username, string password)
        {
            Account acc = new Account();
            using(var db = new AirportManager()) { 
                acc = db.Accounts.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).Include("Employee").FirstOrDefault();
            }
            return acc;
        }
    }
}
