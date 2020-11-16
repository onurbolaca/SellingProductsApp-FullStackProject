using SatisSimilasyon.Entity.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatisSimilasyon.Entity.BaseClasses
{
public	class BaseObject
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[DisplayName("Oluşturma Tarihi")]
		[ScaffoldColumn(false)]
		[Column(TypeName = "datetime2")]
		public DateTime CreatedOn { get; set; }

		[DisplayName("Oluşturan Kişi")]
		[ScaffoldColumn(false)]
		public string CreatedBy { get; set; }

		[DisplayName("Son Güncelleme Tarihi")]
		[ScaffoldColumn(false)]
		[Column(TypeName = "datetime2")]
		public DateTime LastModifiedOn { get; set; }

		[DisplayName("Son Güncelleyen Kişi")]
		[ScaffoldColumn(false)]
		public string LastModifiedBy { get; set; }

		[DisplayName("Silindi Bilgisi")]
		[ScaffoldColumn(false)]
		public ObjectStatus ObjectStatus { get; set; }

		[DisplayName("Durumu")]
		[ScaffoldColumn(false)]
		public Status Status { get; set; }
	}
}
