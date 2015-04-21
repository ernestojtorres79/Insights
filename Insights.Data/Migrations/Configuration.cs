namespace Insights.Data.Migrations
{
    using Insights.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Insights.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Insights.Data.ApplicationDbContext context)
        {
            Seeder.Seed(context, true);
        }
    }
}
