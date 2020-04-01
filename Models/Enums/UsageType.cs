using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public enum UsageType
    {
        [Display(Name = "قرص")]
        Tablet,
        [Display(Name = "کپسول")]
        Capsules,
        [Display(Name = "شربت")]
        Liquid,
        [Display(Name = "پماد")]
        Topical,
        [Display(Name = "شیاف")]
        Suppositorie,
        [Display(Name = "قطره")]
        Drop,
        [Display(Name = "اسپری")]
        Inhaler,
        [Display(Name = "آمپول")]
        Injection,
        [Display(Name = "کاشتنی")]
        Implant,
        [Display(Name = "وصله‌ای")]
        Patch,
        [Display(Name = "زیرزبانی")]
        Sublingual
    }
}
