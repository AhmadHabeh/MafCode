using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MafCode.Common
{
    public class Encryption
    {
        private static string key = "Y05e3F@2022";
        public static string EncryptString(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecryptString(string cipherText)
        {
            var base64EncodedBytes = Convert.FromBase64String(cipherText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}