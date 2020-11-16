using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
	public class ExcelUploadHelper
	{
		/// <summary>
		/// Excel de hazır olan veriyi sistem içerisine alma
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static List<ExcelDataLines> GetExcelDatadt(HttpPostedFileBase file)
		{
			var benzersizDeger = Guid.NewGuid();
			var path = HttpContext.Current.Server.MapPath("/Content/Excel/") + benzersizDeger.ToString() + Path.GetExtension(file.FileName);

			var dosyaAdi = benzersizDeger + ".xlsx";
			file.SaveAs(path);

			List<ExcelDataLines> lines = new List<ExcelDataLines>();
			DataTable dt = new DataTable();
			try
			{
				using (OleDbConnection excelBaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0; HDR = YES';"))
				{
					excelBaglanti.Open();

					dt = excelBaglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					string sheet1 = dt.Rows[0]["TABLE_NAME"].ToString();

					//excel in ilk sayfasındaki verileri dt2 ye aktar
					OleDbDataAdapter da = new OleDbDataAdapter(string.Format("Select * From [{0}]", sheet1), excelBaglanti);
					DataTable excelTempVerileri = new DataTable();
					da.Fill(excelTempVerileri);
					excelBaglanti.Close();

					foreach (var item in excelTempVerileri.AsEnumerable())
					{
						/*Excel deki sıra listesi
						 0- ProductGroup
						 1- CustomerReferenceCode
						 2- Code
						 3- Name
						 4- LastPrice
						 5- ReferenceGroup
						 6- LocalOrExport
						 */

						var ProductGroup = item[0].ToString();
						var CustomerReferenceCode = item[1].ToString();
						var Code = item[2].ToString();
						var Name = item[3].ToString();
						var LastPrice = item[4].ToString();
						var ReferenceGroup = item[5].ToString();
						var LocalOrExport = item[6].ToString();

						//satırdaki bütün verilerin dolu olmasını bekliyoruz
						if (string.IsNullOrWhiteSpace(ProductGroup) ||
						string.IsNullOrWhiteSpace(CustomerReferenceCode) ||
						string.IsNullOrWhiteSpace(Code) ||
						string.IsNullOrWhiteSpace(Name) ||
						string.IsNullOrWhiteSpace(LastPrice) ||
						string.IsNullOrWhiteSpace(ReferenceGroup) ||
						string.IsNullOrWhiteSpace(LocalOrExport)) continue;

						lines.Add(new ExcelDataLines()
						{
							ProductGroup = ProductGroup,
							CustomerReferenceCode = CustomerReferenceCode,
							Code = Code,
							Name = Name,
							LastPrice = LastPrice,
							ReferenceGroup = ReferenceGroup,
							LocalOrExport = LocalOrExport
						});
					}
				}
			}
			catch (Exception hata)
			{
				throw new Exception(hata.Message);
			}

			//excel i silme işlemi
			DirectoryInfo dosya = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/Content/Excel/");
			FileInfo[] fileInfo = dosya.GetFiles();

			foreach (var item in fileInfo)
			{
				if (item.Name == dosyaAdi)
					item.Delete();
			}
			return lines;
		}

		/// <summary>
		/// Excel den içeri fiyat kartlarını alma - Simülasyon esnasında eklenebilir.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static List<SalesQuantityExcel> GetSalesQuantitydt(HttpPostedFileBase file)
		{
			var benzersizDeger = Guid.NewGuid();
			var path = HttpContext.Current.Server.MapPath("/Content/Excel/") + benzersizDeger.ToString() + Path.GetExtension(file.FileName);

			var dosyaAdi = benzersizDeger + ".xlsx";
			file.SaveAs(path);

			DataTable dt = new DataTable();
			List<SalesQuantityExcel> lines = new List<SalesQuantityExcel>();
			try
			{
				using (OleDbConnection excelBaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0; HDR = YES';"))
				{
					excelBaglanti.Open();

					dt = excelBaglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					string sheet1 = dt.Rows[0]["TABLE_NAME"].ToString();

					//excel in ilk sayfasındaki verileri dt2 ye aktar
					OleDbDataAdapter da = new OleDbDataAdapter(string.Format("Select * From [{0}]", sheet1), excelBaglanti);
					DataTable excelTempVerileri = new DataTable();
					da.Fill(excelTempVerileri);
					excelBaglanti.Close();

					foreach (var item in excelTempVerileri.AsEnumerable())
					{
						var customerReferenceCode = item[0].ToString();
						var referenceCode = item[1].ToString();
						var salesQuantity = item[2].ToString();

						if (string.IsNullOrWhiteSpace(customerReferenceCode)) continue;
						if (string.IsNullOrWhiteSpace(referenceCode)) continue;
						if (string.IsNullOrWhiteSpace(salesQuantity)) continue;

						lines.Add(new SalesQuantityExcel()
						{
							CustomerReferenceCode = customerReferenceCode,
							ReferenceCode = referenceCode,
							SalesQuantity = salesQuantity
						});
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			//excel i silme işlemi
			DirectoryInfo dosya = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/Content/Excel/");
			FileInfo[] fileInfo = dosya.GetFiles();

			foreach (var item in fileInfo)
			{
				if (item.Name == dosyaAdi)
					item.Delete();
			}
			return lines;
		}
	}
}