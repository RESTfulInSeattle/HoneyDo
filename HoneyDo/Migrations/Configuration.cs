namespace HoneyDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using HoneyDo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HoneyDo.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            AddUserAndRole(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Todoes.AddOrUpdate(p => p.TaskName,
                 new Todo
                 {
                     TaskName = "Grocery Shopping",
                     Deadline = new DateTime(2016,2,25),
                     Moredetails = "Bread, Milk, Dog Food, Flowers"
                 },
                new Todo
                {
                    TaskName = "Walk The Dog",
                    Deadline = new DateTime(2016, 3, 1),
                    Moredetails = "Apollo likes his walks"
                }
                );

        }

        bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "clayton@naturalselectionmusic.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }
    }
}
