using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class ActionBusinessLayer
    {
        public List<Action> GetActions()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.Actions.ToList();
        }

        public Action InsertAction(Action action)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Actions.Add(action);
            dal.SaveChanges();
            return action;
        }

        public Action UpdateAction(Action action)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(action).State = EntityState.Modified;
            dal.SaveChanges();
            return action;
        }

        public void DeleteAction(Action action)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(action).State = EntityState.Deleted;
            dal.SaveChanges();
        }


        public void DeleteAction(int id)
        {
            DeleteAction(GetAction(id));
        }
        
        public Action GetAction(int id)
        {
            return GetActions().FirstOrDefault(u => u.Id == id);
        }
    }
}