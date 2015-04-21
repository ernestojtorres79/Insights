using Insights.Adapters.Interfaces;
using Insights.Data;
using Insights.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insights.Adapters.Adapters
{
    public class ProfileDataAdapter : IProfileDataAdapter
    {
        public _ProfileViewModel GetProfile(string UserId)
        {
            _ProfileViewModel profileUser;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                profileUser = context.Profiles.Where(p => p.UserId == UserId).Select(p => new _ProfileViewModel
                {
                    Id = p.profileId,
                    firstName = p.firstName,
                    lastName = p.lastName,
                    businessName = p.businessName,
                    address1 = p.address1,
                    city = p.city,
                    state = p.state,
                    zip = p.zip,
                    phoneNumber = p.phoneNumber
                }).FirstOrDefault();
                return profileUser;
            }
        }
    }
}