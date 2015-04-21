using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Models
{
    public class _SurveyClientViewModel
    {
        public int SurveyClientVMId { get; set; }
        public string SurveyClientVMPhone { get; set; }
        public DateTime DateJoined { get; set; }
        public bool Blacklisted { get; set; }
        public int CampaignId { get; set; }
    }
}