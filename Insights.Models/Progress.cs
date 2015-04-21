using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class Progress : AuditObject
    {
        public int ProgressId { get; set; }
        public bool ProgressSwitch { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int SurveyClientControlId { get; set; }
        public virtual SurveyClientControl SurveyClientControl { get; set; }
    }
}
