using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.ReferenceClasses;
using SatisSimilasyon.Web.Filter;
using SatisSimilasyon.Web.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class ReferencesController : Controller
	{
		private DataContext db = new DataContext();

		[Auth]
		[Notification]
		public ActionResult Index()
		{
			var references = db.References.Include(t => t.ProductGroup);
			return View(references.Where(t => t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).ToList());
		}

		[Auth]
		[Notification]
		public ActionResult Create()
		{
			ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Reference reference)
		{
			if (reference != null)
			{
				if (ModelState.IsValid)
				{
					db.References.Add(new Reference()
					{
						Code = reference.Code,
						Definition = reference.Definition,
						LastPrice = reference.LastPrice,
						LocalOrExport = reference.LocalOrExport,
						Name = reference.Name,
						ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted,
						ProductGroup = reference.ProductGroup,
						ProductGroupId = reference.ProductGroupId,
						SalesQuantity = reference.SalesQuantity,
						Status = reference.Status,
						CustomerReferenceCode = reference.CustomerReferenceCode,
						LastModifiedBy = CurrentSession.GetOnlineUser(),
						LastModifiedOn = DateTime.Now,
						CreatedBy = CurrentSession.GetOnlineUser(),
						CreatedOn = DateTime.Now
					});

					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}

			ViewBag.ProductGroupID = new SelectList(db.ProductGroups, "Id", "Name", reference.ProductGroupId);
			return View(reference);
		}

		[Auth]
		[Notification]
		public ActionResult Edit(int? id)
		{
			if (id == null)
				return RedirectToAction("Index");

			Reference reference = db.References.Find(id);
			if (reference == null)
				return RedirectToAction("Index");

			var priceLogs = db.PriceLogs.Where(t => t.ReferenceId == id).ToList();
			if (priceLogs != null && priceLogs.Count > 0)
			{
				ViewBag.PriceLogs = priceLogs.OrderByDescending(t => t.CreatedOn.Ticks);
			}

			#region Gelen bildirimlerin yönetimi
			var noti = db.Notifications.Where(t => t.UserId == CurrentSession.User.Id && t.NotiId == id && t.ContAction == "/References/Edit" && t.NotificationStatus == Entity.Enum.NotificationStatus.UnSeen).FirstOrDefault();
			if (noti != null)
			{
				noti.NotificationStatus = Entity.Enum.NotificationStatus.Seen;
				noti.LastModifiedBy = CurrentSession.GetOnlineUser();
				noti.LastModifiedOn = DateTime.Now;

				db.Entry(noti).State = EntityState.Modified;
				db.SaveChanges();
			}
			#endregion

			ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Name");

			return View(reference);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Reference reference)
		{
			if (reference != null)
			{
				if (ModelState.IsValid)
				{
					var result = db.References.FirstOrDefault(t => t.Id == reference.Id);

					float lastPriceLog = result.LastPrice;

					result.Definition = reference.Definition;
					result.Name = reference.Name;
					result.Code = reference.Code;
					result.SalesQuantity = reference.SalesQuantity;
					result.LastPrice = reference.LastPrice;
					result.LocalOrExport = reference.LocalOrExport;
					result.ReferenceGroup = reference.ReferenceGroup;
					result.CustomerReferenceCode = reference.CustomerReferenceCode;
					result.ProductGroupId = reference.ProductGroupId;
					result.Status = reference.Status;
					result.LastModifiedBy = CurrentSession.GetOnlineUser();
					result.LastModifiedOn = DateTime.Now;

					db.Entry(result).State = EntityState.Modified;

					//eğer fiyat değişmişse LastPriceLog tablosuna kayıt oluştur
					if (lastPriceLog != reference.LastPrice)
					{
						db.PriceLogs.Add(new PriceLogs()
						{
							ReferenceId = result.Id,
							CreatedBy = CurrentSession.GetOnlineUser(),
							CreatedOn = DateTime.Now,
							LastModifiedBy = CurrentSession.GetOnlineUser(),
							LastModifiedOn = DateTime.Now,
							LastPriceLog = lastPriceLog,
							ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted,
							Status = Entity.Enum.Status.Active,
							Reference = result
						});

						var noti = db.Users.Where(t => t.Status == Entity.Enum.Status.Active && t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted && (t.Department == Entity.Enum.Department.Admin || t.Department == Entity.Enum.Department.Management)).ToList();

						foreach (var item in noti)
						{
							db.Notifications.Add(new Entity.NotificationClasses.Notification()
							{
								NotiId = result.Id,
								ContAction = "/Reference/Edit",
								Description = "Referens kartında günceleme mevcut",
								User = item,
								NotificationStatus = Entity.Enum.NotificationStatus.UnSeen,
								CreatedBy = CurrentSession.GetOnlineUser(),
								CreatedOn = DateTime.Now,
								LastModifiedBy = CurrentSession.GetOnlineUser(),
								LastModifiedOn = DateTime.Now,
								ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted,
								Status = Entity.Enum.Status.Active
							});
						}
					}
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			else
				return RedirectToAction("Index");

			ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "Id", "Name", reference.ProductGroupId);

			return View(reference);
		}

		[HttpPost]
		public JsonResult Delete(int? id)
		{
			ValidationModel validationModel = new ValidationModel();

			try
			{
				Reference result = db.References.Where(t => t.Id == id).FirstOrDefault();
				result.LastModifiedBy = CurrentSession.GetOnlineUser();
				result.LastModifiedOn = DateTime.Now;
				result.Status = Entity.Enum.Status.Passive;
				result.ObjectStatus = Entity.Enum.ObjectStatus.Deleted;

				db.Entry(result).State = EntityState.Modified;
				if (db.SaveChanges() > 0)
				{
					validationModel.Type = "success";
					validationModel.Message = "Silme işlemi başarılı.";
				}
			}
			catch (Exception hata)
			{
				validationModel.Type = "error";
				validationModel.Message = "Silme işleminde hata oluştu.";
			}

			return Json(validationModel, JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				db.Dispose();

			base.Dispose(disposing);
		}
	}
}