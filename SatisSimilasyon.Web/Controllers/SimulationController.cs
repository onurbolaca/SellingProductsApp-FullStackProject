using Newtonsoft.Json;
using SatisSimilasyon.Entity.Context;
using SatisSimilasyon.Entity.Enum;
using SatisSimilasyon.Entity.ReferenceClasses;
using SatisSimilasyon.Entity.SimulationClasses;
using SatisSimilasyon.Web.Filter;
using SatisSimilasyon.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SatisSimilasyon.Web.Controllers
{
	public class SimulationController : Controller
	{
		private DataContext _context = new DataContext();

		[Auth]
		[Notification]
		public ActionResult Index()
		{
		//Düzenleme -> bütün similasyonlar gelsin ama değiştir butonuna basınca, saedce kendi açtığı similasyonu değiştirebilmesine izin verelim.

			var user = CurrentSession.GetOnlineUser();
			var result = _context.Simulations.Where(x => x.CreatedBy == user && x.Status == Status.Active && x.ObjectStatus == ObjectStatus.NonDeleted).ToList();
			return View(result);
		}

		public ActionResult KullaniciKarsilastir(string AdSoyad){
			
			string a = AdSoyad;

			return View();
		}


		[Auth]
		[Notification]
		public ActionResult Create()
		{
			ViewBag.ProductGroup = new SelectList(_context.ProductGroups.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList(), "Id", "Name");
			return View();
		}


		[HttpPost]
		public JsonResult Create(SimulationModel model)
		{
			ValidationModel valIdationModel = new  ValidationModel();
			try
			{

				Simulation simulation = new Simulation()
				{
					MevcutLTA = TryConvertFloat(model.MevcutLTA),
					ArtisMevcutNonResale = TryConvertFloat(model.ArtisMevcutNonResale),
					ArtisMevcutResale = TryConvertFloat(model.ArtisMevcutResale),
					YeniLTA = TryConvertFloat(model.YeniLTA),
					ArtisYeniNonResale = TryConvertFloat(model.ArtisYeniNonResale),
					ArtisYeniResale = TryConvertFloat(model.ArtisYeniResale),
					MevcutArtisExportResale = TryConvertFloat(model.MevcutArtisExportResale),
					MevcutExportNonResale = TryConvertFloat(model.MevcutExportNonResale),
					MevcutEnflasyon = TryConvertFloat(model.MevutEnflasyon),
					NonResaleMevcutFiyat = TryConvertFloat(model.NonResaleMevcutFiyat),
					NonResaleYeniFiyat = TryConvertFloat(model.NonResaleYeniFiyat),
					ResaleMevcutFiyat = TryConvertFloat(model.ResaleMevcutFiyat),
					YeniEnflasyon = TryConvertFloat(model.YeniEnflasyon),
					YeniArtisExportResale = TryConvertFloat(model.YeniArtisExportResale),
					ResaleYeniFiyat = TryConvertFloat(model.ResaleYeniFiyat),
					YeniArtisExportNonResale = TryConvertFloat(model.YeniArtisExportNonResale),
					CreatedBy = CurrentSession.GetOnlineUser(),
					CreatedOn = DateTime.Now,
					LastModifiedBy = CurrentSession.GetOnlineUser(),
					LastModifiedOn = DateTime.Now,
					ObjectStatus = ObjectStatus.NonDeleted,
					SimulationName = model.SimulationName,
					Status = Status.Active,
					SimulationLines = new List<SimulationLine>(),
					SimulationType = CurrentSession.User.Department == Department.financialConsultant ? Department.financialConsultant : Department.Sales,
					ProductGroupId = Convert.ToInt32(model.ProductGroupID),
					SimulationStatus = SimulationStatus.Pending,
					LocalOrExport = model.LocalOrExport == "0" ? LocalOrExport.Local : LocalOrExport.Export,

				};
				_context.Simulations.Add(simulation);
				_context.SaveChanges();

				foreach (var item in model.Lines)
				{
					SimulationLine line = new SimulationLine()
					{
						Status = Status.Active,
						CreatedBy = CurrentSession.GetOnlineUser(),
						CreatedOn = DateTime.Now,
						CustomerReferenceCode = item.CustomerReferenceCode,
						LastModifiedBy = CurrentSession.GetOnlineUser(),
						LastModifiedOn = DateTime.Now,
						NewPrice = TryConvertFloat(item.NewPrice),
						ObjectStatus = ObjectStatus.NonDeleted,
						SalesPriceDifference = Convert.ToDecimal(item.SalesPriceDifference),
						SalesQuantity = Convert.ToDecimal(item.SalesQuantity),
						TurnoverDifference = Convert.ToDecimal(item.TurnoverDifference),
						Price = TryConvertFloat(item.Price),
						SimulationId = simulation.Id,
						ReferenceCode = item.ReferenceCode,
						ProductType = item.ProductType.ToLower() == "resale" ? ProductType.Resale : ProductType.NonResale,

					};

					_context.SimulationLines.Add(line);
					_context.SaveChanges();

					valIdationModel.Type = "success";
					valIdationModel.Message = "Simülasyon Başarılı Bir Şekilde Kayıt Olmuştur";
				}
			}
			catch (Exception ex)
			{
				valIdationModel.Type = "error";
				valIdationModel.Message = "Simülasyonu Kayıt Ederken Teknik Bir Hata Oluştu";
			}
			return Json(valIdationModel, JsonRequestBehavior.AllowGet);
		}
		[Auth]
		[Notification]
		public ActionResult Detail(int? Id)
		{
			if (Id == null)
			{
				return RedirectToAction("Index");
			}

			Simulation sim = _context.Simulations.Include(x => x.ProductGroups).Where(x => x.Id == Id).FirstOrDefault();


			if (sim == null)
			{
				return RedirectToAction("Index");
			}
			ViewBag.Lines = _context.SimulationLines.Where(x => x.SimulationId == Id && x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList();
			ViewBag.info = _context.ProductGroups.Where(x => x.Id == sim.ProductGroupId).FirstOrDefault().Name;
			return View(sim);
		}
		public float TryConvertFloat(string value)
		{
			float convertedValue = 0;

			Single.TryParse(value, out convertedValue);

			return convertedValue;
		}
		[HttpPost]
		public string GetReferanceByGroup(int? Id, int ProductGroupId)
		{
			List<Reference> _references = new List<Reference>();

			switch (Id)
			{
				case 2:
					_references = _context.References.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active && t.ProductGroupId == ProductGroupId).ToList();
					break;
				case 0:
					_references = _context.References.Where(t => t.LocalOrExport == LocalOrExport.Local && t.Status == Status.Active && t.ObjectStatus == ObjectStatus.NonDeleted && t.ProductGroupId == ProductGroupId && t.Status == Status.Active).ToList();
					break;
				case 1:
					_references = _context.References.Where(t => t.LocalOrExport == LocalOrExport.Export && t.Status == Status.Active && t.ObjectStatus == ObjectStatus.NonDeleted && t.ProductGroupId == ProductGroupId && t.Status == Status.Active).ToList();
					break;
				default:
					_references = _context.References.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active).ToList();
					break;
			}

			return JsonConvert.SerializeObject(_references, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
		}
		[HttpPost]
		public string GetSimulationLines(int? Id)
		{
			if (Id == null)
			{
				return "";
			}

			var result = _context.SimulationLines.Where(x => x.SimulationId == Id && x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList();

			return JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
		}
		[Auth]
		[Notification]
		public ActionResult CompareSimulation()
		{
			ViewBag.Sim1 = new SelectList(_context.Simulations.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active && x.SimulationType == Department.Sales).ToList(), "Id", "SimulationName");
			ViewBag.Sim2 = new SelectList(_context.Simulations.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active && x.SimulationType == Department.financialConsultant).ToList(), "Id", "SimulationName");
			ViewBag.PG = new SelectList(_context.ProductGroups.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList(), "Id", "Name");
			return View();
		}
		[HttpPost]
		public string GetSimulationDataById(int? Id)
		{
			if (Id == 0)
			{
				return "";
			}

			var result = _context.Simulations.Where(x => x.Id == Id && x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList();

			return JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
		}
		[HttpPost]
		public string GetDRPData(FormCollection form)
		{
			if (form != null)
			{
				int Id = Convert.ToInt32(form["Id"]);
				int Type = Convert.ToInt32(form["Type"]);
				int LocalOrExport = Convert.ToInt32(form["LocalOrExport"]);

				if (Type == 4)
				{
					var result = _context.Simulations.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active && x.ProductGroupId == Id && x.SimulationType == Department.Sales).ToList();

					if (LocalOrExport == 0)
					{
						return JsonConvert.SerializeObject(result.Where(x => x.LocalOrExport == Entity.Enum.LocalOrExport.Local), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					}
					else
					{
						return JsonConvert.SerializeObject(result.Where(x => x.LocalOrExport == Entity.Enum.LocalOrExport.Export), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					}

				}
				else if (Type == 5)
				{
					var result = _context.Simulations.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active && x.ProductGroupId == Id && x.SimulationType == Department.financialConsultant).ToList();
					if (LocalOrExport == 0)
					{
						return JsonConvert.SerializeObject(result.Where(x => x.LocalOrExport == Entity.Enum.LocalOrExport.Local), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					}
					else
					{
						return JsonConvert.SerializeObject(result.Where(x => x.LocalOrExport == Entity.Enum.LocalOrExport.Export), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					}
				}
			}
			return JsonConvert.SerializeObject("", Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
		}
		[HttpPost]
		public JsonResult SimulationApproval(ApprovalModel model)
		{
			ValidationModel vm = new ValidationModel();
			try
			{
				if (model != null)
				{

					int sim1 = Convert.ToInt32(model.Simulation1ID);
					int sim2 = Convert.ToInt32(model.Simulation2ID);
					int selected = Convert.ToInt32(model.SelectedSimulationID);
					int productId = Convert.ToInt32(model.ProductGroupID);
					var result = _context.ApprovedSimulations.Where(x => x.Simulation1Id == sim1 && x.Simulation2Id == sim2 && x.SelectedSimulationId == selected).FirstOrDefault();

					if (result != null)
					{
						vm.Type = "error";
						vm.Message = "Bu Karşılaştırma Daha Önce Yapıldı. Lütfen Başka Bir Karşılaştırma Yapınız";
					}
					else
					{

						result = new ApprovedSimulation();
						result.Simulation1 = _context.Simulations.Where(x => x.Id == sim1).FirstOrDefault();
						result.Simulation2 = _context.Simulations.Where(x => x.Id == sim2).FirstOrDefault();
						result.SelectedSimulationId = selected;
						result.Definition = model.Definition;
						result.CreatedBy = CurrentSession.GetOnlineUser();
						result.CreatedOn = DateTime.Now;
						result.LastModifiedBy = CurrentSession.GetOnlineUser();
						result.LastModifiedOn = DateTime.Now;
						result.ObjectStatus = ObjectStatus.NonDeleted;
						result.Status = Status.Active;
						result.SimulationStatus = SimulationStatus.Pending;
						result.ProductGroupId = productId;
						result.LocalOrExport = model.LocalOrExport == "0" ? LocalOrExport.Local : LocalOrExport.Export;

						_context.ApprovedSimulations.Add(result);
						_context.SaveChanges();

						var noti = _context.Users.Where(x => x.Status == Status.Active && x.ObjectStatus == ObjectStatus.NonDeleted && (x.Department == Department.Admin || x.Department == Department.Management)).ToList();

						foreach (var item in noti)
						{
							_context.Notifications.Add(new Entity.NotificationClasses.Notification()
							{
								NotiId = result.Id,
								ContAction = "/Simulation/CompareSimulationDetail/",
								Description = "Onay Bekleyen Yeni Bir Simülasyon Var !",
								User = item,
								NotificationStatus = NotificationStatus.UnSeen,
								CreatedBy = CurrentSession.GetOnlineUser(),
								CreatedOn = DateTime.Now,
								LastModifiedBy = CurrentSession.GetOnlineUser(),
								LastModifiedOn = DateTime.Now,
								ObjectStatus = ObjectStatus.NonDeleted,
								Status = Status.Active
							});
						}

						_context.SaveChanges();

						var mailAdress = CurrentSession.User.EMail;
						var message = string.Format("{0} Satış ve {1} Mail İşler Simülasyon Karşılaştırması {2} Tarafından Onaya Gönderilmiştir.", result.Simulation1.SimulationName, result.Simulation2.SimulationName, CurrentSession.GetOnlineUser());
						var nameSurname = CurrentSession.GetOnlineUser();
						var subTitle = string.Format("Onay Bekleyen Yeni Bir Simülasyon Var !");

						var returnRes = MailHelper.SendMail(message, nameSurname, subTitle, mailAdress);
						vm.Type = "success";

						switch (returnRes)
						{
							case "0":
								vm.Message = "Karşılaştırma Başarıyla Onaya Gönderildi Fakat Mail Gönderilirken Teknik Bir Hata Oluştu.";
								break;
							case "1":
								vm.Message = "Karşılaştırma ve Mail Başarıyla Onaya Gönderildi";
								break;
							case "2":
								vm.Message = "Karşılaştırma Başarıyla Onaya Gönderildi Fakat Mail Gönderilirken Teknik Bir Hata Oluştu. Mail Parametrelerini Kontrol Ediniz.";
								break;
							default:
								break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				vm.Type = "success";
				vm.Message = "Karşılaştırma Başarıyla Onaya Gönderildi";
			}
			return Json(vm, JsonRequestBehavior.AllowGet);
		}

		[Auth]
		[Notification]
		[OnlyManagement]
		public ActionResult CompareSimulationDetail(int? Id)
		{
			if (Id == null)
			{
				return RedirectToAction("Index");
			}

			var result = _context.ApprovedSimulations.Where(x => x.Id == Id && x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).FirstOrDefault();

			if (result == null)
			{
				return RedirectToAction("Index");
			}

			var noti = _context.Notifications.Where(x => x.UserId == CurrentSession.User.Id && x.NotiId == Id && x.ContAction == "/Simulation/CompareSimulationDetail/" && x.NotificationStatus == NotificationStatus.UnSeen).FirstOrDefault();

			if (noti != null)
			{
				noti.NotificationStatus = NotificationStatus.Seen;
				noti.LastModifiedBy = CurrentSession.GetOnlineUser();
				noti.LastModifiedOn = DateTime.Now;
				_context.Entry(noti).State = EntityState.Modified;
				_context.SaveChanges();
			}
			return View(result);
		}
		[HttpPost]
		public string GetSimulationById(int? Id)
		{
			if (Id == 0)
			{
				return "";
			}
			var result = _context.Simulations.Where(x => x.Id == Id && x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList();

			return JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
		}
		[HttpPost]
		public JsonResult ApproveIt(FormCollection form)
		{
			ValidationModel vm = new ValidationModel();
			try
			{
				if (form != null)
				{
					int CompareId = Convert.ToInt32(form["CompareSimId"]);
					string approvedDefinition = form["AppvoredDefinition"];
					string type = form["Type"];

					var compare = _context.ApprovedSimulations.Where(x => x.Id == CompareId && x.ObjectStatus == ObjectStatus.NonDeleted).FirstOrDefault();

					compare.SimulationStatus = type == "0" ? SimulationStatus.Denied : SimulationStatus.Approved;
					compare.LastModifiedBy = CurrentSession.GetOnlineUser();
					compare.LastModifiedOn = DateTime.Now;
					compare.ApprovedDefinition = approvedDefinition;

					var sim = _context.Simulations.Where(x => x.Id == compare.SelectedSimulation.Id && x.ObjectStatus == ObjectStatus.NonDeleted).FirstOrDefault();

					sim.SimulationStatus = type == "0" ? SimulationStatus.Denied : SimulationStatus.Approved;
					sim.LastModifiedBy = CurrentSession.GetOnlineUser();
					sim.LastModifiedOn = DateTime.Now;
					if (type == "1")
					{
						foreach (var item in sim.SimulationLines)
						{
							var refs = _context.References.Where(x => x.CustomerReferenceCode == item.CustomerReferenceCode && x.Code == item.ReferenceCode && x.Status == Status.Active && x.ObjectStatus == ObjectStatus.NonDeleted).FirstOrDefault();
							float priceLog = refs.LastPrice;
							refs.LastPrice = item.NewPrice;
							refs.LastModifiedBy = CurrentSession.GetOnlineUser();
							refs.LastModifiedOn = DateTime.Now;

							_context.PriceLogs.Add(new PriceLogs()
							{
								ReferenceId = refs.Id,
								LastPriceLog = priceLog,
								CreatedBy = CurrentSession.GetOnlineUser(),
								CreatedOn = DateTime.Now,
								LastModifiedBy = CurrentSession.GetOnlineUser(),
								LastModifiedOn = DateTime.Now,
								Reference = refs,
								ObjectStatus = Entity.Enum.ObjectStatus.NonDeleted,
								Status = Entity.Enum.Status.Active,
							});
						}

						_context.Notifications.Add(new Entity.NotificationClasses.Notification()
						{
							NotiId = compare.Id,
							ContAction = "/Simulation/PriceDifference/",
							Description = "Simülasyonunuz Onaylandı !",
							User = _context.Users.Where(x => x.Name + " " + x.Surname == compare.CreatedBy).FirstOrDefault(),
							NotificationStatus = NotificationStatus.UnSeen,
							CreatedBy = CurrentSession.GetOnlineUser(),
							CreatedOn = DateTime.Now,
							LastModifiedBy = CurrentSession.GetOnlineUser(),
							LastModifiedOn = DateTime.Now,
							ObjectStatus = ObjectStatus.NonDeleted,
							Status = Status.Active
						});

						var mailAdress = CurrentSession.User.EMail;
						var message = string.Format("{0} adlı simülasyonunuz yönetici tarafından onaylanmıştır.", compare.SelectedSimulation.SimulationName);
						var nameSurname = CurrentSession.GetOnlineUser();
						var subTitle = string.Format("Simülasyonunuz Onaylandı!");

						var returnRes = MailHelper.SendMail(message, nameSurname, subTitle, mailAdress);

					}
					if (_context.SaveChanges() > 0)
					{
						vm.Type = "success";
						vm.Message = type == "0" ? "Simülasyon Başarıyla Reddedilmiştir." : "Simülasyon Başarıyla Onaylanmıştır";
					}
				}
			}
			catch (Exception ex)
			{
				vm.Type = "error";
				vm.Message = "Teknik Bir Hata Oldu";
			}
			return Json(vm, JsonRequestBehavior.AllowGet);

		}
		[Auth]
		[Notification]
		public ActionResult CompareSimulationsList()
		{
			var result = _context.ApprovedSimulations.Where(x => x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).ToList();

			return View(result);
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
					vm.Message = "Dosya Yüklenemedi";
				}
				else
				{
					var result = ExcelUploadHelper.GetSalesQuantitydt(file);

					return Json(result, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				vm.Type = "error";
				vm.Message = "Teknik Hata";
			}
			return Json(vm, JsonRequestBehavior.AllowGet);

		}
		[Auth]
		[Notification]
		public ActionResult PriceDifference(int? Id)
		{
			if (Id == null)
			{
				return RedirectToAction("Index");
			}
			var result = _context.ApprovedSimulations.Where(x => x.Id == Id && x.ObjectStatus == ObjectStatus.NonDeleted && x.Status == Status.Active).FirstOrDefault();
			if (result == null)
			{
				return RedirectToAction("Index");
			}
			var noti = _context.Notifications.Where(x => x.UserId == CurrentSession.User.Id && x.NotiId == Id && x.ContAction == "/Simulation/PriceDifference/" && x.NotificationStatus == NotificationStatus.UnSeen).FirstOrDefault();
			if (noti != null)
			{
				noti.NotificationStatus = NotificationStatus.Seen;
				noti.LastModifiedBy = CurrentSession.GetOnlineUser();
				noti.LastModifiedOn = DateTime.Now;
				_context.Entry(noti).State = EntityState.Modified;
				_context.SaveChanges();
			}
			return View(result);
		}
	}
}