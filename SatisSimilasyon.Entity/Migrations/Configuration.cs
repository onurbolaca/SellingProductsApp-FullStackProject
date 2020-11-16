namespace SatisSimilasyon.Entity.Migrations
{
  using SatisSimilasyon.Entity.UserClasses;
  using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SatisSimilasyon.Entity.Context.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SatisSimilasyon.Entity.Context.DataContext context)
        {
        }
    }
}
