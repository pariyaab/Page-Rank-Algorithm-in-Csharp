# Page-Rank-Algorithm-in-C#

The Java source code for this repository can be found on this page (https://github.com/masud-technope/PageRank-MR), and I would like to express my gratitude to Dr. Rahman for his codes.

## Page Rank Algorithm Definition
PageRank is an algorithm used by search engines to rank web pages in their search engine results. It was invented by Google co-founder Larry Page and named after him. The idea behind PageRank is that a webpage's importance can be determined by the number and quality of other web pages linking to it.  

In PageRank, a webpage is viewed as a node in a graph, and links between pages are represented as edges in the graph. The algorithm assigns a score to each node (webpage) in the graph, which represents the probability that a user would arrive at that page by following links from other pages.  

The PageRank algorithm works by iteratively calculating the score for each page based on the scores of the pages that link to it. The score for each page is influenced by the number and quality of pages that link to it, as well as the scores of those pages. This process continues until the scores converge, at which point the algorithm terminates.  

PageRank has become one of the most important algorithms in search engine optimization (SEO), and has been widely used by search engines such as Google, Yahoo, and Bing.  

## How Our Algorithm Works for Weighted Graphs

In `PageRankProviderWeighted.cs` file, I implemented Page Rank algorithm in C#. The `calculatePageRankWeighted` function takes a boolean parameter 'normalize' which is used to determine whether to return the normalized scores or the original scores. The PageRank algorithm is an iterative algorithm that calculates the score for each vertex based on the score of its incoming vertices and the weights of the edges connecting them. The function first initializes the oldScoreMap and newScoreMap dictionaries with an initial score of 1 for each vertex. It then iterates over each vertex in the graph and calculates its PageRank score based on the scores of its incoming vertices and the weights of the edges connecting them. The function uses the getIncomingWeightedEdges and getOutWeightedDegree helper functions to get the incoming edges and out-degree for each vertex, respectively. The algorithm continues to iterate until either the score of each vertex converges or the maximum number of iterations is reached. Once the algorithm converges or the maximum number of iterations is reached, the function records the PageRank scores for each vertex in the tokendb dictionary and returns it. If the 'normalize' parameter is set to true, the function normalizes the scores before returning them.  

## How Our Algorithm Works for Un-Weighted Graphs

The algorithm for un-weighted graphs is similar to the one used for weighted graphs, but without taking the edge weights into account.