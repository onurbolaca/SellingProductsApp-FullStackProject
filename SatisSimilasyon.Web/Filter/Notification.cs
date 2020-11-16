using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Filter
{
	public class Notification : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var cont = filterContext.Controller as Controller;
			if (cont == null)
				return;

			DataContext db = new DataContext();
			var result = db.Notifications.Where(t => t.UserId == CurrentSession.User.Id && t.NotificationStatus == Entity.Enum.NotificationStatus.UnSeen).ToList();

			if (result != null && result.Count > 0)
			{
				cont.ViewData["Count"] = result.Count;
				cont.ViewData["Notifications"] = result.OrderByDescending(t => t.CreatedOn);
			}

			base.OnActionExecuting(filterContext);
		}
	}
}