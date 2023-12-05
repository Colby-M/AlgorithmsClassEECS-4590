using System.Collections;

namespace Algorithms
{
    class AlgorithmsDotNet
    {
        private static int iterations = 100000;
        private static bool printEach = false;

        private static List<Node> nodes = new();
        private static int numberOfCapitals = 0;
        private static List<int> bestOrder = new();
        private static int bestTime = int.MaxValue;
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("No Input File was given");
                return;
            }

            if (File.Exists(args.ElementAtOrDefault(0)))
            {
                string[] lines = File.ReadAllLines(args.ElementAtOrDefault(0) ?? "");
                int.TryParse(lines.ElementAtOrDefault(0), out numberOfCapitals);
                
                for (int i = 1; i < lines.Length - 1; i++)
                {
                    // get all lines minus the first and last
                    nodes.Add(new(lines.ElementAtOrDefault(i)));
                }
                if (nodes.Count != lines.Length - 2)
                {
                    // problem somewhere getting a node
                    Console.Error.WriteLine("Error getting all nodes");
                    return;
                }

                // have all nodes into list with custom objects
                // loop through this n times and print out each order and time
                for (int i = 0; i < iterations; i++)
                {
                    (List<int> order, int time) = findTime();
                    if (printEach)
                    {
                        Console.WriteLine($"{string.Join(',', order)}  {time}");
                    }
                    if (time < bestTime)
                    {
                        bestTime = time;
                        bestOrder = order;
                    }
                }

                Console.WriteLine($"Best Order found: {string.Join(',', bestOrder)}");
                Console.WriteLine($"Best Time found: {bestTime}");
            }
            else
            {
                Console.Error.WriteLine($"Unable to open file: {args.ElementAtOrDefault(0)}");
            }
            return;
        }

        static (List<int> order, int time) findTime()
        {
            List<int> order = new();
            int time = 0;
            // get the simple array, 0 to number of Capitals - 1
            for (int i = 0; i < numberOfCapitals; i++)
            {
                order.Add(i);
            }
            // randomize array
            shuffle(ref order);
            for (int i = 0; i < order.Count - 1; i++)
            {
                int source = order.ElementAtOrDefault(i);
                int destination = order.ElementAtOrDefault(i + 1);
                Node currentNode = nodes.FirstOrDefault(x => x.Source == source && x.Destination == destination)
                    ?? throw new ArgumentException("Error finding Node");
                time += currentNode.Weight;
            }
            return (order, time);
        }

        public static void shuffle(ref List<int> array)
        {
            Random rng = new();
            int n = array.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

    class Node
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }

        public Node(string? line)
        {
            ArgumentNullException.ThrowIfNull(line);
            string[] numbers = line.Split(' ');
            int.TryParse(numbers.ElementAtOrDefault(0), out int s);
            this.Source = s;
            int.TryParse(numbers.ElementAtOrDefault(1), out int d);
            this.Destination = d;
            int.TryParse(numbers.ElementAtOrDefault(2), out int w);
            this.Weight = w;
        }
    }
}
