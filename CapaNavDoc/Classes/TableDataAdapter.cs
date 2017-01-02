using System.Collections.Generic;
using System.Linq;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Classes
{
	public class TableDataAdapter
	{
	    public static List<User> SearchInUsers(string str)
	    {
	        BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());
	        if (string.IsNullOrEmpty(str)) return bl.GetList();
	        return bl.GetList().Where(u =>
	            u.FirstName.Contains(str) ||
	            u.LastName.Contains(str) ||
	            u.UserName.Contains(str) ||
	            u.Password.Contains(str)).ToList();
	    }

        public static List<Action> SearchInActions(string str)
        {
            BusinessLayer<Action> bl = new BusinessLayer<Action>(new CapaNavDocDal());
            if (string.IsNullOrEmpty(str)) return bl.GetList();
            return bl.GetList().Where(a =>
                a.Description.Contains(str)).ToList();
        }

        public static List<Center> SearchInCenters(string str)
        {
            BusinessLayer<Center> bl = new BusinessLayer<Center>(new CapaNavDocDal());
            if (string.IsNullOrEmpty(str)) return bl.GetList();
            return bl.GetList().Where(c =>
                c.Name.Contains(str)).ToList();
        }

        public static List<Equipment> SearchInEquipments(string str)
        {
            BusinessLayer<Equipment> bl = new BusinessLayer<Equipment>(new CapaNavDocDal());
            if (string.IsNullOrEmpty(str)) return bl.GetList();
            return bl.GetList().Where(e =>
                e.Name.Contains(str) ||
                e.ActivityField.Contains(str) ||
                e.Ata.ToString().Contains(str) ||
                e.DocumentsPartNumber.Contains(str) ||
                e.DocumentsReferences.Contains(str) ||
                e.Manufacturer.Contains(str) ||
                e.MechanicsGroup.Contains(str) ||
                e.PartNumber.Contains(str) ||
                e.Type.Contains(str)).ToList();
        }
    }
}