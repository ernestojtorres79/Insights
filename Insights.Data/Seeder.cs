using Insights.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Insights.Data
{
    public static class Seeder
    {
        public static void Seed(ApplicationDbContext context, bool SeedProfile)
        {
            if (SeedProfile) ProfileSeeder(context);
           

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!RoleManager.RoleExists("Admin"))
            {
                var Role = new IdentityRole();
                Role.Name = "Admin";
                RoleManager.Create(Role);
            }
            if (!RoleManager.RoleExists("User"))
            {
                var Role = new IdentityRole();
                Role.Name = "User";
                RoleManager.Create(Role);
            }

            if (!context.Users.Any(x => x.UserName == "ernestojtorres@hotmail.com"))
            {
                ApplicationUser User = new ApplicationUser()
                {
                    UserName = "ernestojtorres@hotmail.com",
                    Email = "ernestojtorres@hotmail.com",
                    EmailConfirmed = true
                };
                UserManager.Create(User, "123456");
                UserManager.AddToRole(User.Id, "Admin");
            }
        }
        private static void ProfileSeeder(ApplicationDbContext context)
        {
            context.Profiles.AddOrUpdate(x => x.UserId,
                new Profile
                {
                    UserId = context.Users.Where(u => u.Email == "ernestojtorres@hotmail.com").FirstOrDefault().Id,
                    firstName = "Ernesto J",
                    lastName = "Torres",
                    businessName = "Maja GPX",
                    address1 = "819 Forest Ivy Ln",
                    city = "Houston",
                    state = "Texas",
                    zip = "77067",
                    phoneNumber = "832-671-3608"
                });
            context.SaveChanges();
        }
    }
}
