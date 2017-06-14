using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Security.Cryptography; 

namespace Demolition.Models
{
    public partial class User : Model, IPrincipal
    {
        public enum Roles { Administrator, Salesperson }

        public static IList<User> ListAll()
        {
            var users = from u in GetDataContext().Users
                        select u;
            return users.ToList();
        }

        public IIdentity Identity
        {
            get { return new GenericIdentity(Name); }
        }

        public bool IsInRole(string role)
        {
            return Role == role;
        }

        public static User Find(string username, string password)
        {
            User ourUser = GetDataContext().Users.SingleOrDefault(d => d.Name == username);

            if (ourUser != null && password == ourUser.Password)
                return ourUser;
            else
                return null;
        }

        public static User Create(string username, string password, string email, Roles role)
        {
            var newUser = new User();
            newUser.Email = email;
            newUser.Name = username;
            newUser.Password = password;
            newUser.Role = role.ToString();
            newUser.CreatedAt = newUser.UpdatedAt = DateTime.Now;

            var context = GetDataContext();
            context.Users.InsertOnSubmit(newUser);
            context.SubmitChanges();

            return newUser;
        }

        public bool ChangePassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public static string EncryptPassword(string message, string passPhrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
 
            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
 
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passPhrase));
 
            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
 
            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
 
            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(message);
 
            // Step 5. Attempt to encrypt the string
           try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
           }

           // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public static string DecryptPassword(string message, string passPhrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
 
            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
 
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passPhrase));
 
           // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
 
            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(message);
 
           // Step 5. Attempt to decrypt the string
            try
           {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
           {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString( Results );
        }
    }
}
