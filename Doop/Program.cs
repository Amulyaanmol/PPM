using System;
using System.Collections.Generic;
using Domain;
using Output;
namespace PPM
{
    public class Program
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            Console.Clear();
            int option1 = Display.DisplayMainMenu();
            try
            {
                Display.MainCall(option1);
                Console.Read();
            }
            catch (Exception)
            {
                    Console.WriteLine("please provide correct Input....");
                    Display.MainCall(option1);
                    Console.Read();
                
            }

        }
    }
}
