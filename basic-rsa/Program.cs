using basic_rsa;
using System;
using System.Numerics;
using System.Text;

class Program
{
    static void Main()
    {
        // Generate public and private keys
        RSAKeyPair keyPair = GenerateKeyPair(1024); // 1024-bit key size for demonstration

        Console.WriteLine("Public Key:\n" + keyPair.PublicKey);
        Console.WriteLine("\nPrivate Key:\n" + keyPair.PrivateKey);

        // Example usage for encryption and decryption
        string plaintext = "Hello, RSA!";
        BigInteger encrypted = Encrypt(plaintext, keyPair.PublicKey, keyPair.Modulus);
        string encryptedString = Encoding.UTF8.GetString(encrypted.ToByteArray());
        string decrypted = Decrypt(encrypted, keyPair.PrivateKey, keyPair.Modulus);

        Console.WriteLine("\nOriginal: " + plaintext);
        Console.WriteLine("Encrypted: " + encrypted);
        Console.WriteLine("Decrypted: " + decrypted);
    }

    // Represents an RSA key pair (public key and private key)
    class RSAKeyPair
    {
        public BigInteger PublicKey { get; set; }
        public BigInteger PrivateKey { get; set; }
        public BigInteger Modulus { get; set; }
    }

    // Generates an RSA key pair with the specified bit length
    static RSAKeyPair GenerateKeyPair(int bitLength)
    {
        // Step 1: Choose two large prime numbers p and q
        BigInteger p = GeneratePrimeNumber(bitLength / 2);
        BigInteger q = GeneratePrimeNumber(bitLength / 2);

        // Step 2: Calculate n = p * q
        BigInteger n = p * q;

        // Step 3: Calculate Euler's totient function phi(n) = (p-1) * (q-1)
        BigInteger phi = (p - 1) * (q - 1);

        // Step 4: Choose a public exponent e such that 1 < e < phi(n) and e is coprime to phi(n)
        BigInteger e = FindCoprime(phi);

        // Step 5: Calculate the private exponent d such that d ≡ e^(-1) (mod phi(n))
        BigInteger d = ModInverse(e, phi);

        return new RSAKeyPair { PublicKey = e, PrivateKey = d, Modulus = n };
    }

    // Encrypts a plaintext string using the provided public key
    static BigInteger Encrypt(string plaintext, BigInteger publicKey, BigInteger modulus)
    {
        BigInteger m = new BigInteger(Encoding.UTF8.GetBytes(plaintext));
        return BigInteger.ModPow(m, publicKey, modulus);
    }

    // Decrypts an encrypted BigInteger using the provided private key
    static string Decrypt(BigInteger encrypted, BigInteger privateKey, BigInteger modulus)
    {
        BigInteger m = BigInteger.ModPow(encrypted, privateKey, modulus);
        return Encoding.UTF8.GetString(m.ToByteArray());
    }

    // Helper method to generate a large prime number
    static BigInteger GeneratePrimeNumber(int bitLength)
    {
        // Implement your preferred method for generating large prime numbers
        // For simplicity, a basic method is used here (not suitable for production)
        return Prime.GenerateProbablePrime(bitLength);
    }

    // Helper method to find a coprime to a given number
    static BigInteger FindCoprime(BigInteger phi)
    {
        // For simplicity, a basic method is used here (not suitable for production)
        BigInteger e = 65537; // Common choice for e in practice
        while (BigInteger.GreatestCommonDivisor(e, phi) != 1)
        {
            e++;
        }
        return e;
    }

    // Helper method to calculate the modular inverse
    static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        // Extended Euclidean Algorithm
        BigInteger m0 = m;
        BigInteger y = 0, x = 1;

        while (a > 1)
        {
            BigInteger q = a / m;
            BigInteger t = m;

            m = a % m;
            a = t;
            t = y;

            y = x - q * y;
            x = t;
        }

        // Make x positive
        if (x < 0)
        {
            x += m0;
        }

        return x;
    }
}
