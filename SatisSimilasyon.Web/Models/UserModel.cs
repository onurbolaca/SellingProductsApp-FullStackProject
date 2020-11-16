using SatisSimilasyon.Entity.Enum;
using SatisSimilasyon.Entity.UserClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[DisplayName("Ad")]
		[DataType(DataType.Text)]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Name { get; set; }

		[DisplayName("Soyad")]
		[DataType(DataType.Text)]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Surname { get; set; }

		[DisplayName("Soyad")]
		[DataType(DataType.EmailAddress)]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Email { get; set; }

		[DisplayName("Kullanıcı Adı")]
		[DataType(DataType.Text)]
		[StringLength(15, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string UserName { get; set; }

		[DisplayName("Soyad")]
		[StringLength(15, MinimumLength = 2, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Password { get; set; }

		[DisplayName("Bölümü")]
		[Required(ErrorMessage = "Bölüm alanı boş geçilemez.")]
		public Department Department { get; set; }

		public Status Status{ get; set; }

		public int UserId { get; set; }
		public virtual User  User{ get; set; }

		public bool CustomerList { get; set; }
		public bool CustomerInsert { get; set; }
		public bool CustomerDelete{ get; set; }
		public bool CustomerEdit { get; set; }

		public bool ReferenceList { get; set; }
		public bool ReferenceInsert { get; set; }
		public bool ReferenceDelete { get; set; }
		public bool ReferenceEdit { get; set; }

		public bool ProductGroupList { get; set; }
		public bool ProductGroupInsert { get; set; }
		public bool ProductGroupDelete { get; set; }
		public bool ProductGroupEdit { get; set; }

		public bool SimulationList { get; set; }
		public bool SimulationInsert { get; set; }
		public bool SimulationDelete { get; set; }
		public bool SimulationEdit { get; set; }

		public bool BolumMuduru { get; set; }
	}
}