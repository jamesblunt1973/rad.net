using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class PatientCumulativeResult
    {
        public int MedicineId { get; set; }
        public string Medicine { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyCode { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurName { get; set; }
        public string PatientNationalCode { get; set; }
        public double Amount { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
