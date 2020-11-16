using SatisSimilasyon.Entity.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SatisSimilasyon.Web.Models
{
	public class CurrentSession
	{
		public static User User
		{
			get
			{
				return Get<User>("login");
			}
		}

		/// <summary>
		/// Session içindeki bilgili eğer güvenilir bir yerden istenmişse, modele ek olarak geri dönüyoruz.
		/// </summary>
		/// <typeparam name="T">Metodu çağıran Model</typeparam>
		/// <param name="key">Session içindeki key(Değişken adı)</param>
		/// <returns></returns>
		private static T Get<T>(string key)
		{
			if (HttpContext.Current.Session[key] != null)
			{
				return (T)HttpContext.Current.Session[key];
			}
			else
			{
				return default(T);
			}
		}

		public static void Set<T>(string key, T obj)
		{
			HttpContext.Current.Session[key] = obj;
		}

		public static void ClearSession()
		{
			HttpContext.Current.Session.Clear();
		}

		public static string GetOnlineUser()
		{
			string result = "";
			result = string.Format("{0} {1}", User.Name, User.Surname);

			return result;
		}
	}
}