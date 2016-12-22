using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CapaNavDoc.DataAccessLayer;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class EquipmentMonitoringBusinessLayer
    {
        public List<EquipmentMonitoring> GetEquipmentMonitorings()
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            return dal.EquipmentMonitorings.ToList();
        }

        public EquipmentMonitoring InsertEquipmentMonitoring(EquipmentMonitoring equipmentMonitoring)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.EquipmentMonitorings.Add(equipmentMonitoring);
            dal.SaveChanges();
            return equipmentMonitoring;
        }

        public EquipmentMonitoring UpdateEquipmentMonitoring(EquipmentMonitoring equipmentMonitoring)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(equipmentMonitoring).State = EntityState.Modified;
            dal.SaveChanges();
            return equipmentMonitoring;
        }

        public void DeleteEquipmentMonitoring(EquipmentMonitoring equipmentMonitoring)
        {
            CapaNavDocDal dal = new CapaNavDocDal();
            dal.Entry(equipmentMonitoring).State = EntityState.Deleted;
            dal.SaveChanges();
        }


        public void DeleteEquipmentMonitoring(int id)
        {
            DeleteEquipmentMonitoring(GetEquipmentMonitoring(id));
        }

        public EquipmentMonitoring GetEquipmentMonitoring(int id)
        {
            return GetEquipmentMonitorings().FirstOrDefault(u => u.Id == id);
        }
    }
}