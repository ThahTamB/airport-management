using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_Plane
    {
        DAL_Plane planes = new DAL_Plane();

        public List<Plane> GetListPlanes() => planes.LoadPlanes();

        public bool AddPlane(Plane plane)
        {
            if (!planes.checkValidPlane(plane.Registration))
            {
                return planes.AddPlane(plane.Model, plane.Registration, plane.TotalSeat, plane.Manufacturer, plane.State);
            }
            else
            {
                throw new Exception("Invalid plane registration detected!!!");
            }
        }

        public bool UpdatePlane(Plane plane)
        {
            if (planes.checkValidPlane(plane.Registration))
            {
                return planes.UpdatePlane(plane.PlaneID, plane.Model, plane.Registration, plane.TotalSeat,
                    plane.Manufacturer, plane.State);
            }
            else
            {
                throw new Exception("Invalid plane registration detected!!!");
            }
        }

        public bool RemovePlane(Plane plane)
        {
            if (planes.checkValidPlane(plane.Registration))
            {
                return planes.RemovePlane(plane.PlaneID);
            }
            else
            {
                throw new Exception("Invalid plane registration detected!!!");
            }
        }
    }
}