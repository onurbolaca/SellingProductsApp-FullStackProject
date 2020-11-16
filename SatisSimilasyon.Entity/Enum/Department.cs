using System.ComponentModel.DataAnnotations;

namespace SatisSimilasyon.Entity.Enum
{
	public enum Department
	{
		[Display(Name = "Admin")]
		Admin = 0,
		[Display(Name = "Yönetici")]
		Management = 1,
		[Display(Name = "Finans")]
		Accounting = 2,
		[Display(Name = "Bilgi Teknolojileri")]
		Informationsystems = 3,
		[Display(Name = "Satış")]
		Sales = 4,
		[Display(Name = "Mali Danışman")]
		financialConsultant = 5,
	}
}
