using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class Campaign : AuditObject
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public bool CampaignActive { get; set; }
        public string CampaignKeyword { get; set; }
        public string CampaignGift { get; set; }
        public List<Question> Questions { get; set; }
        // Virtual link to users
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
