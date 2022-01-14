using System;
using System.Collections.Generic;

#nullable disable

namespace Assessment_App.Models
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string CategorySerial { get; set; }
        public string CategoryLevel { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
