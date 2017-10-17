using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Blog.Libraries.Data.Settings;

namespace Blog.Libraries.Services.Security
{
    public class EncryptionService : IEncryptionService
    {

        #region Fields

        private readonly SecuritySettings _securitySettings;

        #endregion

        #region Constructor

        public EncryptionService(SecuritySettings securitySettings)
        {
            _securitySettings = securitySettings;
        }

        #endregion

        #region Methods

        public string CreateSaltKey(int size)
        {
            //生成加密随机数
            using (var provider = new RNGCryptoServiceProvider())
            {
                var buff = new byte[size];
                provider.GetBytes(buff);

                // 返回一个Base64字符串表示的随机数
                return Convert.ToBase64String(buff);
            }
        }

        public string CreateHash(string text, string saltkey, string hashAlgorithm = "SHA1")
        {
            return CreateHash(Encoding.UTF8.GetBytes(text + saltkey), hashAlgorithm);
        }

        public string CreateHash(byte[] data, string hashAlgorithm = "SHA1")
        {
            if (String.IsNullOrEmpty(hashAlgorithm))
                hashAlgorithm = "SHA1";

            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(data);
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _securitySettings.EncryptionKey;

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

                byte[] encryptedBinary = EncryptTextToMemory(plainText, provider.Key, provider.IV);
                return Convert.ToBase64String(encryptedBinary);
            }
        }

        public string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _securitySettings.EncryptionKey;

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

                byte[] buffer = Convert.FromBase64String(cipherText);
                return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
            }
        }

        #endregion

        #region Utilities

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = Encoding.Unicode.GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs, Encoding.Unicode))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        #endregion

    }
}
