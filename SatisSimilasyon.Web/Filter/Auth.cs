using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.UserClasses;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Filter
{
	public class Auth : FilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if(CurrentSession.User ==null)
				filterContext.Result = new RedirectResult("/Home/Login");
			else
			{
				DataContext db = new DataContext();
				var result = db.Users.Where(t => t.Id == CurrentSession.User.Id).FirstOrDefault();

				CurrentSession.ClearSession();
				CurrentSession.Set<User>("login", result);
			}
		}
	}
}