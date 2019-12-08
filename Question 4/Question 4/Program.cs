using System;

namespace Question_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialising n, testnum and array total. also initialising/ creating array with 59 spaces
            int n = 0; 
            int testnum = 0;
            int[] primes = new int[59];
            int arrtot = 0;

            while (n != 59) //couldnt figure out how to solve the problem with for loop and continue, so i used while
            {
            if (testPrime(testnum)) //call the method that tests the number and returns a boolean and if true is returned...
            {
                    primes[n] = testnum;//the number is added to the array
                    n++;//increment n
                    testnum++;//increment testnum

            }
            else //if false is returned 
            {
                    testnum++; // only increment testnum
            }
            }
            for (int i = 0; i < primes.Length; i++)//getting the total of the prime numbers
            {
                //Console.Write("{0} _ ", primes[i]);
                arrtot = arrtot + primes[i];
            }
            Console.WriteLine("The total of the first n (n=59) prime numbers is : {0}",arrtot);//printing the total

        }
        public static bool testPrime(int number)//method to test if a number is prime
        {
            if (number <= 1) return false;// 0 and 1 are not prime numbers
            if (number == 2) return true; // 2 is a prime number
            if (number % 2 == 0) return false; // other numbers divisible by 2 are not prime numbers

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
