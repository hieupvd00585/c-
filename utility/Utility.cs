using System;

namespace BankPlus.utility
{
    public class Utility
    {
        public static int GetInt32Number()
        {
            var number = 0;
            while (true)
            {
                try
                {
                    number= Int32.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a number:");
                    throw;
                }
            }

            return number;
        }

        public static decimal GetDecimalNumber()
        {
            decimal number = 0;
            while (true)
            {
                try
                {
                    number = Decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a number.");
                    
                }
            }

            return number;
        }
        
    }
}