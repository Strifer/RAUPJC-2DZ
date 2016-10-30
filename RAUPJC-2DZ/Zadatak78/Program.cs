using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak78
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            var result = await IKnowIGuyWhoKnowsAGuy();
            return result;
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            var resulta = await IKnowWhoKnowsThis(10);
            var resultb = await IKnowWhoKnowsThis(5);
            return resulta + resultb;
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            var result = await FactorialDigitSum(n);
            return result;
        }

        public static async Task<int> FactorialDigitSum(int n)
        {
            Task<int> t = new Task<int>(() =>
                {
                    int factorial = 1;
                    int sum = 0;
                    for (int i = 1; i <= n; i++)
                    {
                        factorial *= i;
                    }

                    while (factorial != 0)
                    {
                        sum += (factorial % 10);
                        factorial = factorial / 10;
                    }

                    return sum;

                }
            );

            t.Start();
            return  await t;

        }
    }


}
