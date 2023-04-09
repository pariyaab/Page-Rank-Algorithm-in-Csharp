using System;
using core.pagerank;
using QuickGraph;

namespace MyProject
{
    class Program
    {
        public static void Main(string[] args)
        {
            var graph = new AdjacencyGraph<string, Edge<string>>();
            // add the vertex
            graph.AddVertex("1");
            graph.AddVertex("2");
            graph.AddVertex("3");
            graph.AddVertex("4");

            // add the edges
            graph.AddEdge(new Edge<string>("1", "2"));
            graph.AddEdge(new Edge<string>("2", "3"));
            graph.AddEdge(new Edge<string>("2", "4"));
            graph.AddEdge(new Edge<string>("3", "2"));
            graph.AddEdge(new Edge<string>("4", "3"));

            bool normalizeScore = false;

            var tokendb = new PageRankProviderMgr(graph).GetPageRanks(normalizeScore);
            Console.WriteLine(string.Join(", ", tokendb.Select(kv => $"{kv.Key}: {kv.Value}")));
        }
    }
}
