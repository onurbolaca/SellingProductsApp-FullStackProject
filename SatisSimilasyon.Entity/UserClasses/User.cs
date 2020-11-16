using SatisSimilasyon.Entity.BaseClasses;
using SatisSimilasyon.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.UserClasses
{
	public class User : BaseObject
	{
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
		public string EMail { get; set; }

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

		[DisplayName("Bölüm Müdürü")]
		public bool BolumMuduru { get; set; }
	}
}
