using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Filter
{
	public class OnlyManagement : FilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (CurrentSession.User == null)
			{
				filterContext.Result = new RedirectResult("/Home/Login");
			}
			else
			{
				if(CurrentSession.User.Department != Entity.Enum.Department.Management &&
				CurrentSession.User.Department != Entity.Enum.Department.Admin)
				filterContext.Result = new RedirectResult("/Home/");
			}
		}
	}
}