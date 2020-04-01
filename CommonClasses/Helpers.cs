using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace rad.net
{
    public static class Helpers
    {
        public static string Encrypt(string un, string pw)
        {
            //string password = pw + un;
            //HashAlgorithm mhash = new SHA1CryptoServiceProvider();
            //byte[] bytValue = Encoding.UTF8.GetBytes(password);
            //byte[] bytHash = mhash.ComputeHash(bytValue);
            //mhash.Clear();
            //return Convert.ToBase64String(bytHash);
            using MD5 md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, pw);
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string GenerateJwtToken(string id)
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            var configuration = builder.Build(); var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id),
            };

            var key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value)
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static long Random(Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return Math.Abs(longRand);
        }
    }

    public static class Extentions
    {
        public static DateTime? ToGregoryDate(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var items = str.Split(new char[] { '/', '-', '_', '\\' });
            if (items.Length != 3)
                return null;
            int year = Convert.ToInt32(items[0].Trim());
            int month = Convert.ToInt32(items[1].Trim());
            int day = Convert.ToInt32(items[2].Trim());
            var gc = new PersianCalendar();
            return gc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public static string GetExceptionMessage(this Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex.Message;
        }
    }

    public static class JalaliDate
    {
        public static string GetJalaliDate(this DateTime date, JalaliDateFormat format)
        {
            PersianCalendar jc = new PersianCalendar();
            int year = jc.GetYear(date);
            int month = jc.GetMonth(date);
            int day = jc.GetDayOfMonth(date);
            string dayName = jc.GetDayOfWeek(date).GetJalaliName();
            string monthName = GetJalaliMonthName(month);
            string str;
            switch (format)
            {
                case JalaliDateFormat.SimpleDate:
                    str = year + "/" + month + "/" + day;
                    break;
                case JalaliDateFormat.Zarvan:
                    str = year + "/" + (month < 10 ? ("0" + month) : month.ToString()) + "/" + (day < 10 ? ("0" + day) : day.ToString());
                    break;
                case JalaliDateFormat.SimpleDateTime:
                    str = year + "/" + month + "/" + day + ", " + date.ToShortTimeString();
                    break;
                case JalaliDateFormat.LongDate:
                    str = dayName + " " + day + " " + monthName + " " + year;
                    break;
                case JalaliDateFormat.LongDateTime:
                    str = dayName + " " + day + " " + monthName + " " + year + ", ساعت " + date.ToLongTimeString();
                    break;
                case JalaliDateFormat.Elpased:
                    var span = (DateTime.Now - date);
                    double value = span.TotalSeconds;
                    if (value < 60)
                        str = " لحظاتی پیش";
                    else if (value < 3600)
                        str = Math.Round(span.TotalMinutes) + " دقیقه پیش";
                    else if (value < 86400)
                        str = Math.Round(span.TotalHours) + " ساعت پیش";
                    else
                        str = Math.Round(span.TotalDays) + " روز پیش";
                    break;
                case JalaliDateFormat.Day:
                    str = day.ToString();
                    break;
                default:
                    str = year + "/" + month + "/" + day;
                    break;
            }
            return str;
        }
        public static string GetJalaliName(this DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Friday => "جمعه",
                DayOfWeek.Monday => "دوشنبه",
                DayOfWeek.Saturday => "شنبه",
                DayOfWeek.Sunday => "یک‌شنبه",
                DayOfWeek.Thursday => "پنج‌شنبه",
                DayOfWeek.Tuesday => "سه‌شنبه",
                DayOfWeek.Wednesday => "چهارشنبه",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        public static string GetJalaliMonthName(int month)
        {
            return month switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    public enum JalaliDateFormat
    {
        SimpleDate,
        SimpleDateTime,
        LongDate,
        LongDateTime,
        Elpased,
        Zarvan,
        Day
    }

}
