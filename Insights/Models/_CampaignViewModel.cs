using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Models
{
    public class _CampaignViewModel
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public bool CampaignActive { get; set; }
        public string CampaignKeyWord { get; set; }
        public string CampaignGift { get; set; }
        public string UserId { get; set; }
        public List<_QuestionViewModel> Questions { get; set; }
    }
}