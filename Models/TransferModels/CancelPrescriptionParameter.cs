using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class CancelPrescriptionParameter
    {
        // {"TrackNo": "1881795645",  "PharmacyCode": "2548"}
        public string TrackNo { get; set; }
        public string PharmacyCode { get; set; }
    }
}
