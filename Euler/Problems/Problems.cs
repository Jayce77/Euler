using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Euler.Problems
{
    public class Problems
    {
        public int Problem1Solve(int upperLimit)
        {
            int sum = 0;
            for (int i = 0; i < upperLimit; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }
            return sum;
        }

        public int Problem2Solve(int upperLimit)
        {
            int sum = 0;
            int fibseqPrev2 = 0;
            int fibseqPrev1 = 1;

            while (fibseqPrev1 + fibseqPrev2 < upperLimit)
            {
                int fibSeq = fibseqPrev1 + fibseqPrev2;
                if (fibSeq % 2 == 0)
                {
                    sum += fibSeq;
                }
                fibseqPrev2 = fibseqPrev1;
                fibseqPrev1 = fibSeq;
            }
            return sum;
        }

        public long LargestPrimeFactor(long value)
        {
            List<long> knownPrimes = FindPrimes((long)Math.Sqrt(value));
            List<long> foundFactors = FindPrimeFactors(value).ToList();
            return foundFactors.Max();
        }

        public IEnumerable<int> FindPrimeFactors(int value)
        {
            List<int> knownPrimes = PrimeSieve2((int)Math.Sqrt(value));
            List<int> foundFactors = new List<int>();
            while (value > 1)
            {
                for (int i = 0; i < knownPrimes.Count; i++)
                {
                    if (value % knownPrimes[i] == 0)
                    {
                        foundFactors.Add(knownPrimes[i]);
                        value = value / knownPrimes[i];
                    }
                }
            }
            return foundFactors;
        }

        public IEnumerable<long> FindPrimeFactors(long value)
        {
            List<long> knownPrimes = PrimeSieve2(value);
            List<long> foundFactors = new List<long>();
            while (value > 1)
            {
                for (int i = 0; i < knownPrimes.Count; i++)
                {
                    if (value % knownPrimes[i] == 0)
                    {
                        foundFactors.Add(knownPrimes[i]);
                        value = value / knownPrimes[i];
                    }
                }
            }
            return foundFactors;
        }

        public List<long> FindPrimes(long value)
        {
            List<long> foundPrimes = new List<long>();
            Console.WriteLine($"Searching for Primes");
            for (long i = 2; i < value; i++)
            {
                bool isPrime = true;
                for (int j = 0; j < foundPrimes.Count; j++)
                {
                    if (i % foundPrimes[j] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    foundPrimes.Add(i);
                }
            }
            Console.WriteLine($"Retunring for Primes");
            return foundPrimes;
        }

        public List<int> FindPrimes(int value)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<int> foundPrimes = new List<int>();

            for (int i = 2; i < value; i++)
            {
                bool isPrime = true;
                for (int j = 0; j < foundPrimes.Count; j++)
                {
                    if (i % foundPrimes[j] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    foundPrimes.Add(i);
                }
            }

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            return foundPrimes;
        }

        public int Problem4Solve()
        {
            int threeDigitMax = 999;
            int maxValue = 0;

            for (int i = threeDigitMax; i > 99; i--)
            {
                for (int j = threeDigitMax; j > 99; j--)
                {
                    if (i * j < maxValue)
                    {
                        break;
                    }
                    int value = i * j;
                    int reverseBackHalf = (100 * (value % 10)) 
                        + (10 * ((value / 10) % 10)) 
                        + ((value / 100) % 10);
                    int frontHalf = (int)value / 1000;

                    if (frontHalf == reverseBackHalf)
                    {
                        if (value > maxValue)
                        {
                            maxValue = value;
                        };
                    }
                }
                if (maxValue > i * threeDigitMax)
                {
                    break;
                }
            }
            return maxValue;
        }

        public int Problem5Solve(int value)
        {
            int lcm = 1;
            Dictionary<int, int> primeFactorFrequencyTable = new Dictionary<int, int>();
            List<int> primes = FindPrimes(value);

            for (int i = 0; i < primes.Count; i++)
            {
                primeFactorFrequencyTable.Add(primes[i], 1);
            }

            for (int i = 2; i <= value; i++)
            {
                if (primeFactorFrequencyTable.ContainsKey(i))
                {
                    continue;
                }

                int currentValue = i;
                Dictionary<int, int> tempTable = new Dictionary<int, int>();

                for (int j = 0; j < primes.Count; j++)
                {
                    if (primes[j] > currentValue)
                    {
                        break;
                    }
                    if (currentValue % primes[j] == 0)
                    {
                        if (!tempTable.ContainsKey(primes[j]))
                        {
                            tempTable.Add(primes[j], 1);
                        }
                        else
                        {
                            tempTable[primes[j]]++;
                            if (tempTable[primes[j]] > primeFactorFrequencyTable[primes[j]])
                            {
                                primeFactorFrequencyTable[primes[j]] = tempTable[primes[j]];
                            }
                        }
                        currentValue = currentValue / primes[j];
                        j = -1;
                    }
                    if (currentValue == 1) break;
                }
            }
            for (int i = 0; i < primes.Count; i++)
            {
                lcm = lcm * (int)Math.Pow(primes[i], (float)primeFactorFrequencyTable[primes[i]]);
            }
            return lcm;
        }

        public int Problem6Solve(int value)
        {
            int factoral = (value * (value + 1)) / 2;
            int squareOfSum = (int)Math.Pow(factoral, 2);

            int sumOfSquare = 0;

            for (int i = 1; i <= value; i++)
            {
                sumOfSquare += (int)Math.Pow(i, 2);
            }

            return squareOfSum - sumOfSquare;
        }

        public int Problem7Solve(int value)
        {
            List<int> primes = new List<int>();

            int currentValue = 2;
            while (primes.Count < value)
            {
                bool isPrime = true;
                for (int i = 0; i < primes.Count; i++)
                {
                    if (currentValue % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    Console.WriteLine(currentValue.ToString());
                    primes.Add(currentValue);
                }
                currentValue++;
            }
            return primes[value - 1];
        }

        public long Problem8Solve(string bigNumber)
        {
            long maxValue = 0;
            char[] bignumberChars = bigNumber.ToCharArray();
            long[] bigNumberDigits = Array.ConvertAll(bignumberChars, c => (long)Char.GetNumericValue(c));

            long value0 = 1;
            long value1 = 1;
            long value2 = 1;
            long value3 = 1;
            long value4 = 1;
            long value5 = 1;
            long value6 = 1;
            long value7 = 1;
            long value8 = 1;
            long value9 = 1;
            long value10 = 1;
            long value11 = 1;
            long value12 = 1;

            for (int i = 0; i < bigNumberDigits.Length; i++)
            {
                if ( i == 0 || i  % 13 == 0)
                {
                    if (maxValue < value0)
                    {
                        maxValue = value0;
                    }
                    value0 = bigNumberDigits[i];
                }
                else
                {
                    value0 = value0 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 1)
                {
                    if (maxValue < value1)
                    {
                        maxValue = value1;
                    }
                    value1 = bigNumberDigits[i];
                }
                else if (i > 0)
                {
                    value1 = value1 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 2)
                {
                    if (maxValue < value2)
                    {
                        maxValue = value2;
                    }
                    value2 = bigNumberDigits[i];
                }
                else if (i > 1)
                {
                    value2 = value2 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 3)
                {
                    if (maxValue < value3)
                    {
                        maxValue = value3;
                    }
                    value3 = bigNumberDigits[i];
                }
                else if (i > 2)
                {
                    value3 = value3 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 4)
                {
                    if (maxValue < value4)
                    {
                        maxValue = value4;
                    }
                    value4 = bigNumberDigits[i];
                }
                else if (i > 3)
                {
                    value4 = value4 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 5)
                {
                    if (maxValue < value5)
                    {
                        maxValue = value5;
                    }
                    value5 = bigNumberDigits[i];
                }
                else if (i > 4)
                {
                    value5 = value5 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 6)
                {
                    if (maxValue < value6)
                    {
                        maxValue = value6;
                    }
                    value6 = bigNumberDigits[i];
                }
                else if (i > 5)
                {
                    value6 = value6 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 7)
                {
                    if (maxValue < value7)
                    {
                        maxValue = value7;
                    }
                    value7 = bigNumberDigits[i];
                }
                else if (i > 6)
                {
                    value7 = value7 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 8)
                {
                    if (maxValue < value8)
                    {
                        maxValue = value8;
                    }
                    value8 = bigNumberDigits[i];
                }
                else if (i > 7)
                {
                    value8 = value8 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 9)
                {
                    if (maxValue < value9)
                    {
                        maxValue = value9;
                    }
                    value9 = bigNumberDigits[i];
                }
                else if (i > 8)
                {
                    value9 = value9 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 10)
                {
                    if (maxValue < value10)
                    {
                        maxValue = value10;
                    }
                    value10 = bigNumberDigits[i];
                }
                else if (i > 9)
                {
                    value10 = value10 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 11)
                {
                    if (maxValue < value11)
                    {
                        maxValue = value11;
                    }
                    value11 = bigNumberDigits[i];
                }
                else if (i > 10)
                {
                    value11 = value11 * bigNumberDigits[i];
                }
                if ( i > 0 && i  % 13 == 12)
                {
                    if (maxValue < value12)
                    {
                        maxValue = value12;
                    }
                    value12 = bigNumberDigits[i];
                }
                else if (i > 11)
                {
                    value12 = value12 * bigNumberDigits[i];
                }
            }
            return maxValue;
        }

        public double Problem9Solve(double value)
        {
            double a = 1.0;
            double b = 2.0;
            double c = 0.0;

            for (double i = 1.0; i < value; i++)
            {
                a = i;
                b = i + 1.0;
                c = 1000.0 - (a + b);

                while (b < c && Math.Pow(a, 2.0) + Math.Pow(b, 2.0) != Math.Pow(c, 2.0))
                {
                    b++;
                    c = 1000.0 - (a + b);
                }

                if (Math.Pow(a, 2.0) + Math.Pow(b, 2.0) == Math.Pow(c, 2.0))
                {
                    break;
                }
            } 
            return a * b * c;
        }

        public long Problem10Solve(long value)
        {
            List<long> primes = FindPrimes(value);
            return primes.Sum();
        }

        public List<int> PrimeSieve(int value)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<int> primes = new List<int> { 2 };
            int[] values = new int[value + 1];
            int currentNumber = 3;
            Console.WriteLine("Getting Primes");
            while (currentNumber <= value)
            {
                if (values[currentNumber] == 0)
                {
                    primes.Add(currentNumber);
                    int mark = currentNumber * 2;
                    while (mark < value)
                    {
                        values[mark] = 1;
                        mark += currentNumber;
                    }
                }
                currentNumber += 2;
            }

            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Returing Primes");
            return primes;
        }

        public List<int> PrimeSieve2(int value)
        {
            //Stopwatch sw = Stopwatch.StartNew();
            List<int> primes = new List<int> { 2 };
            int[] values = new int[value + 1];
            int currentNumber = 3;
            //Console.WriteLine("Getting Primes");
            while (currentNumber <= value)
            {
                if (values[currentNumber] == 0)
                {
                    primes.Add(currentNumber);
                    int mark = currentNumber * 2;
                    while (mark < value)
                    {
                        values[mark] = 1;
                        mark += currentNumber;
                    }
                }
                currentNumber += 2;
            }

            for (int i = (int)Math.Sqrt(value) + 1; i <= value; i+=2)
            {
                if (i % 2 == 0)
                {
                    i--;
                    continue;
                }

                if (values[i] == 0)
                {
                    primes.Add(i);
                }
            }

            //sw.Stop();
            //Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            //Console.WriteLine("Returing Primes");
            return primes;
        }

        public List<long> PrimeSieve2(long value)
        {
            //Stopwatch sw = Stopwatch.StartNew();
            List<long> primes = new List<long> { 2 };
            long[] values = new long[value + 1];
            long currentNumber = 3;
            //Console.WriteLine("Getting Primes");
            while (currentNumber <= Math.Sqrt(value))
            {
                if (values[currentNumber] == 0)
                {
                    primes.Add(currentNumber);
                    long mark = currentNumber * 2;
                    while (mark < value)
                    {
                        values[mark] = 1;
                        mark += currentNumber;
                    }
                }
                currentNumber += 2;
            }

            for (long i = (long)Math.Sqrt(value) + 1; i <= value; i += 2)
            {
                if (i % 2 == 0)
                {
                    i--;
                    continue;
                }

                if (values[i] == 0)
                {
                    primes.Add(i);
                }
            }

            //sw.Stop();
            //Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
            //Console.WriteLine("Returing Primes");
            return primes;
        }

        public long HighlyDivisibleTriangularNumber(long neededNumberOfDivisors)
        {
            long currentNumerOfDivisors = 0;
            long triangleNumber = 0;
            long count = 1;

            while (currentNumerOfDivisors < neededNumberOfDivisors + 1)
            {
                triangleNumber = CalcTriangleNumber(count);
                currentNumerOfDivisors = GetDivisors(triangleNumber).ToList().Count();
                Console.WriteLine($"Triangle Number: {triangleNumber} Number of Divsiors {currentNumerOfDivisors}");
                count++;
            }
            return triangleNumber;

        }

        public IEnumerable<long> GetDivisors(long number)
        {
            HashSet<long> divisors = new HashSet<long>();
            if (number == 1)
            {
                divisors.Add(1);
                return divisors;
            }

            for (int i = 1; i < number / 2; i++)
            {
                if (number % i == 0)
                {
                    divisors.Add(i);
                    divisors.Add(number / i);
                }
            }

            divisors.Add(1);
            divisors.Add(number);
            return divisors;
        }

        private int CalcTriangleNumber(int value)
        {
            return (value * (value + 1))/2;
        }

        private long CalcTriangleNumber(long value)
        {
            return (value * (value + 1)) / 2;
            var fetureFreaquencies = new Dictionary<string, int>();
            var topFeatureList = new List<string>();
            foreach (string request in featureRequests)
            {
                request.ToLower();
                foreach (string possibleFeature in possibleFeatures)
                {
                    possibleFeature.ToLower();
                    if (request.Contains(possibleFeature))
                    {
                        if (fetureFreaquencies.Contains(possibleFeature))
                        {
                            fetureFreaquencies(possibleFeature)++;
                            Console.WriteLine($"Updating feature: {possibleFeature}");
                        }
                        else
                        {
                            fetureFreaquencies.Add(possibleFeature);
                            Console.WriteLine($"Adding feature: {possibleFeature}");
                        }
                    }
                }
            }
        }
    }
}
