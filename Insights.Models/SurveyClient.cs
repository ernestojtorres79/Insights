using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public class SurveyClient : AuditObject
    {
        public int SurveyClientId { get; set; }
        public string SurveyClientPhone { get; set; }
        public bool SurveyClientBlackList { get; set; }

    }
}
