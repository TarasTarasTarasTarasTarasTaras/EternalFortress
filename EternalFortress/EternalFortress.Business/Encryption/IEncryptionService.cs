using System.Text;

namespace EternalFortress.Business.Encryption
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string data);
        byte[] Encrypt(string data, Encoding encoding);
        string Decrypt(byte[] data);

        string Decrypt(byte[] data, Encoding encoding);
    }
}
