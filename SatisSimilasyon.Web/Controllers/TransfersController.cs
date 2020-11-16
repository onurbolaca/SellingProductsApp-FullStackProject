using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.ReferenceClasses;
using SatisSimilasyon.Web.Filter;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class TransfersController : Controller
	{
		private DataContext db = new DataContext();

		[Auth]
		[Notification]
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ExcelToDb()
		{
			return View();
		}

		[HttpPost]
		public JsonResult UploadExcel(HttpPostedFileBase file)
		{
			ValidationModel vm = new ValidationModel();

			try
			{
				if (file == null)
				{
					vm.Type = "error";
					vm.Message = "Dosya yüklenemedi";
				}
				else
				{
					//Excel i okumak için Helper nesnesi oluşturacağız
					//Dönen verileri db ye yazacağız

					var result = ExcelUploadHelper.GetExcelDatadt(file);
					return Json(result, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception hata)
			{
				vm.Type = "error";
				vm.Message = hata.Message;
			}

			return Json(vm, JsonRequestBehavior.AllowGet);
		}

		//Excel den gelen veriler db ye yazılıyor
		[HttpPost]
		public JsonResult SaveExcelData(IList<ExcelDataLines> model)
		{
			using (var tr = db.Database.BeginTransaction())
			{
				ValidationModel vm = new ValidationModel();

				try
				{
					if (model != null)
					{
						foreach (var item in model)
						{
							//excel den okuduğumuz grup bizde var mı kontrolü. Eğer yoksa dışarı atalım.
							var productGroup = db.ProductGroups.Where(t => t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted && t.Name == item.ProductGroup).FirstOrDefault();

							if (productGroup == null)
							{
								vm.Type = "error";
								vm.Message = string.Format("{0} adlı ürün grubu sistemde kayıtlı değil", item.ProductGroup);
								tr.Rollback(); //yapılan işlemler varsa geri al
								return Json(vm, JsonRequestBehavior.AllowGet);
							}

							var reference = db.References.Where(t => t.CustomerReferenceCode == item.CustomerReferenceCode && t.Code == item.Code && t.ObjectStatus == Entity.Enum.ObjectStatus.NonDeleted).FirstOrDefault();
							if (reference != null)
							{
								vm.Type = "error";
								vm.Message = string.Format("{0} adlı Müşteri Referans kodlu {1} referansı sistemde zaten kayıtlı", item.CustomerReferenceCode, item.Code);
								tr.Rollback(); //yapılan işlemler varsa geri al
								return Json(vm, JsonRequestBehavior.AllowGet);
							}

							var references = new Reference()
							{
								Code = item.Code,
								CreatedBy = CurrentSession.GetOnlineUser(),
								CreatedOn = DateTime.Now,
								CustomerReferenceCode = item.CustomerReferenceCode,
								// Definition= item.Definition
								LastModifiedBy = CurrentSession.GetOnlineUser(),
								LastModifiedOn = DateTime.Now,
								LastPrice = float.Parse(item.LastPrice),
								LocalOrExport = item.LocalOrExport.ToLower() == "local" ? Entity.Enum.LocalOrExport.Local : Entity.Enum.LocalOrExport.Export,
								Name = item.Name,
								ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted,
								ProductGroupId = productGroup.Id,
								ReferenceGroup = item.ReferenceGroup.ToLower() == "resale" ? Entity.Enum.ProductType.Resale : Entity.Enum.ProductType.NonResale,
								//SalesQuantity
								Status = Entity.Enum.Status.Active
							};

							db.References.Add(references);
							db.SaveChanges();

							vm.Type = "success";
							vm.Message = "Kayıt başarılı";
						}
					}
				}
				catch (Exception hata)
				{
					vm.Type = "success";
					vm.Message = "Kayıt sırasında hata oluştu."+hata.Message;
					tr.Rollback();
					return Json(vm, JsonRequestBehavior.AllowGet);
				}
				tr.Commit();
				return Json(vm, JsonRequestBehavior.AllowGet);
			}
		}
	}
}