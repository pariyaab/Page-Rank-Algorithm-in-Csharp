namespace core.pagerank;

using System.Collections.Generic;
using customedge;
using QuickGraph;

public class PageRankProviderMgr
{
    public AdjacencyGraph<string, Edge<string>> graph;
    public AdjacencyGraph<string, CustomEdge<string>> wgrpah = new AdjacencyGraph<string, CustomEdge<string>>();
    public PageRankProviderMgr(AdjacencyGraph<string, CustomEdge<string>> wgraph) => this.wgrpah = wgraph;
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
    public Dictionary<string, double> GetPageRanksWeighted(bool normalize)
    {
        if (this.wgrpah != null)
        {
            HashSet<String> vertices = new HashSet<String>(this.wgrpah.Vertices);
            Dictionary<String, Double> tokendb = new Dictionary<String, Double>();
            foreach (String token in vertices)
            {
                tokendb[token] = 0.0;
            }
            // now get the pageRank
            PageRankProviderWeighted prProvider = new PageRankProviderWeighted(wgrpah, tokendb);
            return prProvider.calculatePageRankWeighted(normalize);
        }
        return null;
    }
}
