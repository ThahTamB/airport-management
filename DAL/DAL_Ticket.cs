using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Ticket
    {
        public List<Bill_Detail> LoadBills()
        {
            var list = new List<Bill_Detail>();
            using (var db = new AirportManager())
                list = db.Bill_Detail.ToList();
            return list;
        }

        public bool AddTicket(Bill_Detail tick)
        {
            using (var db = new AirportManager())
            {
                db.Bill_Detail.Add(tick);
                return db.SaveChanges() > 0;
            }
        }

        public bool RemoveTicket(int id)
        {
            using (var db = new AirportManager())
            {
                var targetedBill = (from d in db.Bill_Detail where d.BillID == id select d).SingleOrDefault();
                db.Bill_Detail.Remove(targetedBill);
                return db.SaveChanges() > 0;
            }
        }
    }
}
