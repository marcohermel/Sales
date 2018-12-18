using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SalesMVC.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<vwCustomer> Customers { get; set; }
        public string Name { get; set; }
        [Display(Name = "Gender")]
        public int? GenderId { get; set; }
        [Display(Name = "City")]
        public int? CityId { get; set; }
        [Display(Name = "Region")]
        public int? RegionId { get; set; }
        [Display(Name = "Classification")]
        public int? ClassificationId { get; set; }
        [Display(Name = "Last Purchase")]
        public DateTime? DateStart { get; set; }
        [Display(Name = "Until")]
        public DateTime? DateFinish { get; set; }
        [Display(Name = "Seller")]
        public int? UserSysId { get; set; }
    }
}
