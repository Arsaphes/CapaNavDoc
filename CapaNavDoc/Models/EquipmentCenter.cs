using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaNavDoc.Models
{
    public class EquipmentCenter
    {
        [Key]
        public int Id { get; set; }

        public Center Center { get; set; }
        public List<Action> Actions { get; set; }  
    }
}