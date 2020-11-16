using SatisSimilasyon.Entity.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.ReferenceClasses
{
	public class PriceLogs : BaseObject
	{
		public int ReferenceId { get; set; }
		public float LastPriceLog { get; set; }
		public virtual Reference Reference { get; set; }
	}
}
