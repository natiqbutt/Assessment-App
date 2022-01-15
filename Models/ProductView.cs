using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_App.Models
{
    public class ProductView
    {
        public int Id { get; set; }
        public int? ProductCategory { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string ProductCode { get; set; }
        public string ProductSerial { get; set; }
        public double ProductQuantity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
    }
}
