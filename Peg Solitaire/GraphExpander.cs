using MoreLinq.Extensions;
using SharpGraph.Core;

namespace Peg_Solitair;

public class GraphExpander
{
    private int _previousVerticesCount;

    public void ExpandSinks(Graph<Board, Move> graph, HashSet<string> cache, Func<Board, IEnumerable<Move>> expand)
    {
        CurrentDepth++;

        var nodeCandidates = graph.Sinks.Where(s => !s.Visited).ToArray();
        if (!nodeCandidates.Any())
        {
            return;
        }
        
        nodeCandidates
            .Batch(10000)
            .ForEach(b =>
            {
                var expandedNodes = b
                    .AsParallel()
                    .SelectMany(expand)
                    .ToArray();

                expandedNodes
                    .ForEach(e =>
                    {
                        if (!graph.Vertices.Contains(e.Target))
                        {
                            cache.Add(e.TargetIdentifier);
                            graph.AddVertex(e.Target);
                        }

                        if (!graph.Edges.Contains(e))
                        {
                            graph.AddEdge(e);
                        }
                    });
            });

        Console.WriteLine($"Working with {nodeCandidates.Length:N0} nodes. Vertices diff {graph.Vertices.Count - _previousVerticesCount:N0}, total {graph.Vertices.Count:N0}. Ignored {graph.Vertices.Count - nodeCandidates.Length:N0} nodes.");
        _previousVerticesCount = graph.Vertices.Count;

        ExpandSinks(graph, cache, expand);
    }

    public int CurrentDepth { get; private set; }
}