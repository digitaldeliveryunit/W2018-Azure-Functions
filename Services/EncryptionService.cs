using System;
using System.Security.Cryptography;
using System.Text;

namespace com.petronas.myevents.api.Helpers
{
    public static class EncryptionService
    {
        private static readonly byte[] key = new byte[8] {1, 2, 3, 4, 5, 6, 7, 8};
        private static readonly byte[] iv = new byte[8] {1, 2, 3, 4, 5, 6, 7, 8};

        public static string Crypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            var transform = algorithm.CreateEncryptor(key, iv);
            var inputbuffer = Encoding.Unicode.GetBytes(text);
            var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Base64UrlEncode(Convert.ToBase64String(outputBuffer));
        }

        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            var transform = algorithm.CreateDecryptor(key, iv);
            var inputbuffer = Convert.FromBase64String(Base64UrlDecode(text));
            var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }

        private static string Base64UrlEncode(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var bytesToEncode = Encoding.UTF8.GetBytes(text);
            var returnVal = Convert.ToBase64String(bytesToEncode);

            return returnVal.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        private static string Base64UrlDecode(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            text.Replace('-', '+');
            text.Replace('_', '/');

            var paddings = text.Length % 4;
            if (paddings > 0) text += new string('=', 4 - paddings);

            var encodedDataAsBytes = Convert.FromBase64String(text);
            var returnVal = Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnVal;
        }
    }
}