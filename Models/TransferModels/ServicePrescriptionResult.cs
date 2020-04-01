using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class ServicePrescriptionResult
    {
        public int MessageCode { get; set; }
        public string Message { get; set; }
        public long TrackNo { get; set; }
        public string IllegalDrug { get; set; }
    }
}
