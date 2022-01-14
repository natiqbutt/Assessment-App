using System;
using System.Collections.Generic;

#nullable disable

namespace Assessment_App.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? ProductCategory { get; set; }
        public string Brand { get; set; }
        public string ProductCode { get; set; }
        public string ProductSerial { get; set; }
        public int ProductQuantity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
