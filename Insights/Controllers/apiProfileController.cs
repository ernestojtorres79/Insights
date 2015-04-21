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
    public class apiProfileController : ApiController
    {
        IProfileDataAdapter _adapter;
        public apiProfileController()
        {
            _adapter = new ProfileDataAdapter();
        }
        public IHttpActionResult Get()
        {
            string userId = User.Identity.GetUserId();
            _ProfileViewModel model = _adapter.GetProfile(userId);
            return Ok(model);
        }
    }
}