using System;
using core.pagerank;
using QuickGraph;
using customedge;

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

            // test weighted graph
            
            var wgraph = new AdjacencyGraph<string, CustomEdge<string>>();
            // add the vertex
            wgraph.AddVertex("1");
            wgraph.AddVertex("2");
            wgraph.AddVertex("3");
            wgraph.AddVertex("4");

            // add the edges
            wgraph.AddEdge(new CustomEdge<string>("1", "2", 1.0));
            wgraph.AddEdge(new CustomEdge<string>("2", "3", 1.0));
            wgraph.AddEdge(new CustomEdge<string>("2", "4", 1.0));
            wgraph.AddEdge(new CustomEdge<string>("3", "2", 1.0));
            wgraph.AddEdge(new CustomEdge<string>("4", "3", 1.0));

            var tokendbweighted = new PageRankProviderMgr(wgraph).GetPageRanksWeighted(normalizeScore);
            Console.WriteLine(string.Join(", ", tokendbweighted.Select(kv => $"{kv.Key}: {kv.Value}")));
        }
    }
}
