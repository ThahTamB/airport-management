using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BUS
{
    public class BUS_Ticket
    {
        private static DAL_Ticket ticketbill = new DAL_Ticket();

        public List<Bill_Detail> GetListBills() => ticketbill.LoadBills();
        public bool AddBillService(Bill_Detail tick)
        {
            return ticketbill.AddTicket(tick);
        }

        public static bool DeleteBillService(Bill_Detail tick)
        {
            return ticketbill.RemoveTicket(tick.BillID);
        }
    }
}
