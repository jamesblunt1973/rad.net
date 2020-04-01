using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models.TransferModels
{
    public class DistributionDatailsReult
    {
        public DateTime DateTime { get; set; }
        public double Amount { get; set; }
        public int? PrescriptionId { get; set; }
        public double Stock { get; set; }
    }
}
