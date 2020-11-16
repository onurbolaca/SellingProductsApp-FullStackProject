using System.ComponentModel.DataAnnotations;

namespace SatisSimilasyon.Entity.Enum
{
	public enum Status
	{
		[Display(Name = "Pasif")]
		Passive = 0,
		[Display(Name = "Aktif")]
		Active = 1
	}
}
