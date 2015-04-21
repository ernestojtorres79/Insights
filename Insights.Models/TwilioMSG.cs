using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class TwilioMSG : AuditObject
    {
        public int TwilioMSGId { get; set; }
        public string TwiilioMSGSid { get; set; }
        public string TwilioMSGBody { get; set; }
        public string TwilioMSGFrom { get; set; }
        public string TwilioMSGTo { get; set; }
        public DateTime TwilioMSGDate { get; set; }
    }
}
