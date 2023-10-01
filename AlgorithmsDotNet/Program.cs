using System.Diagnostics;
namespace Algorithms
{
    class AlgorithmsDotNet
    {
        private static readonly HashSet<string> hashSet = new();
        private static readonly BinaryTreeSet<string> tree = new();
        static void Main(string[] args)
        {
            bool DoHashSet = true;
            bool DoBinaryTree = true;
            if (args.Length < 1)
            {
                Console.Error.WriteLine("No Input File was given");
                return;
            }
            else if (args.Length == 2)
            {
                // given hashSet
                bool.TryParse(args[1], out DoHashSet);
            }
            else if (args.Length == 3)
            {
                // given both
                bool.TryParse(args[1], out DoHashSet);
                bool.TryParse(args[2], out DoBinaryTree);
            }
            // Console.WriteLine(args[0]);
            if (File.Exists(args[0]))
            {
                Stopwatch stopwatch = new();
                // first do the HashSet implementation
                if (DoHashSet)
                {
                    stopwatch.Start();
                    getWordsUsingHashSet(args[0]);
                    stopwatch.Stop();
                    Console.WriteLine($"Total time for Hashing function to fully run: {stopwatch.Elapsed}");
                    stopwatch.Reset();
                }
                if (DoBinaryTree)
                {
                    // next, do the BinaryTreeSet implementation
                    stopwatch.Start();
                    getWordsUsingTree(args[0]);
                    stopwatch.Stop();
                    Console.WriteLine($"Total time for Binary Tree function to fully run: {stopwatch.Elapsed}");
                }
            }
            //if (DoHashSet)
            //{
            //    Console.WriteLine("Print out all hashset items");
            //    foreach (string item in hashSet)
            //    {
            //        Console.Write(item + ", ");
            //    }
            //}
            //Console.WriteLine();
            //if (DoBinaryTree)
            //{
            //    Console.WriteLine("Print out all Binary Tree items");
            //    tree.PrintTree();
            //}
            return;
        }

        public static int getWordsUsingHashSet(string fileName)
        {
            int counter = 0;
            Stopwatch stopwatch = new();
            // Read in the file by characters
            using (StreamReader file = new StreamReader(fileName))
            {
                char? ch;
                string word = "";
                while ((ch = (char)file.Read()) != null)
                {
                    // check if the char is a letter or not, from testing _ counts as a letter for some reason
                    if (char.IsLetter(ch.Value))
                    {
                        // add on to the word here
                        word += ch;
                    }
                    else
                    {
                        // this is the end of a word
                        // first check if this is a 5 letter word so far
                        if (word.Length == 5)
                        {
                            // add to dictionary
                            word = word.ToLower();
                            // time just the add
                            stopwatch.Start();
                            bool success = hashSet.Add(word);
                            if (success)
                            {
                                counter++;
                            }
                            stopwatch.Stop();
                        }
                        word = "";
                        if (file.EndOfStream)
                        {
                            break;
                        }
                    }
                }
                file.Close();
            }
            Console.WriteLine($"Time to add every item to the hashSet, without reading or checking for 5 letters: {stopwatch.Elapsed}");
            return counter;
        }

        public static int getWordsUsingTree(string fileName)
        {
            int counter = 0;
            Stopwatch stopwatch = new();
            // Read in the file by characters
            using (StreamReader file = new StreamReader(fileName))
            {
                char? ch;
                string word = "";
                while ((ch = (char)file.Read()) != null)
                {
                    if (char.IsLetter(ch.Value))
                    {
                        // add on to the word here
                        word += ch;
                    }
                    else
                    {
                        // this is the end of a word
                        // first check if this is a 5 letter word so far
                        if (word.Length == 5)
                        {
                            // add to dictionary
                            word = word.ToLower();
                            stopwatch.Start();
                            bool success = tree.Add(word);
                            if (success)
                            {
                                counter++;
                            }
                            stopwatch.Stop();
                        }
                        word = "";
                        if (file.EndOfStream)
                        {
                            break;
                        }
                    }
                }
                file.Close();
            }
            Console.WriteLine($"Time to add every item to the binary tree set, without reading or checking for 5 letters: {stopwatch.Elapsed}");
            return counter;
        }
    }
    // My implementation of a simple Binary tree that can add items
    // and only return true if new item added
    public class BinaryTreeSet<T> where T : IComparable<T>
    {
        private Node<T>? Root { get; set; }
        public BinaryTreeSet()
        {
            Root = null;
        }
        // only add if it isn't already there
        public bool Add(T value)
        {
            if (Root == null)
            {
                Root = new Node<T>(value);
                return true;
            }
            else
            {
                return Root.Add(value);
            }
        }

        public void PrintTree()
        {
            if (Root == null) return;
            Root.PrintTree(Root);
        }
    }

    public class Node<T> where T : IComparable<T>
    {
        public T Value { get; private set; }
        public Node<T> Left { get; private set; } = null!;
        public Node<T> Right { get; private set; } = null!;

        public Node(T value) => Value = value;

        public bool Add(T newValue)
        {
            // find out if the string is less than or greater than
            // then go that direction
            // if that direction is null, then add a new node
            if (newValue.CompareTo(Value) < 0)
            {
                if (Left == null)
                {
                    Left = new Node<T>(newValue);
                    return true;
                }
                else
                {
                    return Left.Add(newValue);
                }
            }
            else if (newValue.CompareTo(Value) > 0)
            {
                if (Right == null)
                {
                    Right = new Node<T>(newValue);
                    return true;
                }
                else
                {
                    return Right.Add(newValue);
                }
            }
            // this is the same string therefore it is already here
            else
            {
                return false;
            }
        }
        public void PrintTree(Node<T> node)
        {
            if (node == null)
            {
                return;
            }
            PrintTree(node.Left);
            Console.Write(node.Value + ", ");
            PrintTree(node.Right);
        }
    }
}