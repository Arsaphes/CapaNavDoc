using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class EquipmentBusinessLayer
    {
        public List<Equipment> GetEquipments()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.Equipments.ToList();
        }

        public Equipment InsertEquipment(Equipment equipment)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Equipments.Add(equipment);
            dal.SaveChanges();
            return equipment;
        }

        public Equipment UpdateEquipment(Equipment equipment)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(equipment).State = EntityState.Modified;
            dal.SaveChanges();
            return equipment;
        }

        public void DeleteEquipment(Equipment equipment)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(equipment).State = EntityState.Deleted;
            dal.SaveChanges();
        }


        public void DeleteEquipment(int id)
        {
            DeleteEquipment(GetEquipment(id));
        }

        public Equipment GetEquipment(int id)
        {
            return GetEquipments().FirstOrDefault(u => u.Id == id);
        }
    }
}