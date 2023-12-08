using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ProgramWEB.Libary
{
    public class StringHelper
    {
        public static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static bool IsPhoneNbr(string number)
        {
            string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch { }
            return false;
        }
        public static bool IsValidCCCD(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }

            foreach (var c in id)
            {
                if (c > '9' || c < '0')
                    return false;
            }

            int maTinh = int.Parse(id.Substring(0, 3));
            if (maTinh < 1 || maTinh > 96)
                return false;

            int gioiTinh = int.Parse(id.Substring(3, 1));
            if (gioiTinh != 2 && gioiTinh != 3)
                return false;

            return true;       
        }
        public static string toDateView(DateTime date)
        {
            if (date == null)
                return string.Empty;
            return date.Day + "/" + date.Month + "/" + date.Year;
        }
    }
}