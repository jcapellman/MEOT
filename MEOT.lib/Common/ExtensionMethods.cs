using System;
using System.Security.Cryptography;
using System.Text;

namespace MEOT.lib.Common
{
    public static class ExtensionMethods
    {
        public static string ToSHA256(this string str)
        {
            using var shaManager = new SHA256Managed();

            return BitConverter.ToString(shaManager.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "");
        }
    }
}