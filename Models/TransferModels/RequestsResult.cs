using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class RequestsResult
    {
		public bool IsCanceled { get; set; }
		public long TrackCode { get; set; }
		public DateTime ReceivedDate { get; set; }
		public string PharmacyName { get; set; }
		public string PharmacyCode { get; set; }
	}
}
