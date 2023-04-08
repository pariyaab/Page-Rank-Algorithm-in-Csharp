using System;
namespace ca.usask.cs.srlab.pagerank.config
{
public interface StaticData
{
// public attributes used by PageRank
    public static double SIGNIFICANCE_THRESHOLD = 0.0001;
    public const int WINDOW_SIZE = 2;

    public const double EDGE_WEIGHT_TH = 0.25;
    public const double INITIAL_VERTEX_SCORE = 0.25;
    public const double DAMPING_FACTOR = 0.85;
    public const int MAX_ITERATION = 100;
}