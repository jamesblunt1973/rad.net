using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models.TransferModels
{
    public class VerificationsResult
    {
        public string PatientName { get; set; }
        public string PatientNationalCode { get; set; }
        public int MedicineId { get; set; }
        public string Medicine { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyCode { get; set; }
        public long TrackCode { get; set; }
        public double Amount { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}
