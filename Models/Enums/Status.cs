using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public enum Status
    {
        [Display(Name = "دارو موجود نیست")]
        NoTracked,
        [Display(Name = "تایید نشده")]
        Unverified,
        [Display(Name = "تایید شده")] 
        Verified
    }
}
