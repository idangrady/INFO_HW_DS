using System;
using System.Collections.Generic;

namespace struck_merge_sort
{
    class Program
    {
        public struct Motor
        {
            public string name;
            public long price;
            public long speed;
            public long weight;
        }
        static void Main(string[] args)
        {

            var input = Console.ReadLine().Split();
            long input_con = long.Parse(input[0]);

            List<Motor> price_list = new List<Motor>();
            List<Motor> speed_list = new List<Motor>();


            int round = 0;
            int loc_array = 0;
            if (input_con >= 1)
            {
                while (round < input_con)
                {

                    var input_detail = Console.ReadLine().Split();
                    var name = input_detail[0];
                    //var name = characters.toString(input_detail[0]); //name
                    var price = long.Parse(input_detail[1]); //price
                    var speed = long.Parse(input_detail[2]) * (-1); //speed
                    var weight = long.Parse(input_detail[3]); //weight

                    Motor motor1 = new Motor();
                    motor1.name = name;
                    motor1.speed = speed;
                    motor1.weight = weight;
                    motor1.price = price;

                    price_list.Add(motor1);
                    speed_list.Add(motor1);



                    loc_array++;
                    round++;
                }
                var list_price_sorted = Merge_sort(price_list, 0, price_list.Count - 1, -1);
                var list_speed_sorted = Merge_sort(speed_list, 0, price_list.Count - 1, 1);
                Console.WriteLine(list_price_sorted.Count);
                for (int i = 0; i < list_price_sorted.Count; i++)
                {
                    var name_price = list_price_sorted[i].name;
                    int length_price = name_price.Length;

                    var speed_name = list_speed_sorted[i].name;
                    int length_speed = speed_name.Length;

                    int num_of_point_price = 21 - length_price;
                    int num_of_point_speed = 21 - length_speed;

                    Console.WriteLine(name_price + (new string('.', num_of_point_price)) + speed_name + (new string('.', num_of_point_speed)));
                }

            }
            else
            {
                Console.WriteLine("0");
            }

        }

        static List<Motor> Merge_sort(List<Motor> list, long begin, long end, int side)
        {

            var canter = (begin + end) / 2;
            if (begin >= end)
            {
                return new List<Motor> { list[(int)end] };
            }

            var left_list = Merge_sort(list, begin, canter, side);
            var right_list = Merge_sort(list, canter + 1, end, side);

            if (side == -1)
            {
                var merged_array = Merge(left_list, right_list, -1);

                return merged_array;

            }
            else
            {
                var merged_array = Merge(left_list, right_list, 1);
                return merged_array;
            }


        }

        public static List<Motor> Merge(List<Motor> first_list, List<Motor> second_list, int side)
        {

            if (side == -1)
            {
                List<Motor> output_list = new List<Motor>();

                int i = 0;
                int j = 0;
                int k = 0;


                while (i < first_list.Count && j < second_list.Count)
                {


                    if (first_list[i].price == second_list[j].price)
                    {

                        if (first_list[i].speed < second_list[j].speed)
                        {
                            output_list.Add(first_list[i]);
                            i++;
                        }

                        else if (first_list[i].speed > second_list[j].speed)
                        {
                            output_list.Add(second_list[j]);
                            j++;
                        }

                        else if (first_list[i].speed == second_list[j].speed)
                        {

                            if (first_list[i].weight < second_list[j].weight)
                            {
                                output_list.Add(first_list[i]);
                                i++;
                            }
                            else if (first_list[i].weight > second_list[j].weight)
                            {
                                output_list.Add(second_list[j]);
                                j++;
                            }

                            else if (first_list[i].weight == second_list[j].weight)
                            {
                                string word_one = first_list[i].name;
                                string word_two = second_list[j].name;


                                int result = string.Compare(word_one, word_two);

                                if (result < 0)
                                {
                                    output_list.Add(first_list[i]);
                                    i++;
                                }
                                else if (result >= 0)
                                {
                                    output_list.Add(second_list[j]);
                                    j++;
                                }
                            }


                        }
                    }


                    else if (first_list[i].price < second_list[j].price)
                    {
                        output_list.Add(first_list[i]);
                        i++;
                    }

                    else if (first_list[i].price > second_list[j].price)
                    {
                        output_list.Add(second_list[j]);
                        j++;
                    }

                    k++;

                }

                while (i < first_list.Count)//if all the items in one array have neem places
                {
                    output_list.Add(first_list[i]);
                    i++;
                    k++;/// he took it out- I am not usre why

                }
                while (j < second_list.Count)//if all the items in one array have neem places
                {
                    output_list.Add(second_list[j]);
                    j++;
                    k++;/// he took it out- I am not usre why

                }

                return output_list;
            }
            else
            {
                List<Motor> output_list = new List<Motor>();


                int i = 0;
                int j = 0;
                int k = 0;


                while (i < first_list.Count && j < second_list.Count)
                {


                    if (first_list[i].speed == second_list[j].speed)
                    {

                        if (first_list[i].weight < second_list[j].weight)
                        {
                            output_list.Add(first_list[i]);
                            i++;
                        }

                        else if (first_list[i].weight > second_list[j].weight)
                        {
                            output_list.Add(second_list[j]);
                            j++;
                        }

                        else if (first_list[i].weight == second_list[j].weight)
                        {

                            if (first_list[i].price < second_list[j].price)
                            {
                                output_list.Add(first_list[i]);
                                i++;
                            }
                            else if (first_list[i].price > second_list[j].price)
                            {
                                output_list.Add(second_list[j]);
                                j++;
                            }

                            else if (first_list[i].price == second_list[j].price)
                            {
                                string word_one = first_list[i].name;
                                string word_two = second_list[j].name;


                                int result = string.Compare(word_one, word_two);

                                if (result < 0)
                                {
                                    output_list.Add(first_list[i]);
                                    i++;
                                }
                                else if (result >= 0)
                                {
                                    output_list.Add(second_list[j]);
                                    j++;
                                }
                            }


                        }
                    }



                    else if (first_list[i].speed < second_list[j].speed)
                    {
                        output_list.Add(first_list[i]);
                        i++;
                    }


                    else if (first_list[i].speed > second_list[j].speed)
                    {
                        output_list.Add(second_list[j]);
                        j++;
                    }

                    k++;

                }

                while (i < first_list.Count)//if all the items in one array have neem places
                {
                    output_list.Add(first_list[i]);
                    i++;
                    k++;/// he took it out- I am not usre why

                }
                while (j < second_list.Count)//if all the items in one array have neem places
                {
                    output_list.Add(second_list[j]);
                    j++;
                    k++;/// he took it out- I am not usre why

                }

                return output_list;
            }

        }



    }
}