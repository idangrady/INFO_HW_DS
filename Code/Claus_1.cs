using System;

namespace ConsoleApp2
{
    public class Class11
    {

        public static void Main(string[] args)
        {




            for (int i = 0; i < 4; i++)
            {
                string result = Console.ReadLine();
                char[] chare = { ' ', ' ' };

                string[] input = result.Split(chare);
                var answer = long.Parse(result);
                Console.WriteLine(collaz_cal(answer));
            }




        }

        static long collaz_cal(long x)
        {
            int steps = 0;

            while (x != 1)
            {
                if (x % 2 == 0)
                {
                    x = x / 2;
                    steps += 1;
                }
                else
                {
                    x = (3 * x) + 1;
                    steps += 1;

                }
            }
            return steps;
        }
    }
}
