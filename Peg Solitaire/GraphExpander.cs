using MoreLinq.Extensions;
using SharpGraph.Core;

namespace Peg_Solitair;

public class GraphExpander<TVertex, TEdge>
    where TVertex : BoardNode
    where TEdge : Line<TVertex>
{
    private int _previousVerticesCount;

    public async Task ExpandSinks(Graph<TVertex, TEdge> graph, Func<TVertex, IEnumerable<TEdge>> expand)
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
                    .WithDegreeOfParallelism(10)
                    .SelectMany(expand)
                    .ToArray();

                expandedNodes
                    .ForEach(e =>
                    {
                        if (!graph.Vertices.Contains(e.Point2))
                        {
                            graph.AddVertex(e.Point2);
                        }

                        if (!graph.Edges.Contains(e))
                        {
                            graph.AddEdge(e);
                        }
                    });
            });

        Console.WriteLine($"Working with {nodeCandidates.Length} nodes. Vertices diff {graph.Vertices.Count - _previousVerticesCount}, total {graph.Vertices.Count}. Ignored {graph.Vertices.Count - nodeCandidates.Length} nodes.");
        _previousVerticesCount = graph.Vertices.Count;

        await ExpandSinks(graph, expand).ConfigureAwait(false);
    }

    public int CurrentDepth { get; private set; }
}