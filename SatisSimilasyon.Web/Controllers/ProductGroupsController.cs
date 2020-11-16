using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.ProductGroupClasses;
using SatisSimilasyon.Web.Filter;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class ProductGroupsController : Controller
	{
		private DataContext db = new DataContext();

		[Auth]
		[Notification]
		public ActionResult Index()
		{
			var pg = db.ProductGroups.Where(t => t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).ToList();

			return View(pg);
		}

		[Auth]
		[Notification]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ProductGroup productGroup)
		{
			if (productGroup != null)
			{
				if (db.ProductGroups.Where(t => t.Name == productGroup.Name && t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).FirstOrDefault() != null)
				{
					ViewBag.Empty = string.Format("{0} ismindeki ürün grubu zaten tanımlı.", productGroup.Name);
				}
				if (ViewBag.Empty == null)
				{
					if (ModelState.IsValid)
					{
						db.ProductGroups.Add(new ProductGroup()
						{
							Name = productGroup.Name,
							Oran1 = productGroup.Oran1,
							Oran2 = productGroup.Oran2,
							CreatedBy = CurrentSession.GetOnlineUser(),
							CreatedOn = DateTime.Now,
							LastModifiedBy = CurrentSession.GetOnlineUser(),
							LastModifiedOn = DateTime.Now,
							Status = Entity.Enum.Status.Active,
							ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted
						}
						);

						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
			}
			else
				return RedirectToAction("Index");

			return View(productGroup);
		}

		[Auth]
		[Notification]
		public ActionResult Edit(int? id)
		{
			if (id == null)
				return RedirectToAction("Index");

			var pg = db.ProductGroups.Find(id);
			if (pg == null)
				return RedirectToAction("Index");

			return View(pg);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ProductGroup productGroup)
		{
			if (productGroup != null)
			{
				var pg = db.ProductGroups.Where(t => t.Name == productGroup.Name && t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).FirstOrDefault();
				if (pg != null)
				{
					if (db.ProductGroups.Where(t => t.Id == productGroup.Id).FirstOrDefault() == null)
						ViewBag.Empty = string.Format("{0} isimli grup zaten var.", productGroup.Name);
				}

				if (ViewBag.Empty == null)
				{
					if (ModelState.IsValid)
					{
						var result = db.ProductGroups.FirstOrDefault(t => t.Id == productGroup.Id);
						result.Name = productGroup.Name;
						result.Oran1 = productGroup.Oran1;
						result.Oran2 = productGroup.Oran2;
						result.Status = productGroup.Status;
						result.LastModifiedBy = CurrentSession.GetOnlineUser();
						result.LastModifiedOn = DateTime.Now;
						
						db.Entry(result).State = System.Data.Entity.EntityState.Modified;
						db.SaveChanges();

						return RedirectToAction("Index");
					}
				}
			}
			else
				return RedirectToAction("Index");

			return View(productGroup);
		}

		[HttpPost]
		public JsonResult Delete(int? id)
		{
			ValidationModel vm = new ValidationModel();

			try
			{
				ProductGroup pg = db.ProductGroups.Where(t => t.Id == id).FirstOrDefault();

				var ReferenceList = db.References.Where(t => t.ProductGroupId == id && t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).FirstOrDefault();

				if (ReferenceList != null)
				{
					vm.Type = "error";
					vm.Message = "Bu ürün grubu kullanılıyor. Silemezsiniz.";
				}
				else
				{
					pg.LastModifiedBy = CurrentSession.GetOnlineUser();
					pg.LastModifiedOn = DateTime.Now;
					pg.Status = Entity.Enum.Status.Passive;
					pg.ObjectStatus = Entity.Enum.ObjectStatus.Deleted;

					db.Entry(pg).State = System.Data.Entity.EntityState.Modified;

					if (db.SaveChanges() > 0)
					{
						vm.Type = "success";
						vm.Message = "Silme başarılı";
					}
				}
			}
			catch (Exception hata)
			{
				vm.Type = "error";
				vm.Message = "Silme esnasında hata. " + hata.Message;
			}

			return Json(vm, JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				db.Dispose();

			base.Dispose(disposing);
		}
	}
}