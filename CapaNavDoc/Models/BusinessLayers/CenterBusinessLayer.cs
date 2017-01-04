using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.Extensions;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class CenterBusinessLayer:BusinessLayer<Center>
    {
        public CenterBusinessLayer(DbContext context) : base(context)
        {
        }

        public List<User> GetCenterUsers(int centerId)
        {
            Center center = Get(centerId);
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
