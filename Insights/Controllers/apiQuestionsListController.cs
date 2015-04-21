using Insights.Adapters.Adapters;
using Insights.Adapters.Interfaces;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Insights.Controllers
{
    public class apiQuestionsListController : ApiController
    {
        IQuestionsList _adapter;
        public apiQuestionsListController()
        {
            _adapter = new QuestionsList(); 
        }
        public IHttpActionResult Get(int Id)
        {
            List<_QuestionViewModel> model = _adapter.GetAllQuestions(Id);
            return Ok(model);
        }
    }
}
