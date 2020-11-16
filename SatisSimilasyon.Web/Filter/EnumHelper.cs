using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SatisSimilasyon.Web.Filter
{
	public static class EnumHelper
	{
		public static string DisplayName(this Enum enumType)
		{
			return enumType.GetType().GetMember(enumType.ToString())
			.First().GetCustomAttribute<DisplayAttribute>().Name;
		}
	}
}