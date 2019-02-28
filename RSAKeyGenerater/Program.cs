using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSAKeyGenerater
{
    class Program
    {
        string publicKeyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PublicKey.xml");
        string privateKeyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrivateKey.xml");

        static void Main(string[] args)
        {
            var p = new Program();
            Console.WriteLine("秘钥长度2048生成中...");
            p.GenerateKeys(2048);
            Console.WriteLine("秘钥生成中完成");
            Console.ReadLine();
        }

        public void GenerateKeys(int rsaKeySize)
        {
            using (var rsa = new RSACryptoServiceProvider(rsaKeySize))
            {
                try
                {
                    // 获取私钥和公钥。
                    var publicKey = rsa.ToXmlString(false);
                    var privateKey = rsa.ToXmlString(true);

                    // 保存到磁盘
                    File.WriteAllText(publicKeyFileName, publicKey);
                    File.WriteAllText(privateKeyFileName, privateKey);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
