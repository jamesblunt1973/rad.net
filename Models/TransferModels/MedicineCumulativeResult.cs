using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class MedicineCumulativeResult
    {
        public int MedicineId { get; set; }
        public string Medicine { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyCode { get; set; }
        public double Amount { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
