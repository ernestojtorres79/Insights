using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class Response : AuditObject
    {
        public int ResponseId { get; set; }
        public string ResponseBody { get; set; }

        // Virual Link to Question
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        // Virtual Link to SurveyClientControl

        public int SurveyClientControlId { get; set; }
        public virtual SurveyClientControl SurveyClientControl { get; set; }
    }
}
