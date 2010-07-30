using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ActionFuncConsole
{
    class Program
    {
        static void Main()
        {
            var result = Div(16, 3, (q, r) => new { Quotient = q, Remainder = r });
            Console.WriteLine("quotient: {0}", result.Quotient);
            Console.WriteLine("remainder: {0}", result.Remainder);          

            var multiplyResult = Multiply(16, 3, (answer) => new { Answer = answer });

            Console.WriteLine("Survey Says:{0}", multiplyResult.Answer);

            Console.ReadLine();
        }

        public static T Div<T>(int dividend, int divisor, Func<int, int, T> func)
        {
            Contract.Requires(divisor != 0);
            Contract.Requires(func != null);

            var quotient = dividend / divisor;
            var remainder = dividend % divisor;

            var retValue = func(quotient, remainder);

            return func(quotient, remainder);
        }

        public static T Multiply<T>(int a, int b, Func<int, T> func)
        {
            Contract.Requires(func != null);

            return func(a * b);
        }
    }
}
