using Insights.Adapters.Interfaces;
using Insights.Data;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Insights.Adapters.Adapters
{
    public class CampaignAdapter : ICampaignAdapter
    {
        public List<_CampaignViewModel> GetAllCampaigns(string Id)
        {
            List<_CampaignViewModel> AllCampaigns;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                AllCampaigns = context.Campaigns.Where(c => c.UserId == Id).Select(c => new _CampaignViewModel()
                {
                    CampaignId = c.CampaignId,
                    CampaignName = c.CampaignName,
                    CampaignKeyWord = c.CampaignKeyword,                    
                    CampaignGift = c.CampaignGift,
                    CampaignActive = c.CampaignActive

                }).ToList();
            }
            return AllCampaigns;
        }
        public _CampaignViewModel GetCampaign(int Id)
        {
            _CampaignViewModel SingleCampaign;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                SingleCampaign = context.Campaigns.Where(x => x.CampaignId == Id).Select(x => new _CampaignViewModel()
                {
                    CampaignId = x.CampaignId,
                    CampaignName = x.CampaignName,                    
                    CampaignKeyWord = x.CampaignKeyword,
                    CampaignGift = x.CampaignGift,
                    CampaignActive = x.CampaignActive 
                }).FirstOrDefault();
              return SingleCampaign;
            }
        }
        public void CreateCampaign(_CampaignViewModel model)
        {
            
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Campaign dbCampaign = context.Campaigns.Create();
                dbCampaign.CampaignName = model.CampaignName;
                dbCampaign.CampaignKeyword = model.CampaignKeyWord;
                dbCampaign.CampaignActive = model.CampaignActive;
                dbCampaign.CampaignGift = model.CampaignGift;
                dbCampaign.UserId = model.UserId;
                context.Campaigns.Add(dbCampaign);
                context.SaveChanges();
            }
        }
        public void UpdateCampaign(_CampaignViewModel model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Campaign dbCampaign = context.Campaigns.Where(c => c.CampaignId == model.CampaignId).FirstOrDefault();
                dbCampaign.CampaignKeyword = model.CampaignKeyWord;
                dbCampaign.CampaignActive = model.CampaignActive;
                dbCampaign.CampaignGift = model.CampaignGift;
                context.SaveChanges();
            }
        }
    }
}