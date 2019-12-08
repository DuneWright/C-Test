using System;

namespace Question1
{
    class Program
    {
        static void Main(string[] args)
        {
            string givenSentence = "Please replace all characters equals to the letter “A” with an underscore(_)"; //the string we got
            char[] sLetters = givenSentence.ToCharArray();// convert the string into an array of chars
            for(int i = 0; i< givenSentence.Length; i++)//forloop to go through all the chars in the array
            {
                char letter = sLetters[i];//takes one of the chars and places it into the variavble
                if (letter == 'A' )//checks for match
                {
                    sLetters[i] = '_';// replace
                }
                else if (letter == 'a')
                {
                    sLetters[i] = '_';
                }
            }
            string newSentence = new string(sLetters);//puts the array into a new string
            Console.WriteLine(newSentence);//prints the string
        }
    }
}
