using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Messages.NET.Utils
{
    static class Encryption
    {
        public static string EncryptAES(string clearText, string strKey, string strIv)
        {
            // Place le texte à chiffrer dans un tableau d'octets
            byte[] plainText = Encoding.Default.GetBytes(clearText);
            // Place la clé de chiffrement dans un tableau d'octets
            byte[] key = Encoding.Default.GetBytes(strKey);
            // Place le vecteur d'initialisation dans un tableau d'octets
            byte[] iv = Encoding.UTF8.GetBytes(strIv);
            RijndaelManaged rijndael = new RijndaelManaged();
            // Définit le mode utilisé
            rijndael.Mode = CipherMode.CBC;
            // Crée le chiffreur AES - Rijndael
            ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(key, iv);
            MemoryStream ms = new MemoryStream();
            // Ecris les données chiffrées dans le MemoryStream
            CryptoStream cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
            cs.Write(plainText, 0, plainText.Length);
            cs.FlushFinalBlock();
            // Place les données chiffrées dans un tableau d'octet
            byte[] CipherBytes = ms.ToArray();
            ms.Close();
            cs.Close();
            // Place les données chiffrées dans une chaine encodée en Base64
            return Convert.ToBase64String(CipherBytes);
        }

        public static string DecryptAES(string cipherText, string strKey, string strIv)
        {
            // Place le texte à déchiffrer dans un tableau d'octets
            byte[] cipheredData = Convert.FromBase64String(cipherText);
            // Place la clé de déchiffrement dans un tableau d'octets
            byte[] key = Encoding.UTF8.GetBytes(strKey);
            // Place le vecteur d'initialisation dans un tableau d'octets
            byte[] iv = Encoding.UTF8.GetBytes(strIv);
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            // Ecris les données déchiffrées dans le MemoryStream
            ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
            MemoryStream ms = new MemoryStream(cipheredData);
            CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            // Place les données déchiffrées dans un tableau d'octet
            byte[] plainTextData = new byte[cipheredData.Length];
            int decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);
            ms.Close();
            cs.Close();
            return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);
        }

        public static void generateAes(out string key, out string iv)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.GenerateKey();
                rijndael.GenerateIV();
                key = Encoding.Default.GetString(rijndael.Key);
                iv = Encoding.Default.GetString(rijndael.IV);
            }

        }
        
        //public static string EncryptRSA(string pubKey, string message)
        //{
        //    byte[] bytesKey = Encoding.UTF8.GetBytes(pubKey);
        //    RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);         

        //    RSAParameters RSAKeyInfo = csp.ExportParameters(false);            
        //    RSAKeyInfo.Modulus = bytesKey;

        //    csp.ImportParameters(RSAKeyInfo);

        //    var bytesMessage = Encoding.UTF8.GetBytes(message);
        //    var bytesCypherText = csp.Encrypt(bytesMessage, false);
        //    return Convert.ToBase64String(bytesCypherText);
        //}

        //public static string DecryptRSA(string privKey, string cypherText)
        //{
        //    byte[] bytesCypherText = Convert.FromBase64String(cypherText);
        //    RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        //    RSAParameters RSAKeyInfo = csp.ExportParameters(true);

        //    csp = new RSACryptoServiceProvider();
        //    csp.ImportParameters(RSAKeyInfo);

        //    bytesCypherText = csp.Decrypt(bytesCypherText, false);
        //    return Encoding.Unicode.GetString(bytesCypherText);
        //}

        //public static string generatePubKey()
        //{
        //    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //    return RSA.ToXmlString(false);
        //}

        //public static string generatePrivKey()
        //{
        //    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //    return RSA.ToXmlString(true);
        //}

        public static void Test(out String publicKey, out String privateKey)
        {
            var csp = new RSACryptoServiceProvider();  // génére une paire de clefs (privée,publique)
            publicKey = csp.ToXmlString(false); // récupère un xml représentant la clef publique
            privateKey = csp.ToXmlString(true); //récupère un xml représentant l’ensemble des information du trousseau
            var plainTextData = "foobar"; //texte à chiffrer
            var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData); //encode les data en tableau de byte
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false); // chiffre
            var cypherText = Convert.ToBase64String(bytesCypherText); //passage en base64 pour le stoquage ou le transport

            bytesCypherText = Convert.FromBase64String(cypherText); //convertit de la base 64 en tableau de byte 
            csp = new RSACryptoServiceProvider();//instancie le crypto proviter 
            csp.FromXmlString(privateKey);
            bytesPlainTextData = csp.Decrypt(bytesCypherText, false); //déchiffrement
            String ntm = System.Text.Encoding.Unicode.GetString(bytesPlainTextData); //passage en en text

            Console.WriteLine("----------------------------------");
            Console.WriteLine(ntm);
        }

        public static void GenerateRSA(out String publicKey, out String privateKey)
        {
            var csp = new RSACryptoServiceProvider();  
            publicKey = csp.ToXmlString(false);
            privateKey = csp.ToXmlString(true);
        }

        public static String EncryptRSA(String publicKey, String message)            
        {
            var csp = new RSACryptoServiceProvider();
            var sr = new System.IO.StringReader(publicKey);

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "RSAKeyValue";
            xRoot.IsNullable = false;

            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters), xRoot);


            var pubKey = (RSAParameters)xs.Deserialize(sr);
            csp.ImportParameters(pubKey);

            var bytesMessage = System.Text.Encoding.Unicode.GetBytes(message);
            var bytesCypherText = csp.Encrypt(bytesMessage, false);
            var cypherText = Convert.ToBase64String(bytesCypherText);

            return cypherText;
        }

        public static String DecryptRSA(String privateKey, String cypherText)
        {
            var bytesCypherText = Convert.FromBase64String(cypherText);
            var csp = new RSACryptoServiceProvider();
            
            var sr = new System.IO.StringReader(privateKey);

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "RSAKeyValue";
            xRoot.IsNullable = false;

            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters), xRoot);
            var privKey = (RSAParameters)xs.Deserialize(sr);
            csp.ImportParameters(privKey);

            var bytesDecrypted = csp.Decrypt(bytesCypherText, false);

            return System.Text.Encoding.Unicode.GetString(bytesDecrypted);
        }

    }
}
