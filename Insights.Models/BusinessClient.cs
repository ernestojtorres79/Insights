using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class BusinessClient : AuditObject
    {
        public int BusinessClientId { get; set; }
        public bool BusinessActive { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessContactName { get; set; }
        public string BusinessContactPhone { get; set; }
        public string BusinessContactEmail { get; set; }
        public string BusinessContactName2 { get; set; }
        public string BusinessContactPhone2 { get; set; }
        public string BusinessContactEmail2 { get; set; }
        public List<Campaign> Campaigns { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
