using System;
using System.Collections.Generic;

namespace OSlBackendWebService.Models
{
    public partial class Checkings
    {
        public int TranszactionId { get; set; }
        public int? SupId { get; set; }
        public string Station { get; set; }
        public string EmpId { get; set; }
        public double? Xcord { get; set; }
        public double? Ycoord { get; set; }
        public bool? CheckStatus { get; set; }
        public DateTime? CheckDate { get; set; }

        public Supervisors Sup { get; set; }
    }
}
