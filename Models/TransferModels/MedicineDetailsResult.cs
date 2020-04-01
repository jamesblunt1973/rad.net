using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class MedicineDetailsResult
    {
		public int Id { get; set; }
		public DateTime DeliveryDate { get; set; }
		public DateTime PrescriptionDate { get; set; }
		public DateTime SubmitDate { get; set; }
		public string PharmacyName { get; set; }
		public string PharmacyCode { get; set; }
		public string PhysicianName { get; set; }
		public string PhysicianCode { get; set; }
		public string PatientName { get; set; }
		public string PatientNationalCode { get; set; }
		public string PatientCell { get; set; }
		public string PatientTell { get; set; }
		public double Amount { get; set; }
		public string Unit { get; set; }
	}
}
