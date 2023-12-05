namespace Algorithms
{
    class AlgorithmsDotNet
    {
        private static List<Node> nodes = new();
        private static int numberOfCapitals = 0;
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("No Input File was given");
                return;
            }
            // Console.WriteLine(args[0]);
            if (File.Exists(args[0]))
            {
                string[] lines = File.ReadAllLines(args[0]);
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
                // start doing the randomized nodes and saving minimum
                // to make sure it doesn't go to one already visited
                // make a list of all capitals, 0-numberOfCapitals
                // randomize from this list
            }
            else
            {
                Console.Error.WriteLine($"Unable to open file: {args[0]}");
            }
            return;
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
            int.TryParse(numbers.ElementAtOrDefault(0), out int d);
            this.Source = d;
            int.TryParse(numbers.ElementAtOrDefault(0), out int w);
            this.Source = w;
        }
    }
}
