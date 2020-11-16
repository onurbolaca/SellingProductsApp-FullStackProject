using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
	public class LoginModel
	{
		[DisplayName("Kullanıcı Adı")]
		public string UserName { get; set; }
		[DisplayName("Parola")]
		public string Password { get; set; }
	}
}