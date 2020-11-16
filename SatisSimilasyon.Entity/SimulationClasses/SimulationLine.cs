using SatisSimilasyon.Entity.BaseClasses;
using SatisSimilasyon.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.SimulationClasses
{
	public class SimulationLine : BaseObject
	{
		public int SimulationId { get; set; }
		public virtual Simulation Simulation { get; set; }

		public string CustomerReferenceCode { get; set; }
		public string ReferenceCode { get; set; }
		public ProductType ProductType { get; set; }

		public float Price { get; set; }
		public float NewPrice { get; set; }

		public decimal SalesQuantity { get; set; }
		public decimal SalesPriceDifference { get; set; }
		public decimal TurnoverDifference { get; set; }

	}
}
