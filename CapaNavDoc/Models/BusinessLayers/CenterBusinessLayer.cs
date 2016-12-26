using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Extensions;

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



        public List<User> GetCenterUsers(int centerId)
        {
            Center center = GetCenter(centerId);
            if(center.UserList == null) return new List<User>();

            string[] ids = center.UserList.Split(';');
            UserBusinessLayer bl = new UserBusinessLayer();
            List<User> users = bl.GetUsers().Where(u => ids.Contains(u.Id.ToString())).ToList();
            
            return users;
        }

        public void AddCenterUser(Center center, int userId)
        {
            center.UserList = center.UserList.AddId(userId);
        }

        public void RemoveCenterUser(Center center, int userId)
        {
            center.UserList = center.UserList.RemoveId(userId);
        }
    }
}
