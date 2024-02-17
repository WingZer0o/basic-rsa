using System.Numerics;

namespace basic_rsa
{
    public static class BigIntHelper
    {
        public static BigInteger Sqrt(BigInteger value)
        {
            if (value < 0)
            {
                throw new ArithmeticException("Cannot calculate square root of a negative number.");
            }

            if (value == 0 || value == 1)
            {
                return value;
            }

            BigInteger x = value / 2;
            BigInteger prev;

            do
            {
                prev = x;
                x = (x + value / x) / 2;
            } while (x < prev);

            return prev;
        }
    }
}
