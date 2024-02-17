using System.Numerics;
using System.Text;

namespace basic_rsa
{
    public static class PEM
    {
        public static string ToPEMFormat(BigInteger modulus, BigInteger exponent, bool isPublic)
        {
            // Convert the modulus and exponent to byte arrays
            byte[] modulusBytes = modulus.ToByteArray();
            byte[] exponentBytes = exponent.ToByteArray();

            // Create the PEM string with base64 encoding
            string base64Modulus = Convert.ToBase64String(modulusBytes);
            string base64Exponent = Convert.ToBase64String(exponentBytes);

            StringBuilder pemBuilder = new StringBuilder();
            if (isPublic)
            {
                pemBuilder.AppendLine("-----BEGIN PUBLIC KEY-----");
                pemBuilder.AppendLine("Modulus: " + base64Modulus);
                pemBuilder.AppendLine("Exponent: " + base64Exponent);
                pemBuilder.AppendLine("-----END "  + "-----");

            }
            else
            {
                pemBuilder.AppendLine("-----BEGIN PRIVATE KEY-----");
                pemBuilder.AppendLine("Modulus: " + base64Modulus);
                pemBuilder.AppendLine("Exponent: " + base64Exponent);
                pemBuilder.AppendLine("-----END PRIVATE KEY-----");
            }
            return pemBuilder.ToString();
        }
    }
}
