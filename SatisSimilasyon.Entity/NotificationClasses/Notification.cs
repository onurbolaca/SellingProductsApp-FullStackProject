using SatisSimilasyon.Entity.BaseClasses;
using SatisSimilasyon.Entity.Enum;
using SatisSimilasyon.Entity.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.NotificationClasses
{
	public class Notification : BaseObject
	{
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public int NotiId { get; set; }
		public string ContAction { get; set; }
		public string Description { get; set; }
		public NotificationStatus NotificationStatus { get; set; }
	}
}
