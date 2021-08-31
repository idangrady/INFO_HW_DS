using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Algorithms_HW1
{
    struct Algorithm
    {
        public string name;
        public string description;
        public string result_text;
        public Func<dynamic, bool, dynamic> algFunction; // Array input, array output, debug
        public Func<dynamic, bool, dynamic> genInputFunction; // Function that generates an input for the algFunction to use.
        public dynamic optionsGenFunction;  // optional options for genInputFunctions to use.

    }

    class Algorithms
    {

        struct RandomIntArrayInput
        {
            public int size;
            public bool unique;

            public void setOptions() {
                do
                {
                    Console.WriteLine("Set the size of the randomized array:");
                } while (!int.TryParse(Console.ReadLine(), out size));

                Console.WriteLine("Enter 'y' if you want only unique integer values:");
                unique = Console.ReadLine() == "y";
            }
        }

        List<Algorithm> algList = new List<Algorithm> { 
            new Algorithm{ name = "pannenkoeken", algFunction = algPannenkoeken, genInputFunction = genRandomIntArray, optionsGenFunction = new RandomIntArrayInput(), 
                result_text = "Total flips made: [total_result] with an avarage flips of [avg_result]",
                description = "Sorting algorithm where you can only flip part of the array between 0 and X, implemented Quick Sort."},
            new Algorithm{ name = "pannenkoeken-select", algFunction = algPannenkoekenSelect, genInputFunction = genRandomIntArray, optionsGenFunction = new RandomIntArrayInput(),
                result_text = "Total flips made: [total_result] with an avarage flips of [avg_result]",
                description = "Sorting algorithm where you can only flip part of the array between 0 and X. This implements a Selection Sort variant"},
            new Algorithm{ name = "pannenkoeken-qs", algFunction = algPannenkoekenQS, genInputFunction = genRandomIntArray, optionsGenFunction = new RandomIntArrayInput(), 
                result_text = "Total flips made: [total_result] with an avarage flips of [avg_result]",
                description = "Sorting algorithm where you can only flip part of the array between 0 and X. This implements an Insertion Sort variant.."}
        };



        public void run()
        {
            while(true)
            {
                Console.WriteLine("\n\nEnter an Algorithm that you wish to run and how often you want to run it. For example: 'pannenkoeken 20'");
                
                foreach(Algorithm alg_text in algList)
                {
                    Console.WriteLine($"\t{alg_text.name} \t- \t{alg_text.description}");
                }

                string[] commands = Console.ReadLine().Split(' ');
                if (commands.Length == 0) { continue; }

                int run_amount = 1;
                if (commands.Length > 1)
                {
                    int.TryParse(commands[1], out run_amount);
                }

                string alg_name = commands[0];
                Algorithm alg = algList.Find(alg => alg.name == alg_name);

                if (alg.name != "")
                {
                    var stopwatch = new Stopwatch();

                    // Set new options for generating random input.
                    alg.optionsGenFunction.setOptions();

                    int overflow_limit = 3;
                    bool debug = true;
                    long total_result = 0;

                    // For each attempt, generate a random input and execute algorithm.
                    for (int n=0; n<run_amount; n++)
                    {
                        if (debug) { Console.WriteLine("\n"); }
                        // Only debug a few times in the beginning and end (Depending on overflow_limit)
                        if (n == overflow_limit)
                        {
                            Console.WriteLine("...\n");
                            debug = false;
                        } else if (n == run_amount - overflow_limit)
                        {
                            debug = true;
                        }

                       


                        // Generate input
                        dynamic input = alg.genInputFunction(alg.optionsGenFunction, debug);

                        // Execture algorithm and track time elapsed
                        stopwatch.Start();
                        dynamic result = alg.algFunction(input, debug);
                        stopwatch.Stop();

                        if (result is int || result is long)
                        {
                            total_result += (long)result;
                        }
                    }

                    // Debug results
                    long elapsed_time = stopwatch.ElapsedMilliseconds;
                    long avg_elapsed_time = elapsed_time / run_amount;
                    Console.WriteLine($"\n\nTotal time taken: {elapsed_time}ms with an avarage time of: {avg_elapsed_time}ms");
                    Console.WriteLine(alg.result_text.Replace("[total_result]", total_result.ToString()).Replace("[avg_result]", (total_result / run_amount).ToString()));

                } else {
                    Console.WriteLine($"Algorithm '{alg_name}' was not found.\n");
                }
            }
        }

        // Generate an array with random ints
        public static dynamic genRandomIntArray(dynamic options, bool debug)
        {
            int size = options.size;
            bool unique = options.unique;


            int[] arr = new int[size];
            Random rand = new Random();

            // Generate the numbers
            for (int i = 0; i <= arr.Length-1; i++)
            {
                if (unique)
                {
                    arr[i] = i+1;
                } else
                {
                    arr[i] = rand.Next(1, size+1); 
                }
            }

            // Randomize number positions by swapping positions
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int j = rand.Next(i, arr.Length);
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }

            if (debug)
            {
                string extra_text = (unique) ? "with unique numbers" : "with randomized numbers";
                Console.WriteLine($"Randomized array of size '{size}' created, {extra_text} from 1 to {size}");
                debugArrayValues(arr);
            }

            return arr;
        }


        public static void debugArrayValues(int[] arr)
        {
            // Debug array values
            for (int i = 0; i < arr.Length - 1; i++)
            {
                Console.Write(arr[i].ToString());
                if (i < arr.Length - 2)
                {
                    Console.Write(", ");
                }

                int overflow_limit = 12;
                if (i > overflow_limit && i < arr.Length - overflow_limit - 1)
                {
                    Console.Write("..., ");
                    i = arr.Length - overflow_limit - 1;
                }
            }
            //Console.Write("\n");
        }


        public static dynamic algPannenkoekenQS(dynamic dyn_input, bool debug)
        {
            int[] input = dyn_input;
            int total_flips;

            if (debug ){
                Console.WriteLine($"\nQuick Sorting array by flipping... ");
            }

            (input, total_flips) = pannenkoekenQS(input, 0, input.Length - 1);

            if (debug)
            {
                Console.WriteLine($"Completed! Total flips executed: {total_flips}");
                debugArrayValues(input);
            }

            return total_flips;
        }

        public static (int[], int) pannenkoekenQS(int[] input, int low, int high)
        {
            int total_flips = 0;
            if (low < high)
            {
                int pivot_pos, added_flips;
                (input, pivot_pos, added_flips) = pannenkoekenQSPart(input, low, high);
                total_flips += added_flips;
                (input, added_flips) = pannenkoekenQS(input, low, pivot_pos - 1);
                total_flips += added_flips;
                (input, added_flips) = pannenkoekenQS(input, pivot_pos + 1, high);
                total_flips += added_flips;
            }

            return (input, total_flips);
        }


        public static (int[], int, int) pannenkoekenQSPart(int[] array, int low, int high)
        {
            int pivot = array[low];
            int left_wall = low;
            int total_flips = 0;
            int add_flips;

            for (int i = low + 1; i <= high; i++) 
            {
                if (array[i] < pivot) {
                    (array, add_flips) = pannenkoekenSwap(array, i, left_wall + 1);
                    left_wall++;
                    total_flips += add_flips;
                }
            }

            (array, add_flips) = pannenkoekenSwap(array, low, left_wall);
            total_flips += add_flips;

            return (array, left_wall, total_flips);
        }

        private static (int[], int) pannenkoekenSwap(int[] arr, int pos1, int pos2)
        {
            if (pos1 == pos2)
            {
                return (arr, 0);
            }
            else
            {
                int a = (pos1 < pos2) ? pos1 : pos2;
                int b = (pos1 < pos2) ? pos2 : pos1;

                arr = flipIntArrayAt(arr, a+1);
                arr = flipIntArrayAt(arr, b+1);
                arr = flipIntArrayAt(arr, b);
                arr = flipIntArrayAt(arr, b - 1);
                arr = flipIntArrayAt(arr, b);
                arr = flipIntArrayAt(arr, a+1);

                return (arr, 6);
            }
        }

        public static dynamic algPannenkoekenSelect(dynamic dyn_input, bool debug)
        {
            int[] input = dyn_input;
            int total_flips = 0;

            if (debug) { Console.WriteLine("\nSorting array, with selection sort by flipping..."); }
            for (int i = 0; i < input.Length; i++)
            {
                // Array 0 to i has already been sorted
                int lowest = i;
                int lowest_val = input[i];
                for(int j = i; j<input.Length; j++)
                {
                    if (input[j] < lowest_val)
                    {
                        lowest = j;
                        lowest_val = input[j];
                    }
                }

                input = flipIntArrayAt(input, lowest);
                input = flipIntArrayAt(input, lowest + 1);

                total_flips += 2;
            }

            // Flip to get lowest value first.
            if (Math.Sign(input[input.Length - 1] - input[0]) < 0)
            {
                input = flipIntArrayAt(input, input.Length - 1);
                total_flips++;
            }

            if (debug)
            {
                Console.WriteLine($"Completed! Total flips executed: {total_flips}");
                debugArrayValues(input);
            }

            return total_flips;
        }

        public static dynamic algPannenkoeken(dynamic dyn_input, bool debug) {
            int[] input = dyn_input;
            int total_flips = 0;

            if (debug) { Console.WriteLine("\nSorting array by flipping..."); }
            if (input.Length > 2)
            {
                int sign = Math.Sign(input[1] - input[0]);
                for (int i = 2; i < input.Length; i++)
                {
                    int new_sign = Math.Sign(input[i] - input[i - 1]);
                    if (new_sign != 0 && new_sign != sign)
                    {
                        // A incorrect order has been found.
                        if (sign == 1 && (input[i] <= input[0] || input[i] >= input[i-1]) || sign == -1 && (input[i] >= input[0] || input[i] <= input[i - 1]))
                        {
                            // highest or lowest
                            input = flipIntArrayAt(input, i);
                            total_flips++;
                            sign = new_sign;
                        } else
                        {
                            // Number needs to go somewhere between 0 and i. 
                            (int[] first, int[] second) = splitArrayAt(input, i+1);
                            int pos = BinarySearch(first, input[i]) + 1;

                            // Place item 'i' at pos after flipping whole stack to i.
                            input = flipIntArrayAt(input, i + 1);
                            input = flipIntArrayAt(input, i - pos + 1);
                            input = flipIntArrayAt(input, i - pos);

                            total_flips += 3;
                            sign = new_sign;
                        }
                    }

                   /* 
                    Console.Write("\n");
                    debugArrayValues(input);
                   */

                }
            }


            // Flip to get lowest value first.
            if (Math.Sign(input[input.Length - 1] - input[0]) < 0)
            {
                input = flipIntArrayAt(input, input.Length-1);
                total_flips++;
            }

            if (debug)
            {
                Console.WriteLine($"Completed! Total flips executed: {total_flips}");
                debugArrayValues(input);
            }

            return total_flips;
        }



        private static int[] flipIntArrayAt(int[] arr, int pos)
        {
            // Split array into two and flip the first part
            (int[] arr_flipped, int[] arr_rest) = splitArrayAt(arr, pos);

            Array.Reverse(arr_flipped);

            // Combine arrays into arr_flipped
            int temp_length = arr_flipped.Length;
            Array.Resize(ref arr_flipped, temp_length + arr_rest.Length);
            Array.Copy(arr_rest, 0, arr_flipped, temp_length, arr_rest.Length);

            return arr_flipped;
        }

        private static (int[], int[]) splitArrayAt(int[] arr, int pos)
        {
            /** Resource:
             * https://picscout.com/blog/how-to-split-an-array-in-c
             */

            // Create new arrays and copy data into it.
            int[] first = new int[pos];
            Array.Copy(arr, first, first.Length);
            int[] second = new int[arr.Length - first.Length];
            Array.Copy(arr, first.Length, second, 0, second.Length);

            return (first, second);
        }

        private static int BinarySearch(int[] arr, int value)
        {
            int left = 0;
            int right = arr.Length - 1;
            bool reversed = (arr[0] - arr[arr.Length - 1] > 0);

            while (right > left + 1)
            {
                // Bit shift is the same as deviding by 2. Saves a little time
                int mid = (left + right) >> 1;

                // Check if value is left or right.
                if (value < arr[mid])
                {
                    if (reversed) { left = mid; } else { right = mid; } 
                }
                else
                {
                    if (reversed) { right = mid; } else { left = mid; }
                }
            }

            return left;
        }

    }
}
