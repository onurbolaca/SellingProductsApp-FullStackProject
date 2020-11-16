using SatisSimilasyon.Entity.MailParamsClasses;
using SatisSimilasyon.Entity.NotificationClasses;
using SatisSimilasyon.Entity.ProductGroupClasses;
using SatisSimilasyon.Entity.ReferenceClasses;
using SatisSimilasyon.Entity.SimulationClasses;
using SatisSimilasyon.Entity.UserClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.Context
{
	public class DataContext : DbContext
	{
		public DataContext() : base(ConnectionString())
		{
			//initialize işelmi var mı?
			Database.SetInitializer<DataContext>(new DbInitializer<DataContext>());
		}

		private static string ConnectionString()
		{
			var conStr = ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString;
			return conStr.ToString();
		}
		public DbSet<MailParams> MailParams { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<ProductGroup> ProductGroups { get; set; }
		public DbSet<PriceLogs> PriceLogs { get; set; }
		public DbSet<Reference> References { get; set; }
		public DbSet<ApprovedSimulation> ApprovedSimulations { get; set; }
		public DbSet<Simulation> Simulations { get; set; }
		public DbSet<SimulationLine> SimulationLines { get; set; }
		public DbSet<User> Users { get; set; }

	}

	internal class DbInitializer<T> : CreateDatabaseIfNotExists<DataContext>
	{
		protected override void Seed(DataContext context)
		{
			User yeniKullanici = new User() { CreatedBy = "admin", CreatedOn = DateTime.Now, Department = Enum.Department.Admin, EMail = "admin@admin.com", Id = 1, LastModifiedBy = "admin", LastModifiedOn = DateTime.Now, Name = "admin", ObjectStatus = Enum.ObjectStatus.NonDeleted, Password = "123", Status = Enum.Status.Active, Surname = "Amca", UserName = "Yönetici" };

			context.Users.Add(yeniKullanici);
			base.Seed(context);
		}
	}
}
