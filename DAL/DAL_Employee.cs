using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class DAL_Employee
    {
        public bool AddEmployee(Employee employee)
        {
            using ( var db = new AirportManager())
            {
                db.Employees.Add(employee);
               
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            
        }
        public bool CheckExitNationalID(String nationalID)
        {
            using ( var db = new AirportManager())
            {
                var query = from emp in db.Employees
                            select emp.NationID;
                foreach ( var id in query )
                {
                    if ( id == nationalID ) 
                        return true;

                }
                return false;
            }    
        }

        public bool CheckExitPhone(String phone)
        {
            using (var db = new AirportManager())
            {
                var query = from emp in db.Employees
                            select emp.TeleNumber;
                foreach (var id in query)
                {
                    if (id == phone)
                        return true;
                }
                return false;
            }
        }
        
        public bool UpdateEmployee(Employee employee)
        {
            using (AirportManager db = new AirportManager())
            {
                {
                    db.Entry(employee).State = EntityState.Modified;
                
                   if (db.SaveChanges() >0) 
                        return true;
                   else
                        return false;
                }
            }
        }

        public bool DeleteEmployee(int employeeID)
        {
            
            using (AirportManager db = new AirportManager())
            {
                var query = (from emp in db.Employees
                            where emp.EmployeeID == employeeID
                            select emp).Single();
               
                    if (query.EmployeeID == employeeID)
                    {
                        db.Employees.Remove(query);
                        db.SaveChanges();
                        return true;
                    }
                
                return false;
            }
        }
     
        public bool CheckExitEmployee(int employeeID)
        {
            using (AirportManager db = new AirportManager())
            {
                var query = from emp in db.Employees
                            where emp.EmployeeID == employeeID
                            select emp;
                foreach ( var e in query)
                {
                    if (e.EmployeeID == employeeID)
                        return true;
                }    

            }
            return false;
        }
    }

}
