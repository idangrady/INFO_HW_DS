using System;
using System.Collections.Generic;

namespace Koel_6
{
    class Example
    {
        static public string Table =
            "8 8 P" +
            "\nMMMMMMMM" +
            "\nM.....?M" +
            "\nM....TTM" +
            "\nM....TTM" +
            "\nM..!...M" +
            "\nM....+.M" +
            "\nM......M" +
            "\nMMMMMMMM";

        static public string small_input =
            "4 5 L" +
            "\nMMMM" +
            "\nM.?M" +
            "\nM.!M" +
            "\nM+.M" +
            "\nMMMM";

        static public string Impossible =
            "7 7 L" +
            "\nMMMMMMM" +
            "\nM....?M" +
            "\nM...TTM" +
            "\nM...TTM" +
            "\nM!....M" +
            "\nM...+.M" +
            "\nMMMMMMM";

        static public string Large_ex =
            "105 23 L" +
            "\nMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
            "\nM......................................................................................................MM" +
            "\nM?!+...................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nM......................................................................................................MM" +
            "\nMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";

        static public string harder =
            "12 12 P" +
            "\nMMMMMMMMMMMM" +
            "\nM...MMMM...M" +
            "\nM...MMMM...M" +
            "\nM..........M" +
            "\nMMM.MMMM.MMM" +
            "\nMMM.MMMM.MMM" +
            "\nMMM.MMMM.MMM" +
            "\nMMM.MMMM.MMM" +
            "\nM..!..M..+?M" +
            "\nM...M...MMMM" +
            "\nM...MMMMMMMM" +
            "\nMMMMMMMMMMMM";

        static public string longest =
            "256 5 P" +
            "\nMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
            "\nM?!+...........................................................................................................................................................................................................................................................M" +
            "\nM..............................................................................................................................................................................................................................................................M" +
            "\nMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
        static public string another_problem =
            "18 9 P" +
            "\nMMMMMMMMMMMMMMMMMM" +
            "\nM?MMMMMMMMMMMMMMMM" +
            "\nM+MMMMMMMMMMMMMMMM" +
            "\nM!...............M" +
            "\nM.MMMMMMMMMMMMMM.M" +
            "\nM................M" +
            "\nM.M.MMMMMMMMMMMMMM" +
            "\nM...MMMMMMMMMMMMMM" +
            "\nMMMMMMMMMMMMMMMMMM";
    }

    class Program
    {
        static private bool debug_mode = false;
        static void Main(string[] args)
        {
            //getting the example to a list
            string[] tables_example = Example.Table.Split('\n');

            // Write input to screen. Should not be used in DomJudge
            if (debug_mode)
            {
                foreach (string line in tables_example)
                {
                    Console.WriteLine(line);
                }
            }


            if (debug_mode)
            {
                uint length = uint.Parse(tables_example[0].Split(' ')[0]);
                uint height = uint.Parse(tables_example[0].Split(' ')[1]);
                string operation = tables_example[0].Split(' ')[2].ToLower();

                (Value[,] grid, ushort person_loc, ushort koel_kast_loc, ushort target_loc) = scan_location_from_example(length, height, tables_example);
                uint location = Convert_number.combine_two_ushorts(person_loc, koel_kast_loc);

                var check = Convert_number.from_int_to_byte(location);

                (ushort steps, string path, string result) = Bfs(grid, location, target_loc);
                printing_values(operation, path, steps, result);
                //  find_childrens_int(location);
                // Console.ReadLine();
            }
            else
            {
                var input_line = Console.ReadLine().Split(' ');
                uint number;
                if (uint.TryParse(input_line[0], out number) && uint.TryParse(input_line[1], out number))
                {
                    // get the amount of colomns/rows to get the values
                    uint rows = uint.Parse(input_line[0]);
                    uint column = uint.Parse(input_line[1]);
                    string[] strings_lines_input = new string[column + 1];
                    for (int row = 1; row < column + 1; row++)
                    {
                        strings_lines_input[row] = Console.ReadLine();
                    }
                    // get the amount of rows
                    //uint column = uint.Parse(input_line[1]);// check if this should not be opposite ==> chancg column and row
                    string operation = input_line[2].ToLower();
                    (Value[,] grid, ushort person_loc, ushort koel_kast_loc, ushort target_loc) = scan_location_from_example(rows, column, strings_lines_input);
                    uint location = Convert_number.combine_two_ushorts(person_loc, koel_kast_loc);

                    (ushort steps, string path, string result) = Bfs(grid, location, target_loc);
                    printing_values(operation, path, steps, result);

                }
                else
                {
                    Console.WriteLine("No solution");
                }
            }
        }

