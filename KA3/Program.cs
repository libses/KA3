using System;
using System.Collections.Generic;
using System.Linq;

namespace KA3
{
    public class Node
    {
        public int Number;
        public List<Edge> Ingoing = new List<Edge>();
        public List<Edge> Outgoing = new List<Edge>();
        public Edge Parent;
        public int Value = int.MaxValue;
    }

    public class Edge
    {
        public Node Start;
        public Node End;
        public int Value;

        public Edge(Node s, Node e, int v)
        {
            Start = s;
            End = e;
            Value = v;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var hashset = new HashSet<Node>();
            var n = int.Parse(Console.ReadLine());
            var nodes = Enumerable.Range(0, n).Select(x => new Node() { Number = x}).ToArray();
            for (int i = 0; i < n; i++)
            {
                var currentNode = nodes[i];
                var input = Console.ReadLine();
                var array = input.Split().Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j + 1 < array.Length; j += 2)
                {
                    var outerNode = nodes[array[j] - 1];
                    var value = array[j + 1];
                    var edge = new Edge(outerNode, currentNode, value);
                    outerNode.Outgoing.Add(edge);
                    currentNode.Ingoing.Add(edge);
                }
            }

            var start = int.Parse(Console.ReadLine()) - 1;
            var end = int.Parse(Console.ReadLine()) - 1;

            var startNode = nodes[start];
            startNode.Value = 0;
            while (hashset.Count != n)
            {
                var max = nodes.Where(x => !hashset.Contains(x)).Min(x => (x.Value, x)).x;
                foreach (var edge in max.Outgoing)
                {
                    if (hashset.Contains(edge.End))
                    {
                        continue;
                    }

                    if (edge.End.Value > edge.Value)
                    {
                        edge.End.Value = edge.Value;
                        edge.End.Parent = edge;
                    }
                }

                hashset.Add(max);
            }

            var endNode = nodes[end];
            if (endNode.Value < 0)
            {
                Console.WriteLine("N");
            }
            else
            {
                Console.WriteLine("Y");
                var parent = endNode;
                var result = "";
                var max = 0;
                while (parent != null)
                {
                    result += " " + (parent.Number + 1);
                    if (parent.Parent == null) break;
                    if (parent.Parent.Value > max)
                    {
                        max = parent.Parent.Value;
                    }

                    parent = parent.Parent.Start;
                }

                Console.WriteLine(new string(result.Reverse().ToArray()));
                Console.WriteLine(max);
            }  
        }
    }
}
