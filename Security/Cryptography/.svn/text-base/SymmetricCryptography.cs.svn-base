
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
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Origami.Security.Cryptography
{
    public class SymmetricCryptography
    {
        private SymmetricAlgorithm symetricCryptoService;
        private string symetricAlgorithm;
        private string key;
        private string salt;

        public SymmetricCryptography(SymmetricProvider symetricProvider)
        {
            switch (symetricProvider)
            {
                case SymmetricProvider.DES:
                    symetricCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmetricProvider.RC2:
                    symetricCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmetricProvider.Rijndael:
                    symetricCryptoService = new RijndaelManaged();
                    break;
                case SymmetricProvider.TripleDES:
                    symetricCryptoService = new TripleDESCryptoServiceProvider();
                    break;
            }
        }

        public SymmetricCryptography(SymmetricAlgorithm symetricAlgorithm)
        {
            this.symetricCryptoService = symetricAlgorithm;
        }


        public SymmetricCryptography()
        {

        }

        private SymmetricAlgorithm SelectAlgorithm()
        {
            if (symetricAlgorithm.Equals(SymmetricProvider.DES.ToString()))
            {
                symetricCryptoService = new DESCryptoServiceProvider();
            }
            else if (symetricAlgorithm.Equals(SymmetricProvider.RC2.ToString()))
            {
                symetricCryptoService = new RC2CryptoServiceProvider();
            }
            else if (symetricAlgorithm.Equals(SymmetricProvider.Rijndael.ToString()))
            {
                symetricCryptoService = new RijndaelManaged();
            }
            else if (symetricAlgorithm.Equals(SymmetricProvider.TripleDES.ToString()))
            {
                symetricCryptoService = new TripleDESCryptoServiceProvider();
            }

            return symetricCryptoService;

        }

        public string Algorithm
        {
            get { return symetricAlgorithm; }
            set
            {
                symetricAlgorithm = value;
                symetricCryptoService = SelectAlgorithm();
            }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public string Salt
        {
            get { return salt; }
            set { salt = value; }
        }

        public string Encrypt(string source)
        {
            Rfc2898DeriveBytes rfc = null;

            byte[] plainBytes = plainBytes = Encoding.ASCII.GetBytes(source);

            if (key != null && salt != null)
            {
                rfc = new Rfc2898DeriveBytes(key, Encoding.ASCII.GetBytes(salt));
            }
            else
            {
                throw new Exception("'Key' or 'Salt' Property is null");
            }

            symetricCryptoService.Key = rfc.GetBytes(symetricCryptoService.KeySize / 8);
            symetricCryptoService.IV = rfc.GetBytes(symetricCryptoService.BlockSize / 8);

            MemoryStream strCiphered = new MemoryStream();

            CryptoStream strCrypto = new CryptoStream(strCiphered,
                symetricCryptoService.CreateEncryptor(), CryptoStreamMode.Write);

            strCrypto.Write(plainBytes, 0, plainBytes.Length);
            strCrypto.Close();
            string chipered = Convert.ToBase64String(strCiphered.ToArray());

            strCiphered.Close();

            return chipered;


        }

        public string Decrypt(string source)
        {

            Rfc2898DeriveBytes rfc = null;
            byte[] cipheredBytes = Convert.FromBase64String(source);

            if (key != null && salt != null)
            {
                rfc = new Rfc2898DeriveBytes(key, Encoding.ASCII.GetBytes(salt));
            }
            else
            {
                throw new Exception("'Key' or 'Salt' Property is null");
            }

            symetricCryptoService.Key = rfc.GetBytes(symetricCryptoService.KeySize / 8);
            symetricCryptoService.IV = rfc.GetBytes(symetricCryptoService.BlockSize / 8);

            MemoryStream strDeciphered = new MemoryStream();

            CryptoStream strCrypto = new CryptoStream(strDeciphered,
                symetricCryptoService.CreateDecryptor(), CryptoStreamMode.Write);

            strCrypto.Write(cipheredBytes, 0, cipheredBytes.Length);
            strCrypto.Close();
            string deciphered = Encoding.ASCII.GetString(strDeciphered.ToArray());

            strDeciphered.Close();

            return deciphered;

        }




    }
}
