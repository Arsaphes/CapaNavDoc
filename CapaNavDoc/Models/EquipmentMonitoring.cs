using System;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class EquipmentMonitoring
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}