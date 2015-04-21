using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class SurveyClientControl : AuditObject
    {
        public int SurveyClientControlId { get; set; } 
        public string SurveyClientPhone { get; set;}        
        public int SurveyClientId { get; set; }
        public virtual SurveyClient SurveyClient { get; set; }
        public int CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }
        public List<Question> Questions { get; set;}
        public List<Response> Responces { get; set; }
        public List<Progress> ProgressSwitches { get; set; }
        public List<CurrentQuestion> CurrentQuestionSwitches { get; set; }
        public string UserId { get; set; }
        public bool BlackList { get; set; }

    }
}
