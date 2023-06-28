using System.Globalization;

namespace JobSeeker.BLL.Services.DataConverter
{
	public static class StringDateConverter
	{
		public static DateTime GetDjiniDate(string dateString)
		{
			string format = "d MMMM";
			CultureInfo culture = new CultureInfo("uk-UA");
			dateString = dateString.Trim();

			DateTime dateTime = DateTime.Today;
			try
			{
				dateTime = DateTime.ParseExact(dateString, format, culture);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return dateTime;
		}
	}
}
