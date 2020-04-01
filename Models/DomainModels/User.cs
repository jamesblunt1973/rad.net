using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(1000)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(1000)]
        public byte[] PasswordSalt { get; set; }
    }
}
