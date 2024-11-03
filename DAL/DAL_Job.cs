using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Job
    {
        public bool AddJob(Job job)
        {
            using (var db = new AirportManager())
            {
                db.Jobs.Add(job);

                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool CheckExitEmployee(int employeeID)
        {
            using (var db = new AirportManager())
            {
                var query = from emp in db.Employees
                            where emp.EmployeeID == employeeID
                            select emp;
                foreach (var id in query)
                {
                    if (id.EmployeeID == employeeID)
                        return true;
                }
                return false;
            }
        }
        public bool CheckExitFlight(int flightID)
        {
            using (var db = new AirportManager())
            {
                var query = from fly in db.Flights
                            where fly.FlightID == flightID
                            select fly;
                foreach (var id in query)
                {
                    if (id.FlightID == flightID)
                        return true;
                }
                return false;
            }
        }

        public bool UpdateJob(Job job)
        {
            using (AirportManager db = new AirportManager())
            {
                {
                    db.Entry(job).State = EntityState.Modified;

                    if (db.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public bool CheckExitJob(int job)
        {
            using (AirportManager db = new AirportManager())
            {
                var query = from j in db.Jobs
                            select j.JobID;
                foreach (var e in query)
                {
                    if (e == job)
                        return true;
                }
            }
            return false;
        }

        public bool DeleteJob(Job job)
        {

            using (AirportManager db = new AirportManager())
            {
                var query = (from j in db.Jobs
                             where j.JobID == job.JobID
                             select j).Single();

                if (query.JobID == job.JobID)
                {
                    db.Jobs.Remove(query);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}
