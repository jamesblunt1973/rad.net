using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Physician
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Name { get; set; }

        public int? SpecialityId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } // کد نظام پزشکی Medical System Code

        // Navigation Properties
        public Speciality Speciality { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
