using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class Question : AuditObject
    {
        
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public string QuestionType { get; set; }
        public bool QuestionActive { get; set; }

        // Virtual Link to Campaing

        public int CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }

        // Virtual Link to SurveyClientControl

        public List<SurveyClientControl> SurveyClientControlIds { get; set; }
        //public virtual SurveyClientControl SurveyClientControl { get; set; }

    }
}
