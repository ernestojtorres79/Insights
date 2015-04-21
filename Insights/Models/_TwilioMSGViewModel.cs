using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Models
{
    public class _TwilioMSGViewModel
    {
        public int Id { get; set; }
        public string Sid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public string ACCOUNT_SID { get; set; }
        public string AUTH_TOKEN { get; set; }
        public string UserId { get; set; }
    }
}