using Insights.Adapters.Interfaces;
using Insights.Data;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Adapters.Adapters
{
    public class QuestionsList : IQuestionsList
    {
        public List<_QuestionViewModel> GetAllQuestions(int campaignId)
        {
            List<_QuestionViewModel> model;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                model = context.Questions.Where(q => q.CampaignId == campaignId).Select(q => new _QuestionViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionBody = q.QuestionBody,
                    QuestionType = q.QuestionType,
                    QuestionActive = q.QuestionActive
                }).ToList();
                return model;
            }
        }
    }
}