using Insights.Adapters.Interfaces;
using Insights.Data;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Adapters.Adapters
{
    public class QuestionAdapter : IQuestionAdapter
    {
        public _QuestionViewModel GetQuestion(int questionId)
        {
            _QuestionViewModel model;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                model = context.Questions.Where(x => x.QuestionId == questionId).Select(x => new _QuestionViewModel
                {
                    QuestionId = x.QuestionId,
                    QuestionBody = x.QuestionBody,
                    QuestionType = x.QuestionType,
                    CampaignId = x.CampaignId,
                    QuestionActive = x.QuestionActive
                }).FirstOrDefault();
                return model;
            }
        }
        public void AddQuestion(_QuestionViewModel model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Question dbQuestion = context.Questions.Create();
                dbQuestion.QuestionBody = model.QuestionBody;
                dbQuestion.QuestionType = model.QuestionType;
                dbQuestion.CampaignId = model.CampaignId;
                dbQuestion.IsOn = model.QuestionActive;
                context.Questions.Add(dbQuestion);
                context.SaveChanges();
            }
        }
        public void updateQuestion(_QuestionViewModel model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Question dbQuestion = context.Questions.Where(x => x.QuestionId == model.QuestionId).FirstOrDefault();
                dbQuestion.QuestionBody = model.QuestionBody;
                dbQuestion.QuestionType = model.QuestionType;
                dbQuestion.QuestionActive = model.QuestionActive;
                context.SaveChanges();
            }
        }

    }
}