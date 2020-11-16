using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.MailParamsClasses;
using SatisSimilasyon.Web.Filter;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class MailParamsController : Controller
	{
		private DataContext db = new DataContext();

		[Auth]
		[Notification]
		public ActionResult Index()
		{
			var result = db.MailParams.FirstOrDefault();

			return View(result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(MailParams mailParams)
		{
			if (mailParams != null)
			{
				if (ModelState.IsValid)
				{
					var result = db.MailParams.FirstOrDefault();

					result.LastModifiedBy = CurrentSession.GetOnlineUser();
					result.LastModifiedOn = DateTime.Now;
					result.CreatedBy = CurrentSession.GetOnlineUser();
					result.CreatedOn = DateTime.Now;

					result.Email = mailParams.Email;
					result.Password = mailParams.Password;
					result.SMTP = mailParams.SMTP;
					result.SSL = mailParams.SSL;
					result.Port = mailParams.Port;

					db.Entry(result).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
					return View(mailParams);
			}

			return RedirectToAction("Index");
		}

		public void SendTestMail()
		{
			var nameSurname = CurrentSession.GetOnlineUser();
			var mailAdress = CurrentSession.User.EMail;

			var returnRes = MailHelper.SendMail("Test Maili", nameSurname, "Test", mailAdress);
		}
	}
}