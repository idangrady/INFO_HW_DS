using System;
using System.Collections.Generic;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {

            var numtime = Console.ReadLine();
            long num_time = long.Parse(numtime);// how many time we should loop

            long[] begin_array = new long[num_time];
            long[] leave_array = new long[num_time];


            List<(long start, long end)> list_ppl = new List<(long start, long end)>();

            //list of the arrival and the ppl who leave
            List<(long start, long value, long total)> arrival_list = new List<(long start, long value, long total)>();
            List<(long end, long value, long total)> left_list = new List<(long end, long value, long total)>();

            int round = 0;
            int loc_array = 0;

            while (round < num_time)
            {
                var input = Console.ReadLine().Split();
                long start = long.Parse(input[1]);
                long end = long.Parse(input[2]);

                if (start != end)
                {
                    begin_array[loc_array] = start;
                    leave_array[loc_array] = end;
                    

                    //inserting the values longo the list
                    arrival_list.Add((start, 1, 0));
                    left_list.Add((end, -1, 0));

                    loc_array++;

                }
                round++;

            }
            long[] new_sized_arry_come = new long[loc_array];
            long[] new_sized_arry_left = new long[loc_array];

            for (int i =0; i<loc_array; i++)
            {
                new_sized_arry_come[i] = begin_array[i];
                new_sized_arry_left[i] = leave_array[i];

            }



            //var sorted_list = list_count(list_ppl, begin_array);
            var list_arrival_sorted = list_count(arrival_list, new_sized_arry_come);
            var list_leaving_sorted = list_count(left_list, new_sized_arry_left);



            List<(long start, long value, long total)> final_list = new List<(long start, long value, long total)>(6);

            int current_begin = 0;
            int current_end = 0;

            while (current_begin < new_sized_arry_come.Length)
            {
                var current_begining_tuppel = list_arrival_sorted[current_begin].start;
                var current_leaving_tuppel = list_leaving_sorted[current_end].start;

                if (current_begining_tuppel < current_leaving_tuppel)
                {
                    final_list.Add(list_arrival_sorted[current_begin]);
                    current_begin++;
                }
                else
                {
                    final_list.Add(list_leaving_sorted[current_end]);
                    current_end++;
                }

                if (current_begin == new_sized_arry_come.Length)
                {
                    while (current_end < new_sized_arry_come.Length)
                    {
                        final_list.Add(list_leaving_sorted[current_end]); current_end++;
                    }
                }
            }


            long total = 1;
            var first_item = final_list[0];
            first_item.total = first_item.value;
            final_list[0] = first_item;

            for (int i = 1; i < final_list.Count - 1; i++)
            {

                var previcous_elemnt = final_list[i - 1];

                var corrent_elemnt = final_list[i];

                if (previcous_elemnt.start == corrent_elemnt.start && previcous_elemnt.value != corrent_elemnt.value)
                {
                    previcous_elemnt.total = previcous_elemnt.total + corrent_elemnt.value;
                    final_list[i - 1] = previcous_elemnt;
                    corrent_elemnt.total = previcous_elemnt.total;
                    final_list[i] = corrent_elemnt;


                }
                else
                {
                    corrent_elemnt.total = previcous_elemnt.total + corrent_elemnt.value;
                    final_list[i] = corrent_elemnt;

                }

            }

            List<(long start, long end)> printing_max = new List<(long start, long end)>(6);

            long max_times = 0;//finding max
            //Iterating throgh the List to find what is the max and where
            for (int i = 0; i < final_list.Count - 1; i++)
            {
                var current_loop_element = final_list[i];
                if (current_loop_element.total > max_times)
                {
                    max_times = current_loop_element.total;
                }

            }
            bool found = false;
            long begin_max = -1;//when does the loop start
            long end_max = -1;//when does it end
            int secored = -1;

            for (int i = 0; i <= final_list.Count - 1; i++)
            {
                var current_loop_element = final_list[i];
                if (current_loop_element.total == max_times && begin_max == -1 && secored == -1)
                {
                    found = true;
                    secored = -2;
                    begin_max = current_loop_element.start; //attache the variable

                }

                if (found = true && current_loop_element.total != max_times && secored == -2)
                {
                    end_max = current_loop_element.start;
                    printing_max.Add((begin_max, end_max));
                    found = false;
                    begin_max = -1;
                    secored = -1;
                }

            }

            Console.WriteLine(max_times);
            foreach (var x in printing_max)
            {
                Console.WriteLine("Van" + " " + x.start + " " + "tot " + x.end);
            }
            Console.ReadLine();

        }




        static List<(long start, long end, long total)> list_count(List<(long start, long value, long total)> list, long[] array)
        {

            //cloning the output
            var output_List = new List<(long start, long end, long total)>();
            foreach (var x in list)
            {
                output_List.Add(x);
            }

            long largest_num = array[0];// var 1 in the list assigned as the bigest number

            for (long i = 0; i < array.Length; i++)//find biggest num
            {
                if (array[i] > largest_num)
                {
                    largest_num = array[i];
                }
            }
            //do a list from 0 to largest num

            long[] count_array = new long[largest_num + 1];


            //iterating throgh the list to increment the numbers
            for (long ll = 0; ll < count_array.Length; ll++)
            {
                count_array[ll] = 0;
            }


            for (long m = 0; m <= array.Length - 1; m++)
            {
                long current = array[m];
                count_array[current] += 1;
            }

            // additing based on the value
            for (long n = 1; n < count_array.Length; n++)
            {

                count_array[n] = count_array[n] + count_array[n - 1];

            }


            //
            for (int l = array.Length - 1; l >= 0; l--)
            {

                long current_value = array[l];
                var current_taple = list[l];
                count_array[current_value] -= 1;

                int loc = (int)count_array[current_value];

                output_List[loc] = current_taple;

            }

            return output_List;
        }
    }
}