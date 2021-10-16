using System;
using System.Security.Cryptography;
using System.Text;

namespace turismo_real_controller.Hasher
{
    public class HashPassword
    {
        public string HashSHA256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256Hash.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hashedPassword;
            }
        }

    }
}
