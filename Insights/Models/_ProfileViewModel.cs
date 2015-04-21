using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Models
{
    public class _ProfileViewModel
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string businessName { get; set; }

        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phoneNumber { get; set; }
    }
}