using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inpacken
{
    class ConsoleApp4
    {
        static void Main(string[] args)
        {
            String[] input_user = Console.ReadLine().Split(' ');


            long input_box_user_1 = long.Parse(input_user[0]);
            long input_box_user_2 = long.Parse(input_user[1]);


            long amount_size_boxes = input_box_user_1;
            long amount_present = input_box_user_2;
            long[] array_input_sizes_boxes;
            array_input_sizes_boxes = new long[amount_size_boxes];
            long total = 0;

            for (long t = 0; t < amount_size_boxes; t++)
            {
                string input_box_size = Console.ReadLine();
                array_input_sizes_boxes[t] = long.Parse(input_box_size);
            }


            for (long i = 0; i < amount_present; i++)
            {
                string[] input_box_size = Console.ReadLine().Split(' '); // input user for sizes
                long input_box_user = long.Parse(input_box_size[0]);


                total += binarySearch(input_box_user, array_input_sizes_boxes);
            }
            Console.WriteLine(total);


        }





        public static long binarySearch(long size_present, long[] box)
        {
            long s = 0;
            long e = box.Length - 1;
            long step = 0;


            long best_found = 10000000000;
            while (s <= e)
            {
                step += 1;
                //we come from a perspective that there is no way we have a negative box size.
                long m = (s + e) / 2;

                if (box[m] <= size_present)
                {
                    s = m;
                    s += 1;

                }

                if (box[m] > size_present)
                {
                    if (box[m] < best_found)
                    {
                        best_found = box[m];
                    }

                    e = m;

                    e -= 1;
                }

            }

            return best_found;

            // Console.WriteLine($"{size_present} Gebruik een  {best_found} -doos") ;


        }



    }
}
