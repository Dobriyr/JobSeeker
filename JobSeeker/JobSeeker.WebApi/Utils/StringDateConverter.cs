using System.Globalization;

namespace JobSeeker.WebApi.Utils
{
	public class StringDateConverter
	{
		public static DateTime GetDjiniDate(string dateString)
		{
			string format = "dd MMMM";
			CultureInfo culture = new CultureInfo("uk-UA");

			DateTime dateTime = DateTime.ParseExact(dateString, format, culture);

			return dateTime;
		}
	}
}
