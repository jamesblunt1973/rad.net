using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Setting
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string Key { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Value { get; set; }
    }
}