        public struct important_locations
        {
            public (ushort, ushort) person_loc_loc;
            public (ushort, ushort) koel_loc_loc;
            public (ushort, ushort) destination_loc;
            public (ushort, ushort) wall_loc;
            public (ushort, ushort) way_loc;
            //values 
            public int person;
            public int wall;
            public int koelkast;
            public int way;
            public int target;

        } // not using for now


        public enum Value : ushort
        {
            Wall = 0,
            way = 1,
            koelkast = 2,
            Person = 3,
            Target = 4,

            None = byte.MaxValue
        }

        static (Value[,], ushort loc_person, ushort koelkast_loc, ushort destination) scan_location_from_example(uint row, uint column, string[] grid_examplpe) //I should find a way to return the found values. 
        {
            Value[,] grid = new Value[row, column];
            string alphabet = "ABCDEFGHIGKLMNOPQRSTUVWSXYZabcdefghijklmnopqrstuvwxyz";
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            ushort koelkast_loc = 0;
            ushort person_loc = 0;
            ushort target_byte = 0;
            for (byte x = 0; x < row - 1; x++)
            {
                important_locations values = new important_locations();
                for (byte y = 0; y < column - 1; y++)   // done
                {
                    char c = grid_examplpe[y + 1][x];
                    if (c == '+')
                    {
                        grid[x, y] = Value.way;
                        person_loc = Convert_number.two_bytes_to_num(x, y);
                    }
                    else if (c == '!')
                    {
                        grid[x, y] = Value.way;
                        koelkast_loc = Convert_number.two_bytes_to_num(x, y);

                    }
                    else if (c == '?')
                    {
                        grid[x, y] = Value.Target;
                        target_byte = Convert_number.two_bytes_to_num(x, y);

                    }
                    else if (c == '.')
                    {
                        grid[x, y] = Value.way;
                    }
                    else if (c == 'M')
                    {
                        bool barrier = false;
                        barrier = true;
                        grid[x, y] = Value.Wall;
                    }
                    else
                    {
                        bool barrier = false;
                        char al;
                        for (int i = 0; i < alpha.Length; i++)
                        {
                            var current_letter = alpha[i];

                            if (c == alpha[i])
                            {
                                barrier = true;
                                grid[x, y] = Value.Wall;
                            }
                        }
                    }
                }

            }
            return (grid, person_loc, koelkast_loc, target_byte); //outputing the grip with the current locations of the three important obbjects
        }

