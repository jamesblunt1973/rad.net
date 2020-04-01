using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class RawData
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime ReceivedDate { get; set; }

        [Required]
        public string Json { get; set; }

        [Required]
        public long TrackCode { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public bool IsCanceled { get; set; }

        [Required]
        public int PharmacyId { get; set; }

        public int? PrescriptionId { get; set; }

        // Navigation Properties
        public Pharmacy Pharmacy { get; set; }
        public Prescription Prescription { get; set; }

    }
}
