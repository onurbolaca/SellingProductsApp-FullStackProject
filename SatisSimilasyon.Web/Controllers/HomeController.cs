using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.UserClasses;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class HomeController : Controller
	{
		private DataContext db = new DataContext();

		[SatisSimilasyon.Web.Filter.Auth]
		[SatisSimilasyon.Web.Filter.Notification]
		public ActionResult Index()
		{
			if (CurrentSession.User == null)
				return RedirectToAction("/Login");
			/*
			 SimCount
			 CompareSimCount
			 ApprovedSimCount
			 UserCount
			 */
			ViewBag.CompareSimCount = db.ApprovedSimulations.Where(x => x.Status == Entity.Enum.Status.Active && x.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted && x.SimulationStatus == Entity.Enum.SimulationStatus.Pending).Count();
			ViewBag.SimCount = db.Simulations.Where(x => x.Status == Entity.Enum.Status.Active && x.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).Count();
			ViewBag.ApprovedSim = db.ApprovedSimulations.Where(x => x.Status == Entity.Enum.Status.Active && x.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted && x.SimulationStatus == Entity.Enum.SimulationStatus.Approved).Count();
			ViewBag.Users = db.Users.Where(x => x.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted && x.Status == Entity.Enum.Status.Active).Count();

			return View();
		}

		public ActionResult Login()
		{
			if (CurrentSession.User == null)
				return View();
			else
				return RedirectToAction("/");
		}

		[HttpPost]
		public ActionResult Login(LoginModel model)
		{
			try
			{
				var result = db.Users.FirstOrDefault(t => t.UserName == model.UserName && t.Password == model.Password);

				if (result != null)
				{
					if (result.Status != Entity.Enum.Status.Passive && result.ObjectStatus != Entity.Enum.ObjectStatus.Deleted)
					{
						//Eğer cutom bir model kullanmak istesekydik, bu şekilde tanımlama yapmalıydık.
						//GirisYapanKullaniciBilgileri girisYapanKullaniciBilgileri = new GirisYapanKullaniciBilgileri();

						//girisYapanKullaniciBilgileri.Ad = result.Name;
						//girisYapanKullaniciBilgileri.Soyad = result.Surname;
						//girisYapanKullaniciBilgileri.KullaniciAdi = result.UserName;
						//CurrentSession.Set<GirisYapanKullaniciBilgileri>("login", girisYapanKullaniciBilgileri);

						CurrentSession.Set<User>("login", result);
						return RedirectToAction("Index", "Home");
					}
					ViewBag.Message = "Kullanıcı Pasif ya da Silinmiş.";
					return View(model);
				}
				else
					ViewBag.Message = "Kullanıcı ya da parolanız hatalı.";

				return View(model);
			}
			catch (Exception hata)
			{

				throw;
			}
		}

		public ActionResult LogOut()
		{
			CurrentSession.ClearSession();
			return RedirectToAction("/");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				db.Dispose();

			base.Dispose(disposing);
		}
	}
}