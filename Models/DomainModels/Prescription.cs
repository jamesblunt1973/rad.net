using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public int PhysicianId { get; set; }
        public int PharmacyId { get; set; }
        public int PatientId { get; set; }
        public int RawDataId { get; set; }

        // Navigation Properties
        public Pharmacy Pharmacy { get; set; }
        public Physician Physician { get; set; }
        public Patient Patient { get; set; }
        public IEnumerable<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public List<Distribution> Distributions { get; set; }
        public RawData RawData { get; set; }
    }
}
