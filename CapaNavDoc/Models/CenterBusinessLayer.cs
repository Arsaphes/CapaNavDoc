using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models
{
    public class CenterBusinessLayer
    {
        public List<Center> GetCenters()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.Centers.ToList();
        }

        public Center InserCenter(Center center)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Centers.Add(center);
            dal.SaveChanges();
            return center;
        }

        public Center GetCenter(int id)
        {
            return GetCenters().FirstOrDefault(c => c.Id == id);
        }
    }
}
