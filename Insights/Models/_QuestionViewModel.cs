using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Models
{
    public class _QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionBody{ get; set; }
        public string QuestionType { get; set; }
        public int CampaignId { get; set; }
        public bool QuestionActive { get; set; }
    }
}