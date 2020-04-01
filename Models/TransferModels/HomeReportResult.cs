using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models.TransferModels
{
    public class HomeReportResult
    {
        public int Medicines { get; set; }
        public int Pharmacies { get; set; }
        public int Patients { get; set; }
        public int Physicians { get; set; }
        public int Received { get; set; }
        public int Prescriptions { get; set; }
        public int Verified { get; set; }
    }
}