        static (ushort steps, string path_string, string result) Bfs(Value[,] grid, uint begin, ushort target) // searching Algorythin
        {
            Dictionary<uint, uint> visited = new Dictionary<uint, uint>();
            Queue<uint> to_visit = new Queue<uint>();
            (byte person_x, byte person_y, byte koelkast_x, byte koelkast_y) = Convert_number.from_int_to_byte(begin);
            visited.Add(begin, 0);
            to_visit.Enqueue(begin);
            bool found = false;
            uint count_step = 0;
            uint found_node = 0;
            List<uint> path = new List<uint>();


            while (to_visit.Count > 0 && found == false)
            {
                count_step++;
                var previous_node = to_visit.Dequeue(); // last node
                if (previous_node == target)
                {
                    found = true;
                    previous_node = found_node;

                } // double check
                else
                {
                    List<uint> current_root = find_childrens_int(previous_node, grid); // getting the childrens

                    foreach (uint current_node in current_root)
                    {
                        (byte person_x_current, byte person_y_current, byte koelkast_x_current, byte koelkast_y_current) = Convert_number.from_int_to_byte(current_node);
                        ushort koelkast_loc = Convert_number.two_bytes_to_num(koelkast_x_current, koelkast_y_current);
                        ushort person_loc = Convert_number.two_bytes_to_num(person_x, person_y);
                        //uint current_node = current_root[current];
                        if (koelkast_loc == target)
                        {
                            visited.Add(current_node, previous_node);
                            //Program.dubug_print(grid, current_node, target);
                            found = true;
                            found_node = current_node;
                            break;
                        }
                        else if (visited.ContainsKey(current_node))
                        {
                            continue;
                        }
                        else// && person_loc!=koel_kast_loc
                        {
                            visited.Add(current_node, previous_node);
                            //Program.dubug_print(grid, current_node, target);
                            to_visit.Enqueue(current_node);
                        }

                    }
                }
            }

            if (found == true)
            {
                //string path_string = "";

                //Console.WriteLine(" Found!");
                // dubug_print(grid, found_node, target);
                (ushort steps, string path_string) = get_path_from_visited(found_node, visited, grid, target);

                return (steps, path_string, "found");
                // Console.WriteLine(steps);
                //Console.WriteLine(path_string);
                // we need to find the path.
            }
            //Duplicate code not needed
            /* else if (found == false) 
            {
                return (0, "no", "not_found");
                //Console.WriteLine("impossible");// end the program if I can not find a way!
                //System.Environment.Exit(1);
            }*/
            else
            {
                return (0, "no", "not_found");
            }
        }

        static public void dubug_print(Value[,] grid, uint node, ushort destination)

        {
            (byte destination_x, byte destination_y) = Convert_number.ushort_to_byte(destination);
            (byte person_x, byte person_y, byte koelkast_x, byte koelkast_y) = Convert_number.from_int_to_byte(node);

            Console.WriteLine("");
            for (byte y = 0; y < grid.GetLength(1) - 1; y++)
            {
                Console.Write("\n");
                for (byte x = 0; x < grid.GetLength(0) - 1; x++)
                {
                    Value val = grid[x, y];
                    if (person_x == x && person_y == y)
                    {
                        Console.Write("+");

                    }
                    else if (koelkast_x == x && koelkast_y == y)
                    {
                        Console.Write("!");
                    }
                    else if (destination_x == x && destination_y == y)
                    {
                        Console.Write("?");
                    }
                    else
                    {
                        if (val == Value.Wall)
                        {
                            Console.Write("M");
                        }
                        else if (val == Value.way)
                        {
                            Console.Write(".");
                        }

                    }
                }
            }
        }

        static List<uint> find_childrens_int(uint num, Value[,] grid)
        {


            var height_grid = (byte)grid.GetLength(0);
            var v_size = (byte)grid.GetLength(1);
            List<uint> output = new List<uint>();
            (byte px, byte py, byte kx, byte ky) = Convert_number.from_int_to_byte(num);
            // Loop in 4 orthogonal directions
            byte new_px, new_py;
            byte new_kx, new_ky;
            var height = grid.Length;
            int dirx, diry;
            for (sbyte p = 2; p >= -1; p--)
            {
                dirx = p * (byte)p % 2;
                diry = (1 - p) * (byte)(1 - p) % 2;

                new_px = (byte)(px + dirx);
                new_py = (byte)(py + diry);

                if (grid[new_px, new_py] == Value.Wall)
                {
                    continue;
                }

                // Outside of byte range is concidered a wall
                else if (px + dirx < byte.MinValue || px + dirx >= 256 || py + diry < byte.MinValue || py + diry >= 256)
                {
                    continue;
                }

                else if (new_px == kx && new_py == ky && grid[(byte)(kx + dirx), (byte)(ky + diry)] != Value.Wall)
                {

                    uint add_value = Convert_number.from_byte_to_int(new_px, new_py, (byte)(kx + dirx), (byte)(ky + diry));
                    output.Add(add_value);
                }

                else
                {
                    if (new_px == kx && new_py == ky)
                    {
                        continue;
                    }
                    else
                    {
                        uint add_value_second = Convert_number.from_byte_to_int(new_px, new_py, kx, ky);
                        output.Add(add_value_second);
                    }

                }

            }
            return output;
        } // option 2

