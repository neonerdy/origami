
/*===========================================================
 * 
 * Origami (Object Relational Gateway Microarchitecture)
 * 
 * Lightweight Enterprise Application Framework
 *
 * Version  : 3.0
 * Author   : Ariyanto
 * E-Mail   : neonerdy@yahoo.com
 *  
 * 
 * © 2009, Under Apache Licence 
 * 
 *==========================================================
 */

using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Origami.Security.Cryptography
{
    public class AsymmetricCryptography
    {
        private string privateKey;
        private string publicKey;
        private string privateKeyStream;
        private string publicKeyStream;

        private RSACryptoServiceProvider rsaCipher;

        public string PrivateKey
        {
            get { return privateKey; }
            set
            {
                privateKey = value;
                privateKeyStream = ReadFromFile(privateKey);
            }
        }

        public string PublicKey
        {
            get { return publicKey; }
            set
            {
                publicKey = value;
                publicKeyStream = ReadFromFile(publicKey);
            }
        }


        private string ReadFromFile(string fileName)
        {
            FileStream file = null;
            string stream;
            try
            {
                file = new FileStream(fileName, FileMode.Open);
                StreamReader reader = new StreamReader(file);
                stream = reader.ReadToEnd();
                reader.Close();
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return stream;

        }

        private void WriteFile(string fileName, string stream)
        {
            StreamWriter file = new StreamWriter(fileName, true);
            file.WriteLine(stream);
            file.Close();
        }


        public void GenerateKeyPairs(string publicKeyFileName, string privateKeyFileName)
        {
            rsaCipher = new RSACryptoServiceProvider();
            string xmlPublicKey = rsaCipher.ToXmlString(false);
            string xmlPrivateKey = rsaCipher.ToXmlString(true);

            WriteFile(publicKeyFileName, xmlPublicKey);
            WriteFile(privateKeyFileName, xmlPrivateKey);
        }


        public string Encrypt(string source)
        {
            rsaCipher = null;
            string cipheredText;

            try
            {
                rsaCipher = new RSACryptoServiceProvider();

                string publicKey = this.publicKeyStream;
                rsaCipher.FromXmlString(this.publicKeyStream);

                byte[] plainBytes = Encoding.ASCII.GetBytes(source);

                byte[] cipheredBytes = rsaCipher.Encrypt(plainBytes, true);
                cipheredText = Convert.ToBase64String(cipheredBytes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return cipheredText;
        }


        public string Decrypt(string source)
        {
            rsaCipher = new RSACryptoServiceProvider();

            rsaCipher.FromXmlString(this.privateKeyStream);
            byte[] cipheredBytes = Convert.FromBase64String(source);

            byte[] decipheredBytes = rsaCipher.Decrypt(cipheredBytes, true);
            string decipheredText = Encoding.ASCII.GetString(decipheredBytes);

            return decipheredText;

        }




    }
}
