using System;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
	public class Equipment
	{
        [Key]
        public int Id { get; set; }

	    public string PartNumber { get; set; }
	    public string Manufacturer { get; set; }
	    public string Name { get; set; }
	    public string Type { get; set; }
	    public int Ata { get; set; }
	    public string ActivityField { get; set; }
	    public string MechanicsGroup { get; set; }
        public int MaintenanceDataId { get; set; }
        public string EquipmentCenterActionList { get; set; } // "n,m;" where 'n' is the center id, 'm' the action id, ',' the center/action separator, ';' the center/action group separator
	    public DateTime MonitoringDate { get; set; }
	    public int MonitoringUserId { get; set; }
	}
}