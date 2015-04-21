using Insights.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SurveyClient> SurveyClients { get; set; }
        public DbSet<TwilioMSG> TwilioMSGs { get; set; }
        public DbSet<SurveyClientControl> SurveyClientControls { get; set; }
        public DbSet<Progress> ProgressSwitches { get; set; }
        public DbSet<CurrentQuestion> CurrentQuestionSwitches { get; set; }
        public DbSet<Response> Responses { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
