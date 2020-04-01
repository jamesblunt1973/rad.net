using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(11)]
        public string Tell { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(11)]
        public string Cell { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        [StringLength(1000)]
        public string Password { get; set; }

        // Navigation Properties
        public IEnumerable<Prescription> Prescriptions { get; set; }
        public IEnumerable<RawData> RawDatas { get; set; }
        public IEnumerable<Distribution> Distributions { get; set; }
    }
}
