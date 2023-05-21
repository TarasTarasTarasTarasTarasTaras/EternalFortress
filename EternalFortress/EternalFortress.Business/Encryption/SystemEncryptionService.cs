using EternalFortress.Entities.Options;
using System.Text;

namespace EternalFortress.Business.Encryption
{
    public class SystemEncryptionService : IEncryptionService
    {
        private readonly byte[] _systemKey;
        public SystemEncryptionService(EncryptionOptions options)
        {
            var encryptionKey = options.EncryptionKey;
            _systemKey = GetSystemKey(encryptionKey);
        }

        public byte[] Encrypt(string data)
        {
            if (string.IsNullOrEmpty(data)) return null;

            var bytes = GetBytes(data);
            return AesEncryptionService.Encrypt(bytes, _systemKey);
        }

        public byte[] Encrypt(string data, Encoding encoding)
        {
            var buffer = encoding.GetBytes(data);
            return AesEncryptionService.Encrypt(buffer, _systemKey);
        }

        public string Decrypt(byte[] data)
        {
            if (string.IsNullOrEmpty(Convert.ToString(data)))
                return null;

            var decryptedBytes = AesEncryptionService.Decrypt(data, _systemKey);
            return GetString(decryptedBytes);
        }

        public string Decrypt(byte[] data, Encoding encoding)
        {
            if (string.IsNullOrEmpty(Convert.ToString(data)))
                return null;

            var decryptedBytes = AesEncryptionService.Decrypt(data, _systemKey);
            return encoding.GetString(decryptedBytes);
        }

        private byte[] GetBytes(string str)
        {
            if (str is null) return Array.Empty<byte>();

            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string GetString(byte[] bytes)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(bytes)))
            {
                var chars = new char[bytes.Length / sizeof(char)];
                Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
            }
            else
            {
                return null;
            }
        }

        private byte[] GetSystemKey(string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new InvalidOperationException($"Invalid Encryption Key");
            }

            return Convert.FromBase64String(encryptionKey);
        }
    }
}
