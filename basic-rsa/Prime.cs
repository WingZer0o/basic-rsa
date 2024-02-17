using System.Numerics;
using System.Security.Cryptography;

namespace basic_rsa
{
    public static class Prime
    {
        public static bool IsPrime(BigInteger number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i <= BigIntHelper.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int GenerateRandomPrime()
        {
            Random rnd = new Random();

            while (true)
            {
                int candidate = rnd.Next();
                if (IsPrime(candidate))
                    return candidate;
            }
        }

        public static BigInteger GenerateProbablePrime(int bitLength)
        {
            byte[] randomBytes = new byte[bitLength / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            BigInteger probablePrime = new BigInteger(randomBytes);
            probablePrime |= BigInteger.One; // Ensure it is odd

            // Use a simple primality test (not suitable for production)
            while (!IsProbablePrime(probablePrime, 5))
            {
                probablePrime += 2; // Ensure it stays odd
            }

            return probablePrime;
        }

        // Helper method to check probable primality (not suitable for production)
        static bool IsProbablePrime(BigInteger n, int certainty = 5)
        {
            if (n == 2 || n == 3)
                return true;
            if (n < 2 || n % 2 == 0)
                return false;

            BigInteger d = n - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            byte[] bytes = new byte[n.ToByteArray().LongLength];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                for (int i = 0; i < certainty; i++)
                {
                    rng.GetBytes(bytes);
                    BigInteger a = new BigInteger(bytes);
                    BigInteger x = BigInteger.ModPow(a, d, n);

                    if (x == 1 || x == n - 1)
                        continue;

                    for (int r = 1; r < s; r++)
                    {
                        x = BigInteger.ModPow(x, 2, n);
                        if (x == 1)
                            return false;
                        if (x == n - 1)
                            break;
                    }

                    if (x != n - 1)
                        return false;
                }
            }

            return true;
        }
    }
}
