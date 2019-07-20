namespace MeetingManagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MeetingManagementSystem.Models.MeetingManagementDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
           //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MeetingManagementSystem.Models.MeetingManagementDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
