using System.Security.Cryptography;
using System.Text;

namespace DBWork
{
    internal class Utils
    {
        public static string Md5(string input, bool isLowercase = false)
        {
            if (string.IsNullOrEmpty(input)) return "";
            using (var md5 = MD5.Create())
            {
                var byteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }

        public static string Sha256(string input, bool isLowercase = false)
        {
            if (string.IsNullOrEmpty(input)) return "";

            using (SHA256 sha256 = SHA256.Create())
            {
                var byteHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var hash = BitConverter.ToString(byteHash).Replace("-", "");
                return (isLowercase) ? hash.ToLower() : hash;
            }
        }
    }
}
