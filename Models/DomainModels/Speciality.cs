using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Speciality
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Code { get; set; }

        // Navigation Properties
        public IEnumerable<Physician> Physicians { get; set; }
    }
}
