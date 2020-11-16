using SatisSimilasyon.Entity.BaseClasses;
using SatisSimilasyon.Entity.Enum;
using SatisSimilasyon.Entity.ProductGroupClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.ReferenceClasses
{
	public class Reference : BaseObject
	{
		[DisplayName("Referans Kodu")]
		[DataType(DataType.Text)]
		[StringLength(15, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Code { get; set; }

		[DisplayName("Referans Adı")]
		[DataType(DataType.Text)]
		[StringLength(15, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Name { get; set; }

		[DisplayName("Son Satış Fiyatı")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public float LastPrice { get; set; }

		[DisplayName("Referans Grubu")]
		public ProductType ReferenceGroup { get; set; }

		[DisplayName("Local Or Export")]
		public LocalOrExport LocalOrExport { get; set; }

		public int ProductGroupId { get; set; }
		public virtual ProductGroupClasses.ProductGroup ProductGroup { get; set; }

		[DisplayName("Müşteri")]
		public string CustomerReferenceCode { get; set; }

		[DisplayName("Satış Miktarı")]
		public decimal SalesQuantity { get; set; }

		[DisplayName("Açıklama")]
		[Column(TypeName = "nvarchar(MAX)")]
		[StringLength(9999, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		public string Definition { get; set; }
	}
}
