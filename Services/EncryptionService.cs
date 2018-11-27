using System;
using System.Security.Cryptography;
using System.Text;
using com.petronas.myevents.api.Configurations;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace com.petronas.myevents.api.Helpers
{
    public static class EncryptionService
    {
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Crypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Base64UrlEncode(Convert.ToBase64String(outputBuffer));
        }

        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(Base64UrlDecode(text));
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }

        private static string Base64UrlEncode(string text)
        {
            if (text == null || text == "")
            {
                return null;
            }

            byte[] bytesToEncode = System.Text.UTF8Encoding.UTF8.GetBytes(text);
            String returnVal = System.Convert.ToBase64String(bytesToEncode);

            return returnVal.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        private static string Base64UrlDecode(string text)
        {
            if (text == null || text == "")
            {
                return null;
            }

            text.Replace('-', '+');
            text.Replace('_', '/');

            int paddings = text.Length % 4;
            if (paddings > 0)
            {
                text += new string('=', 4 - paddings);
            }

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(text);
            string returnVal = System.Text.UTF8Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnVal;
        }
    }
}
