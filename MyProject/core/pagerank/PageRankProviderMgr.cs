namespace core.pagerank;

using System.Collections.Generic;
using System.Linq;
using QuickGraph;

public class PageRankProviderMgr
{
    public AdjacencyGraph<string, Edge<string>> graph;

    public PageRankProviderMgr(AdjacencyGraph<string, Edge<string>> graph)
    {
        this.graph = graph;
    }

    public Dictionary<string, double> GetPageRanks(bool normalize)
    {
        if (this.graph != null)
        {
            HashSet<String> vertices = new HashSet<String>(this.graph.Vertices);
            Dictionary<String, Double> tokendb = new Dictionary<String, Double>();
            foreach (String token in vertices)
            {
                tokendb[token] = 0.0;
            }
            // now get the pageRank
            PageRankProvider prProvider = new PageRankProvider(graph, tokendb);
            return prProvider.calculatePageRank(normalize);
        }
        return null;
    }
}
