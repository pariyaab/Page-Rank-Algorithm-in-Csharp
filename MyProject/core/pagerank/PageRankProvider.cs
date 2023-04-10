namespace core.pagerank;
using ca.usask.cs.srlab.pagerank.config;
using customedge;
using QuickGraph;

public class PageRankProvider
{
    public AdjacencyGraph<string, Edge<string>> graph = new AdjacencyGraph<string, Edge<string>>();
    Dictionary<string, double> tokendb = new Dictionary<string, double>();
    Dictionary<string, double> oldScoreMap = new Dictionary<string, double>();
    Dictionary<string, double> newScoreMap = new Dictionary<string, double>();
    const double INITIAL_VERTEX_SCORE = StaticData.INITIAL_VERTEX_SCORE;
    const double DAMPING_FACTOR = StaticData.DAMPING_FACTOR;
    const int MAX_ITERATION = StaticData.MAX_ITERATION;

    public void InitilizeConstructor(Dictionary<string, double> tokendb)
    {
        this.tokendb = tokendb;
        this.oldScoreMap = new Dictionary<string, double>();
        this.newScoreMap = new Dictionary<string, double>();
    }

    public PageRankProvider(
        AdjacencyGraph<string, Edge<string>> graph,
        Dictionary<string, double> tokendb
    )
    {
        // un-weighted graph constructor
        this.graph = graph;
        InitilizeConstructor(tokendb);
    }

    public bool checkSignificantDiff(double oldV, double newV)
    {
        double diff = (newV > oldV) ? newV - oldV : oldV - newV;
        return diff > StaticData.SIGNIFICANCE_THRESHOLD ? true : false;
    }

    // A function for retriving incoming edges
    public static HashSet<Edge<string>> getIncomingEdges(
        string vertex,
        AdjacencyGraph<string, Edge<string>> graph
    )
    {
        HashSet<Edge<string>> incomingEdges = new HashSet<Edge<string>>();

        foreach (Edge<string> edge in graph.Edges)
        {
            if (edge.Target == vertex)
            {
                incomingEdges.Add(edge);
            }
        }

        return incomingEdges;
    }

    // A function for getting out-degree of given vertex
    public int getOutDegree(string vertex, AdjacencyGraph<string, Edge<string>> graph)
    {
        int outDegree = 0;
        foreach (Edge<string> edge in graph.Edges)
        {
            if (edge.Source == vertex)
            {
                outDegree++;
            }
        }
        return outDegree;
    }

    // Write page rank calculator function for un-weighted graph
    public Dictionary<string, double> calculatePageRank(bool normalize)
    {
        // calculating token rank score
        // initially putting 1 to all
        foreach (string vertex in graph.Vertices)
        {
            oldScoreMap[vertex] = INITIAL_VERTEX_SCORE;
            newScoreMap[vertex] = INITIAL_VERTEX_SCORE;
        }
        bool enoughIteration = false;
        int itercount = 0;
        while (!enoughIteration)
        {
            int inSignificant = 0;
            foreach (string vertex in graph.Vertices)
            {
                HashSet<Edge<string>> incomings = getIncomingEdges(vertex, graph);
                // now calculate the PR score
                double trank = (1 - DAMPING_FACTOR);
                double comingScore = 0;
                foreach (Edge<string> edge in incomings)
                {
                    string source1 = edge.Source;
                    // int outdegree = graph.OutEdges(vertex).Count();
                    int outdegree = getOutDegree(source1, graph);
                    // score and out degree should be affected by the edge weight
                    double score = oldScoreMap[source1];
                    if (outdegree == 1)
                        comingScore += score;
                    else if (outdegree > 1)
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
            if (inSignificant == graph.VertexCount)
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

    // normalize scores by dividing them on their max value
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
