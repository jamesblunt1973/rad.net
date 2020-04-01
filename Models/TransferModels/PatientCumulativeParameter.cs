using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class PatientCumulativeParameter
    {
        public int? PharmacyId { get; set; }
        public int? MedicineId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
