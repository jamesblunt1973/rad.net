using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string NationalCode { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? BirthDate { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(11)]
        public string Tell { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(11)]
        public string Cell { get; set; }

        public bool? Gender { get; set; }

        // Navigation Properties
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
