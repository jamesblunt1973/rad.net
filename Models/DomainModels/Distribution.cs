using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Distribution
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public int? PrescriptionId { get; set; }

        public int MedicineId { get; set; }

        public int PharmacyId { get; set; }

        public double Amount { get; set; }

        public double Stock { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime DateTime { get; set; }

        // Navigation Properties
        public Company Company { get; set; }
        public Medicine Medicine { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public Prescription Prescription { get; set; }
    }
}
