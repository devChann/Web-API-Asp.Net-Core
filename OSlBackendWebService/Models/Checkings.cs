using System;
using System.Collections.Generic;

namespace OSlBackendWebService.Models
{
    public partial class Checkings
    {
        public int TranszactionId { get; set; }
        public int SupId { get; set; }
        public string Station { get; set; }
        public string EmpId { get; set; }
        public double? Xcord { get; set; }
        public double? Ycoord { get; set; }
        public DateTime? Lit { get; set; }
        public string StringDate { get; set; }

        public Supervisors Sup { get; set; }
    }
}
