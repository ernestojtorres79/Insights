using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Adapters.Interfaces
{
    interface ICampaignAdapter
    {
        List<_CampaignViewModel>GetAllCampaigns(string userId);
        _CampaignViewModel GetCampaign(int id);
        void CreateCampaign(_CampaignViewModel model);
        void UpdateCampaign(_CampaignViewModel model);
    }
}
