using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rad.net.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string GenericCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string IrcCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string FaName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string EnName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string BrandFaName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string BrandEnName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Unit { get; set; }

        public UsageType? UsageType { get; set; }

        public int? ProducerId { get; set; }

        public int? DistributorId { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(50)]
        public string Dosage { get; set; }

        [Required]
        public int UsagePeriod { get; set; } // Days

        [Required]
        public double MaximumUsageAmount { get; set; }

        // Navigation Properties
        public Company Productcer { get; set; }
        public Company Distributor { get; set; }
        public IEnumerable<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public IEnumerable<Distribution> Distributions { get; set; }
    }
}
