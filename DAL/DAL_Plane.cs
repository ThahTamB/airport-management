using DTO;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL
{
    public class DAL_Plane
    {
        public List<Plane> LoadPlanes()
        {
            var list = new List<Plane>();

            using (var db = new AirportManager())
                list = db.Planes.ToList();


            return list;
        }

        public bool AddPlane(string model, string registration, int totalSeat, string manufacturer, int state)
        {
            using (var db = new AirportManager())
            {
                db.Planes
                    .Add(new Plane() { Model = model, Registration = registration, TotalSeat = totalSeat, Manufacturer = manufacturer, State = state });
                return db.SaveChanges() > 0;
            }
        }

        public bool UpdatePlane(int id, string model, string registration, int totalSeat, string manufacturer, int state)
        {
            using (var db = new AirportManager())
            {
                var pickedPlane = (from d in db.Planes where d.PlaneID == id select d).SingleOrDefault();

                if (pickedPlane != null)
                {
                    pickedPlane.Model = model;
                    pickedPlane.Registration = registration;
                    pickedPlane.TotalSeat = totalSeat;
                    pickedPlane.Manufacturer = manufacturer;
                    pickedPlane.State = state;
                    db.Planes.AddOrUpdate(pickedPlane);
                }

                return db.SaveChanges() > 0;
            }
        }

        public bool RemovePlane(int id)
        {
            using (var db = new AirportManager())
            {
                var pickedPlane = (from d in db.Planes where d.PlaneID == id select d).SingleOrDefault();
                db.Planes.Remove(pickedPlane);
                return db.SaveChanges() > 0;
            }
        }

        public bool checkValidPlane(string registration)
        {
            using (var db = new AirportManager())
            {
                var pickedPlane = (from d in db.Planes where d.Registration == registration select d).FirstOrDefault();
                return pickedPlane != null;
            }
        }
    }
}