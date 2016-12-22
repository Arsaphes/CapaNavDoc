using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class CenterBusinessLayer
    {
        public List<Center> GetCenters()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.Centers.ToList();
        }

        public Center InsertCenter(Center center)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Centers.Add(center);
            dal.SaveChanges();
            return center;
        }

        public Center UpdateCenter(Center center)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(center).State = EntityState.Modified;
            dal.SaveChanges();
            return center;
        }

        public void DeleteCenter(Center center)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(center).State = EntityState.Deleted;
            dal.SaveChanges();
        }


        public void DeleteCenter(int id)
        {
            DeleteCenter(GetCenter(id));
        }
        
        public Center GetCenter(int id)
        {
            return GetCenters().FirstOrDefault(c => c.Id == id);
        }
    }
}
