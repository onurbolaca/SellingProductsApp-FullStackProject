using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.UserClasses;
using SatisSimilasyon.Web.Filter;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class UsersController : Controller
	{
		private DataContext db = new DataContext();

		[Auth]
		[Notification]
		public ActionResult Index()
		{
			var result = db.Users.Where(t => t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).ToList();
			return View(result);
		}

		[Auth]
		[Notification]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(UserModel user)
		{
			if (user != null)
			{
				if (ModelState.IsValid)
				{
					var result = new User()
					{
						LastModifiedBy = CurrentSession.GetOnlineUser(),
						CreatedBy = CurrentSession.GetOnlineUser(),
						CreatedOn = DateTime.Now,
						Department = user.Department,
						EMail = user.Email,
						LastModifiedOn = DateTime.Now,
						Name = user.Name,
						ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted,
						Password = user.Password,
						Status = user.Status,
						Surname = user.Surname,
						UserName = user.UserName,
						BolumMuduru = false
					};
					db.Users.Add(result);
					db.SaveChanges();

					return RedirectToAction("Index");
				}
				return View(user);
			}
			else
				return RedirectToAction("Index");
		}

		[HttpPost]
		public JsonResult DeleteUser(int id)
		{
			ValidationModel validationModel = new ValidationModel();
			try
			{
				User user = db.Users.Where(t => t.Id == id).FirstOrDefault();
				user.LastModifiedBy = string.Format("{0} {1}", CurrentSession.User.Name, CurrentSession.User.Surname);
				user.LastModifiedOn = DateTime.Now;
				user.Status = Entity.Enum.Status.Passive;
				user.ObjectStatus = Entity.Enum.ObjectStatus.Deleted;

				db.Entry(user).State = System.Data.Entity.EntityState.Modified;
				if (db.SaveChanges() > 0)
				{
					validationModel.Type = "success";
					validationModel.Message = "Silme işlemi başarılı";
				}
				else
				{
					validationModel.Type = "error";
					validationModel.Message = "Silme işleminde hata oluştu";
				}
			}
			catch (Exception)
			{
				validationModel.Type = "error";
				validationModel.Message = "Silme işleminde hata oluştu";
			}
			return Json(validationModel, JsonRequestBehavior.AllowGet);
		}

		[Auth]
		[Notification]
		//[OnlyManagement]
		public ActionResult Edit(int? id)
		{
			if (id == null)
				return RedirectToAction("Index");

			User user = db.Users.Find(id);
			if (user == null)
				return RedirectToAction("Index");

			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserModel user)
		{
			var result = db.Users.FirstOrDefault(t => t.Id == user.Id);
			if (result != null)
			{
				result.Name = user.Name;
				result.Surname = user.Surname;
				result.UserName = user.UserName;
				result.Password = user.Password;
				result.EMail = user.Email;
				result.Department = user.Department;
				result.Status = user.Status;
				result.LastModifiedBy = CurrentSession.GetOnlineUser();
				result.LastModifiedOn = DateTime.Now;
				result.BolumMuduru = user.BolumMuduru;

				db.Entry(result).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				db.Dispose();

			base.Dispose(disposing);
		}
	}
}