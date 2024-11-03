using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;



namespace BUS
{
    public class BUS_Employee
    {
        public int AddEmployee(Employee emp)
        {   
            DAL_Employee dalEmp = new DAL_Employee();
            if (dalEmp.CheckExitNationalID(emp.NationID) == true && dalEmp.CheckExitPhone(emp.TeleNumber) == true)
                return 4;   // EXITED PHONE AND NAITONALID
            else
            {
                if (dalEmp.CheckExitNationalID(emp.NationID) == true)
                    return 3;   // EXITED NATIONALID
                if (dalEmp.CheckExitPhone(emp.TeleNumber) == true)
                    return 2;   // EXITED PHONE
                if (dalEmp.AddEmployee(emp) == true)
                    return 1;   // ADD SUCCESS
                else
                    return 0;
            }
        }



        public int UpdateEmployee(Employee emp)
        {
            DAL_Employee dalEmp = new DAL_Employee();
            if (dalEmp.CheckExitEmployee(emp.EmployeeID) == true)
            {
                if (dalEmp.UpdateEmployee(emp) == true)
                    return 1;
                else
                    return 0;
            }
            else
                return 2;

        }

        public int DeleteEmployee(int emp)
        {
            DAL_Employee dalEmp = new DAL_Employee();
            if (dalEmp.CheckExitEmployee(emp) == false)
                return 2;
            else
            {
                if (dalEmp.DeleteEmployee(emp) == true)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
