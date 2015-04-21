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
    public class apiCampaignController : ApiController
    {
        ICampaignAdapter _adapter;
        public apiCampaignController()
        {
            _adapter = new CampaignAdapter();
        }
        public IHttpActionResult Get()
        {
            string userId = User.Identity.GetUserId();
            List<_CampaignViewModel> model = _adapter.GetAllCampaigns(userId);
            return Ok(model);
        }
        public IHttpActionResult Get(int Id)
        {
            _CampaignViewModel model = _adapter.GetCampaign(Id);
            return Ok(model);
        }
        public IHttpActionResult Post(_CampaignViewModel model)
        {
            string userId = User.Identity.GetUserId();
            model.UserId = userId;
            _adapter.CreateCampaign(model);
            return Ok();
        }
        public IHttpActionResult Put(_CampaignViewModel model)
        {
            string userId = User.Identity.GetUserId();
            model.UserId = userId;
            _adapter.UpdateCampaign(model);
            return Ok();
        }
    }
}