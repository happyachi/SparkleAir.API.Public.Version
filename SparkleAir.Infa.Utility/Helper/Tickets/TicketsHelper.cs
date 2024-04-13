using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Tickets
{
    public class TicketsHelper
    {
        public AppDbContext _context;
        public TicketsHelper(AppDbContext context)
        {
            _context = context;
        }
        public static string CreateTicketNum()
        {
            //隨機生成10碼亂數 前面加上031 組成13碼的字串
            int length = 10;
            string randomPart = GenerateRandomNumber(length);
            string prefix = "031";
            // 組合成13碼的字串
            string ticketNum = prefix + randomPart;

            return ticketNum;
        }

        private static string GenerateRandomNumber(int length)
        {
            using (var crypto = new RNGCryptoServiceProvider())
            {
                var bits = (length * 6);
                var byte_size = ((bits + 7) / 8);
                var randomBytes = new byte[byte_size];
                crypto.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-", "").Substring(0, length);
            }
        }
        public static string CreateBookingRef()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = 6;
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string CreateOrderNum()
        {
            string brand = "SK";
            string refix = "031";
            string currentYear = DateTime.Now.Year.ToString();
            int currentNum = 1;
            var lastOrder = _context.Tickets
                .OrderByDescending(x => x.OrderTime)
                .FirstOrDefault();

            if (lastOrder != null)
            {
                currentNum = int.Parse(lastOrder.OrderNum.Substring(brand.Length + currentYear.Length + refix.Length)) + 1;
            }

            // 根據長度計算要補充的0的位數
            int paddingLength = 13 - (brand.Length + currentYear.Length + refix.Length);
            paddingLength = Math.Max(paddingLength, 0); // 防止paddingLength為負數

            // 確保currentNum的字符串表示形式至少為paddingLength + 1位數，不足的話前面補0
            string currentNumString = currentNum.ToString().PadLeft(paddingLength + 1, '0');

            string orderNum = brand + currentYear + refix + currentNumString;

            return orderNum;
        }
    }
}
