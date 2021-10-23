using System;
using System.Collections.Generic;

namespace Nemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            //taking the information
            var main_input = Console.ReadLine().Split();
            int lenght;
            int max_ark = 0;
            int max_weight = 0;
            int[] array_weight = new int[0];
            int trigger = 0;

            lenght = int.Parse(main_input[0]);
            max_ark = int.Parse(main_input[1]);
            max_weight = int.Parse(main_input[2]);
            array_weight = new int[lenght];
            if (lenght >= 3)
            {
                trigger = 1;
            }


            int[] mywiehgt_int = new int[24];

            //weight list
            if (trigger == 1)
            {
                var mywiehgt_string = Console.ReadLine().Split();
                //mywiehgt_int = Array.ConvertAll(mywiehgt_string, s => int.Parse(s));
                int try_pharse;
                int loc = 0;
                for (int i = 0; i < mywiehgt_string.Length; i++)
                {
                    if (int.TryParse(mywiehgt_string[i], out try_pharse))
                    {
                        int cururent = int.Parse(mywiehgt_string[i]);
                        mywiehgt_int[i] = cururent;
                    }
                }



            }

            //current state of the boat
            int current_weight = 0;
            int num = 0;
            int number_animals_on_board = 0;
            int current_ark = 0;

            //list to 
            List<object> total_output = new List<object>();
            int[] tatal_configuration = new int[16777216];
            while (true)
            {
                //gettint the data
                var operator_input = Console.ReadLine().Split();
                int value = 0;
                int number;
                string oper = operator_input[0].ToLower();
                if (operator_input.Length > 1)
                {
                    if (int.TryParse(operator_input[1], out number))
                    {
                        value = int.Parse(operator_input[1]);

                    }
                }

                // seeing the ouputs
                if (oper == "p")
                {
                    int state = ifpossible(max_weight, current_weight, mywiehgt_int[value]);
                    //check if zero
                    if (state == -1 || current_ark >= max_ark || add_p(num, value, mywiehgt_int, max_weight) == 0)
                    {
                        total_output.Add($"Weiger {value}");
                        tatal_configuration[num] += 1;

                    }
                    else
                    {
                        // num = num += 2 ^ value;
                        num = add_p(num, value, mywiehgt_int, max_weight);
                        number_animals_on_board += 1;
                        current_weight += mywiehgt_int[value];
                        tatal_configuration[num] += 1;
                        current_ark += 1;
                    }


                } //done

                if (oper == "q")
                {
                    //Geef het aantal aanwezige dieren en hun totaalgewicht in de vorm Aantal
                    total_output.Add($"Aantal {number_animals_on_board} Gewicht {current_weight}");
                    //Console.WriteLine($"Aantal {number_animals_on_board} Gewicht {current_weight}");
                } //done

                if (oper == "l")
                {
                    // Los i. Dier i gaat van boord
                    int return_leave = leave_i(num, value, current_weight, array_weight);
                    if (return_leave == num)
                    {
                        tatal_configuration[num] += 1;
                    }
                    else
                    {
                        num = return_leave;
                        number_animals_on_board -= 1;
                        current_weight -= mywiehgt_int[value];
                        current_ark -= 1;
                        tatal_configuration[num] += 1;
                    }

                } //done

                if (oper == "a")
                {
                    var current_dieres = which_diers(num);// I need to send the number
                    total_output.Add(current_dieres);
                    //  Geef een lijst van alle aanwezige dieren
                }

                if (oper == "c")
                {
                    total_output.Add($"Herhaald {tatal_configuration[num]}");
                    //Console.WriteLine();
                    //Count Configuration. Geef weer, hoe vaak de huidige Ark-bezetting al is voorgekomen na een p of l opdracht

                }

                if (oper == "e")
                {
                    //break end loop
                    Printing(total_output);
                    break;
                } //done
            }


        }

        static int ifpossible(int maxweight, int current_weight, int value)
        {
            if (maxweight - (current_weight + value) < 0)
            {
                return -1;

            } // impossible
            else
            {
                return 1;
            }
        }
        public static int add_p(int num, int value, int[] weight, int maxweight)
        {
            //OR Operation
            int current_value_add = 2 << (value - 1);// minus -1
            if (value == 0)
            {
                current_value_add = 1;
            }
            int result = num ^ current_value_add;

            if (result < num)// || result == num
            {
                return 0;
            } ////// check this if there is a problem with giving value 0
            else
            {
                return result;
            }


        } // P cheacking if number exsist.  If not, addidnt to list
        public static int leave_i(int num, int value, int current_weight, int[] list_weight)
        {
            //Xor
            int current_value_add = 2 << (value - 1);
            if (value == 0)
            {
                current_value_add = 1;
            }
            int result = num ^ current_value_add;
            if (result >= num || result == num)
            {
                return num;
            }
            return result;
        } // I - leaving
        static string which_diers(int num)
        {
            List<int> output = new List<int>();
            int[] array = new int[24];
            int loc = 0;
            while (num > 0)
            {
                if (num == 1)
                {
                    array[loc] = 1;
                    num -= 1;

                }
                else if (num % 2 == 0)
                {
                    num = num / 2;
                    array[loc] = 0;
                    //mystack.Push(0);
                }
                else
                {
                    num = (num / 2);
                    array[loc] = 1;

                }
                loc++;
            }

            for (int i = 0; i < array.Length; i++)
            {

                int current = array[i];
                if (current == 0)
                {
                    continue;
                }
                else
                {
                    output.Add(i);
                }

            }
            var string_output = "";

            foreach (var x in output)
            {

                string current = x.ToString();
                string_output += current + " ";
            }
            return string_output;

        } //A -  finding which diers are currently in place. ///checked
        static void Printing(List<object> list)
        {
            foreach (var x in list)
            {
                Console.WriteLine(x);
            }
        } // need to improve
    }
}
