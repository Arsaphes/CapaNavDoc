using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class EquipmentCenterBusinessLayer
    {
        public List<EquipmentCenter> GetEquipmentCenters()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.EquipmentCenters.ToList();
        }

        public EquipmentCenter InsertEquipmentCenter(EquipmentCenter equipmentCenter)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.EquipmentCenters.Add(equipmentCenter);
            dal.SaveChanges();
            return equipmentCenter;
        }

        public EquipmentCenter UpdateEquipmentCenter(EquipmentCenter equipmentCenter)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(equipmentCenter).State = EntityState.Modified;
            dal.SaveChanges();
            return equipmentCenter;
        }

        public void DeleteEquipmentCenter(EquipmentCenter equipmentCenter)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(equipmentCenter).State = EntityState.Deleted;
            dal.SaveChanges();
        }


        public void DeleteEquipmentCenter(int id)
        {
            DeleteEquipmentCenter(GetEquipmentCenter(id));
        }

        public EquipmentCenter GetEquipmentCenter(int id)
        {
            return GetEquipmentCenters().FirstOrDefault(u => u.Id == id);
        }
    }
}