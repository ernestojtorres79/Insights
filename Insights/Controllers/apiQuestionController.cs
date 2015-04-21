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
    public class apiQuestionController : ApiController
    {
        IQuestionAdapter _adapter;
        public apiQuestionController()
        {
            _adapter = new QuestionAdapter();
        }
        public IHttpActionResult Get(int Id)
        {
            _QuestionViewModel model = _adapter.GetQuestion(Id);
            return Ok(model);
        }
        public IHttpActionResult Post(_QuestionViewModel model)
        {
            _adapter.AddQuestion(model);
            return Ok();
        }
        public IHttpActionResult Put(_QuestionViewModel model)
        {
            _adapter.updateQuestion(model);
            return Ok();
        }
    }
}
