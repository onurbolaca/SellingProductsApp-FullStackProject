using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.Enum
{
	public enum ProductType
	{
		[Display(Name = "Resale")]
		Resale = 0,
		[Display(Name = "Non-Resale")]
		NonResale = 1
	}
}
