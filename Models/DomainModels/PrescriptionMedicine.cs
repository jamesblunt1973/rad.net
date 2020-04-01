using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class PrescriptionMedicine
    {
        public int Id { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        public int PrescriptionId { get; set; }

        [Required]
        public double Amount { get; set; }

        // Navigation Properties
        public Medicine Medicine { get; set; }
        public Prescription Prescription { get; set; }
    }
}
