using System.ComponentModel.DataAnnotations;

namespace SatisSimilasyon.Entity.Enum
{
	public enum LocalOrExport
	{
		[Display(Name = "Local")]
		Local = 0,
		[Display(Name = "Export")]
		Export = 1
	}
}