        static (ushort, string) get_path_from_visited(uint node, Dictionary<uint, uint> visited, Value[,] grid, ushort target)
        {
            List<uint> found_way = new List<uint>();
            ushort steps = 0;
            uint previous = visited[node];
            string reversed_path = "";
            byte conf_px, conf_py, prev_px, prev_py, _bx, _by;
            (conf_px, conf_py, _bx, _by) = Convert_number.from_int_to_byte(node);
            while (previous != 0) // check that there is no previous ==> we arrive to the beggining
            {
                found_way.Add(previous);
                (prev_px, prev_py, _bx, _by) = Convert_number.from_int_to_byte(previous);

                // To reverse the direction of the ==> We know which riection we took
                reversed_path += Convert_number.get_letter_for_path(prev_px, prev_py, conf_px, conf_py);
                node = previous;
                previous = visited[node];
                conf_px = prev_px;
                conf_py = prev_py;
                steps++;
            }
            foreach (uint node_print in found_way)
            {
                //dubug_print(grid, node_print, target);
            }
            // Reverse the path to get from start to end
            string path = "";
            foreach (char c in reversed_path)
                path = c + path;

            return (steps, path);
        }

        static void printing_values(string operation, string path, ushort steps, string result)
        {
            if (result == "not_found")
            {
                Console.WriteLine("No solution");
            }
            else if (result == "found")
            {
                if (operation == "p")
                {
                    Console.WriteLine(steps);
                    Console.WriteLine(path);

                }
                else if (operation == "l")
                {
                    Console.WriteLine(steps);
                }
            }

            else
            {
                Console.WriteLine("No solution"); //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }

    }
    // helping material
    class Convert_number
    {
        static public char get_letter_for_path(byte p1x, byte p1y, byte p2x, byte p2y)
        {
            int diection_x = p2x - p1x;
            int direction_y = p2y - p1y;

            if (diection_x > 0 && direction_y == 0)
            {
                return 'E';

            }
            else if (diection_x == 0 && direction_y < 0)
            {
                return 'N';
            }
            else if (diection_x < 0 && direction_y == 0)
            {
                return 'W';
            }
            else if (diection_x == 0 && direction_y > 0)
            {
                return 'S';
            }

            else
            {
                return '?';
            }
        }

        public static uint from_byte_to_int(byte px, byte py, byte kx, byte ky)
        {
            return (uint)((px << 24) | (py << 16) | (kx << 8) | ky);
        }
        public static (byte px, byte py, byte kx, byte ky) from_int_to_byte(uint input)
        {
            return ((byte)(input >> 24), (byte)(input >> 16), (byte)(input >> 8), (byte)input);
        }

        public static ushort two_bytes_to_num(byte x_val, byte y_val) // converting between bytes to short.
        {
            return (ushort)((x_val << 8) | y_val);
        }
        public static ushort two_bytes_location(byte x_val, byte y_val) // convertint it to a number ==> calling another function.
        {
            return (two_bytes_to_num(x_val, y_val));
        }

        public static (byte, byte) ushort_to_byte(uint input)
        {
            return ((byte)(input >> 8), (byte)(input));
        }

        public static (byte, byte) convert_uint_to_ushort_byte(uint config)
        {
            return ((byte)(config >> 8), (byte)(config));
        }
        public static uint combine_two_ushorts(ushort b1, ushort b2)
        {
            return (uint)((b1 << 16) | b2);
        }
    }