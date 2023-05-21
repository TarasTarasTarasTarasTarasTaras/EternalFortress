using System;
using System.IO;
using System.Security.Cryptography;

namespace EternalFortress.Business.Encryption
{
    internal static class AesEncryptionService
    {
        public static byte[] Encrypt(byte[] data, byte[] encryptionKey)
        {
            using var aes = new AesCryptoServiceProvider();

            try
            {
                aes.Key = encryptionKey;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                
                using var ms = new MemoryStream();
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
            catch
            {
                throw new InvalidOperationException("Error from Encrypt in AESEncryptionService");
            }
        }

        public static byte[] Decrypt(byte[] data, byte[] encryptionKey)
        {
            try
            {
                using var aes = new AesCryptoServiceProvider();
                aes.Key = encryptionKey;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var dataStream = new MemoryStream(data);

                var iv = new byte[aes.IV.Length];
                dataStream.Read(iv, 0, aes.IV.Length);

                using var decryptor = aes.CreateDecryptor(aes.Key, iv);
                using var cs = new CryptoStream(dataStream, decryptor, CryptoStreamMode.Read);
                var buffer = new byte[4096];

                using var reader = new BinaryReader(cs);
                using var outStream = new MemoryStream();

                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    outStream.Write(buffer, 0, count);
                }

                return outStream.ToArray();
            }
            catch
            {
                throw new InvalidOperationException("Error from Decrypt in AESEncryptionService");
            }
        }

    }
}
