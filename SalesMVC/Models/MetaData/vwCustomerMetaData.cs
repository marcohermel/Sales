﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesMVC.Models
{
    public class vwCustomerMetaData
    {
        public int Id { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public int RegionId { get; set; }
        public int ClassificationId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string RegionCity { get; set; }
        public string Region { get; set; }
        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }
        public string Classification { get; set; }
        [Display(Name = "Saller")]
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public bool isAdmin { get; set; }
    }
    [ModelMetadataType(typeof(vwCustomerMetaData))]
    public partial class vwCustomer {
    }
}
