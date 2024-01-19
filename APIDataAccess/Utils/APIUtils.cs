using System.Text.RegularExpressions;
using System.Text;

namespace APIDataAccess.Utils
{
	public class APIUtils
	{
		public static string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
		public static bool IsValidPhoneNumber(string phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
			{
				return false;
			}

			phoneNumber = phoneNumber.Replace(" ", "");

			if (phoneNumber.Length != 10)
			{
				return false;
			}

			if (!long.TryParse(phoneNumber, out _))
			{
				return false;
			}

			return true;
		}
		public static DateTime DateTimeNow()
		{
			DateTime utc = DateTime.UtcNow;
			TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
			return TimeZoneInfo.ConvertTimeFromUtc(utc, tzi);
		}

		public static string RemoveVietnameseSigns(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				return str;

			string[] vietnameseSigns = new string[]
			{
		"aAeEoOuUiIdDyY",
		"áàạảãâấầậẩẫăắằặẳẵ",
		"ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
		"éèẹẻẽêếềệểễ",
		"ÉÈẸẺẼÊẾỀỆỂỄ",
		"óòọỏõôốồộổỗơớờợởỡ",
		"ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
		"úùụủũưứừựửữ",
		"ÚÙỤỦŨƯỨỪỰỬỮ",
		"íìịỉĩ",
		"ÍÌỊỈĨ",
		"đ",
		"Đ",
		"ýỳỵỷỹ",
		"ÝỲỴỶỸ"
			};

			for (int i = 1; i < vietnameseSigns.Length; i++)
			{
				for (int j = 0; j < vietnameseSigns[i].Length; j++)
				{
					str = str.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
				}
			}

			return str;
		}

		public static string VietToUnsigned(string text)
		{
			text = text.Normalize(NormalizationForm.FormD);
			return Regex.Replace(text, @"[^a-zA-Z0-9\s]", "");
		}
	}
}
