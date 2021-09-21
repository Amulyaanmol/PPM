using System;
using Output;
namespace PPM
{
    public static class Program
    {
        public static void Main()
        {
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
