using System.ComponentModel.DataAnnotations;

namespace SatisSimilasyon.Entity.Enum
{
	public enum ObjectStatus
	{
		[Display(Name = "Silindi")]
		Deleted = 0,
		[Display(Name = "Silinmedi")]
		NonDeleted = 1
	}
}
