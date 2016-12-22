using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class UserBusinessLayer
    {
        public List<User> GetUsers()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.Users.ToList();
        }

        public User InsertUser(User user)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Users.Add(user);
            dal.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(user).State = EntityState.Modified;
            dal.SaveChanges();
            return user;
        }

        public void DeleteUser(User user)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(user).State = EntityState.Deleted;
            dal.SaveChanges();
        }


        public void DeleteUser(int id)
        {
            DeleteUser(GetUser(id));
        }
        
        public User GetUser(string userName, string password)
        {
            return GetUsers().FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }

        public User GetUser(int id)
        {
            return GetUsers().FirstOrDefault(u => u.Id == id);
        }
    }
}
