using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WebPrint.Framework
{
    public sealed class TripleDes
    {
        private readonly string iv;
        private readonly string key;
        private readonly TripleDESCryptoServiceProvider dcsp;

        public TripleDes(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
            dcsp = new TripleDESCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    IV = Encoding.UTF8.GetBytes(this.iv),
                    Key = Encoding.UTF8.GetBytes(this.key)
                };
        }

        public string Encrypt(string value)
        {
            var ct = dcsp.CreateEncryptor(dcsp.Key, dcsp.IV);
            var bytes = Encoding.UTF8.GetBytes(value);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            cs.Close();

            return Utils.ByteToHex(ms.ToArray());
        }

        public string Decrypt(string value)
        {
            var ct = dcsp.CreateDecryptor(dcsp.Key, dcsp.IV);
            var bytes = Utils.HexToByte(value);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
