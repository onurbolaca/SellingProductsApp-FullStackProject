using SatisSimilasyon.Entity.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisSimilasyon.Entity.MailParamsClasses
{
	public class MailParams : BaseObject
	{
		[DisplayName("Mail Adresi")]
		[DataType(DataType.EmailAddress)]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Email { get; set; }

		[DisplayName("Parola")]
		[DataType(DataType.Text)]
		[StringLength(15, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string Password { get; set; }

		[DisplayName("SMTP")]
		[DataType(DataType.Text)]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "{0} alanı en az {2} en fazla {1} karakterden oluşabilir.")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public string SMTP { get; set; }

		[DisplayName("SMTP Port")]
		[Required(ErrorMessage = "{0} alanı boş geçilemez.")]
		public int Port { get; set; }

		[DisplayName("SSL")]
		public bool SSL { get; set; }
	}
}
