using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string Name { get; set; }
        
        // Navigation Properties
        public IEnumerable<Medicine> ProducedMedicines { get; set; }
        public IEnumerable<Medicine> DistributedMedicines { get; set; }
        public IEnumerable<Distribution> Distributions { get; set; }
    }
}
