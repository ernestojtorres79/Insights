using Insights.Adapters.Adapters;
using Insights.Adapters.Interfaces;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Insights.Controllers
{
    public class apiTwilioController : ApiController
    {
        ITwilioAdapter _adapter;
        public apiTwilioController()
        {
            _adapter = new TwilioAdapter();
        }

        public IHttpActionResult Post(_TwilioMSGViewModel model)
        {
            string userId = User.Identity.GetUserId();
            model.UserId = userId;
            model.ACCOUNT_SID = AuthTwilio.ACCOUNT_SID;
            model.AUTH_TOKEN = AuthTwilio.AUTH_TOKEN;
            _TwilioMSGViewModel temp = _adapter.SendMessage(model);
            return Ok();

        }

        public IHttpActionResult Get()
        {
            List<_TwilioMSGViewModel> model = _adapter.GetAllMessages();
            return Ok(model);
        }
    }
}
