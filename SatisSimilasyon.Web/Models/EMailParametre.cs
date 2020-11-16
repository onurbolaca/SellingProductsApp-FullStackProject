using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
	public class EMailParametre
	{
		public string GondericiAdi { get; set; }
		public string EPostaAdresi { get; set; }
		public string EPostaSifresi { get; set; }
		public string SmtpAdresi { get; set; }
		public int SmtpPortu { get; set; }
		public bool SSLKullan { get; set; }

		public string UyariMail1 { get; set; }
	}
}