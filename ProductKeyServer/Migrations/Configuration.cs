using ProductKeyServer.Models;

namespace ProductKeyServer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductKeyServer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductKeyServer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var vps1 = context.Keys.ToList();
            foreach (var vp1 in vps1)
            {
                context.Keys.Remove(vp1);
            }
            context.SaveChanges();

            Key key = new Key
            {
                ExpirationDate = DateTime.Now.AddDays(3),
                HardwareId = "",
                IsDisabled = false,
                LastChecked = DateTime.Now,
                ProductKey = "1234567890"
            };

            context.Keys.Add(key);
            context.SaveChanges();
        }
    }
}
