namespace core.pagerank;
using ca.usask.cs.srlab.pagerank.config;
using customedge;
using QuickGraph;

public class PageRankProviderWeighted
{
    public AdjacencyGraph<string, CustomEdge<string>> wgrpah =
        new AdjacencyGraph<string, CustomEdge<string>>();
    Dictionary<string, double> tokendb = new Dictionary<string, double>();
    Dictionary<string, double> oldScoreMap = new Dictionary<string, double>();
    Dictionary<string, double> newScoreMap = new Dictionary<string, double>();
    const double EDGE_WEIGHT_TH = StaticData.EDGE_WEIGHT_TH;
    const double INITIAL_VERTEX_SCORE = StaticData.INITIAL_VERTEX_SCORE;
    const double DAMPING_FACTOR = StaticData.DAMPING_FACTOR;
    const int MAX_ITERATION = StaticData.MAX_ITERATION;

    public void InitilizeConstructor(Dictionary<string, double> tokendb)
    {
        this.tokendb = tokendb;
        this.oldScoreMap = new Dictionary<string, double>();
        this.newScoreMap = new Dictionary<string, double>();
    }

    public PageRankProviderWeighted(
        AdjacencyGraph<string, CustomEdge<string>> wgraph,
        Dictionary<string, double> tokendb
    )
    {
        this.wgrpah = wgraph;
        InitilizeConstructor(tokendb);
    }

    public bool checkSignificantDiff(double oldV, double newV)
    {
        double diff = (newV > oldV) ? newV - oldV : oldV - newV;
        return diff > StaticData.SIGNIFICANCE_THRESHOLD ? true : false;
    }

    // A function for retriving incoming edges

    public static HashSet<CustomEdge<string>> getIncomingWeightedEdges(
        string vertex,
        AdjacencyGraph<string, CustomEdge<string>> graph
    )
    {
        HashSet<CustomEdge<string>> incomingEdges = new HashSet<CustomEdge<string>>();

        foreach (CustomEdge<string> edge in graph.Edges)
        {
            if (edge.Target == vertex)
            {
                incomingEdges.Add(edge);
            }
        }

        return incomingEdges;
    }

    // A function for getting out-degree of given vertex

    public int getOutWeightedDegree(string vertex, AdjacencyGraph<string, CustomEdge<string>> graph)
    {
        int outDegree = 0;
        foreach (CustomEdge<string> edge in graph.Edges)
        {
            if (edge.Source == vertex)
            {
                outDegree++;
            }
        }
        return outDegree;
    }

    // Write page rank calculator function for weighted graph
    public Dictionary<string, double> calculatePageRankWeighted(bool normalize)
    {
        // calculating token rank score
        // initially putting 1 to all
        foreach (string vertex in wgrpah.Vertices)
        {
            oldScoreMap[vertex] = INITIAL_VERTEX_SCORE;
            newScoreMap[vertex] = INITIAL_VERTEX_SCORE;
        }
        bool enoughIteration = false;
        int itercount = 0;
        while (!enoughIteration)
        {
            int inSignificant = 0;
            foreach (string vertex in wgrpah.Vertices)
            {
                HashSet<CustomEdge<string>> incomings = getIncomingWeightedEdges(vertex, wgrpah);
                // now calculate the PR score
                double trank = (1 - DAMPING_FACTOR);
                double comingScore = 0;
                foreach (CustomEdge<string> edge in incomings)
                {
                    string source1 = edge.Source;
                    // int outdegree = graph.OutEdges(vertex).Count();
                    int outdegree = getOutWeightedDegree(source1, wgrpah);
                    // score and out degree should be affected by the edge weight
                    double edgeWeight = edge.GetWeight();
                    double score = oldScoreMap[source1] * edgeWeight;
                    if (outdegree == 0)
                        comingScore += score;
                    else
                        comingScore += (score / outdegree);
                }
                comingScore = comingScore * DAMPING_FACTOR;
                trank += comingScore;
                _ = checkSignificantDiff(oldScoreMap[vertex], trank)
                    ? newScoreMap[vertex] = trank
                    : inSignificant++;
            }
            // coping values to new Hash Map
            foreach (string key in newScoreMap.Keys)
            {
                oldScoreMap[key] = newScoreMap[key];
            }
            itercount++;
            if (inSignificant == wgrpah.VertexCount)
            {
                enoughIteration = true;
            }
            if (itercount == MAX_ITERATION)
            {
                enoughIteration = true;
            }
        }
        // saving token ranks
        if (normalize)
            recordNormalizedScores();
        else
            recordOriginalScores();
        return tokendb;
    }

    protected void recordNormalizedScores()
    {
        // record normalized scores
        double maxRank = 0.0;
        foreach (string key in newScoreMap.Keys)
        {
            double score = newScoreMap[key];
            if (score > maxRank)
            {
                maxRank = score;
            }
        }
        foreach (string key in newScoreMap.Keys)
        {
            double score = newScoreMap[key];
            score = score / maxRank;
            tokendb[key] = score;
        }
    }

    protected void recordOriginalScores()
    {
        foreach (string key in newScoreMap.Keys)
        {
            double score = newScoreMap[key];
            tokendb[key] = score;
        }
    }
}
