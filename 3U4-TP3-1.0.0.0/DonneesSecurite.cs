// https://crackstation.net/
// https://www.mscs.dal.ca/~selinger/md5collision/

using System;
using System.Text;
using System.Security.Cryptography; 
using Encryption.Blowfish; // blowfish
// ajout de la librairie Bcrypt
using BCrypt.Net;
namespace consoleApp
{
    class DonneesSecurite
    {

        // ========== BLOWFISH ENCRYPTION ==========

        private static string key = "a3bd614b27864e3f854b971f9df1a802";
        private static byte[] iv = new byte[] { 23, 56, 45, 67, 78, 89, 90, 12 };

        public static string Encrypter(string source)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(source);
            buffer = buffer.CopyAndPadIfNotAlreadyPadded();
            var ctr = new BlowfishCtr(key);
            ctr.CryptOrDecrypt(buffer, iv);
            return Convert.ToBase64String(buffer);
        }

        public static string Decrypter(string source)
        {
            byte[] buffer = Convert.FromBase64String(source);
            var ctr = new BlowfishCtr(key);
            ctr.CryptOrDecrypt(buffer, iv);
            return Encoding.UTF8.GetString(buffer).TrimEnd('\0');
        }   
        // ========== BLOWFISH ENCRYPTION FIN ==========


        // fonctions permettant le hachage des mots de passe
        public static string HacherLeMotDePasse(string input)
        {
                return BCrypt.Net.BCrypt.HashPassword(input, workFactor: 12); 
        }
        
        public static bool VerifierLeMotDePasse(string motDePasse, string hache)
        {
            // Calcule le hachage MD5 du mot de passe fourni et le compare avec le haché stocké
            return BCrypt.Net.BCrypt.Verify(motDePasse,hache);
        }
        
    }
}
