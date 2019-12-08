using System;

namespace Question5
{
    class Program
    {
        static void Main(string[] args)
        {
            long num1 = 0, num2 = 1, num3;
            int number = 50;
            
            Console.Write(num1 + " : " + num2 + " : "); //printing 0 and 1    
            for (int i = 2; i < number; ++i) //loop starts from 2 because 0 and 1 are already printed    
            {
                num3 = num1 + num2; //add the previous 2 numbers to get the new number
                Console.Write(num3 + " : ");
                num1 = num2;
                num2 = num3;
            }
        }
    }
}
