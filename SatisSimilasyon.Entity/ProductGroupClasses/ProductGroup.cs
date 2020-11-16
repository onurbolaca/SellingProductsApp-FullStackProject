using SatisSimilasyon.Entity.BaseClasses;
using SatisSimilasyon.Entity.ReferenceClasses;
using SatisSimilasyon.Entity.SimulationClasses;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SatisSimilasyon.Entity.ProductGroupClasses
{
	public class ProductGroup : BaseObject
	{
		[DisplayName("Ürün Grup Adı")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Name { get; set; }

		[DisplayName("Oran 1")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public decimal Oran1 { get; set; }

		[DisplayName("Oran 2")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public decimal Oran2 { get; set; }

		public List<Reference> References { get; set; }
		public List<ApprovedSimulation> ApprovedSimilations { get; set; }
	}
}
