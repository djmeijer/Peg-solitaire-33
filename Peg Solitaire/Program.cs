using System.Diagnostics;
using System.Globalization;
using SharpGraph.Core;

namespace Peg_Solitair;

public static class Program
{
  static async Task Main()
  {
    // Set culture for formatting number with thousand separator.
    CultureInfo.DefaultThreadCurrentCulture = new("nl-NL");

    var defaultBeginBoard = BoardNodeFactory.GetDefaultBeginBoard();
    var defaultEndBoard = BoardNodeFactory.GetDefaultEndBoard();
    var game = new Game();
    var graphExpander = new GraphExpander<BoardNode, Move>();
    var graph = new Graph<BoardNode, Move>();
    graph.AddVertex(defaultBeginBoard);

    // Register an timer to update the status to the console periodically.
    new Timer(c => TimerCallback(graph, defaultBeginBoard, defaultEndBoard, graphExpander), null, 0, 1000);

    // This start the heavy lifting.
    await graphExpander.ExpandSinks(graph, b => game.GraphExpand(graph, b)).ConfigureAwait(false);

    // Compute the successful paths.
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var numberOfSolutions = graph.AllPaths(defaultBeginBoard, defaultEndBoard).Count;
    stopwatch.Stop();

    // Show all the results and a successful path.      
    Console.WriteLine($"It took {stopwatch.ElapsedMilliseconds} milliseconds to compute all the successful paths.");
    Console.WriteLine("Tree is exhaustively searched.");
    Console.WriteLine($"There are {numberOfSolutions} possible ways to solve the problem.");
    Console.WriteLine("A possible solution is as follows.");
    PrintWinningPath(graph, defaultBeginBoard, defaultEndBoard);

    Console.ReadKey();
  }

  private static void TimerCallback(
    Graph<BoardNode, Move> graph,
    BoardNode begin,
    BoardNode end,
    GraphExpander<BoardNode, Move> graphExpander)
  {
    var numberOfSolutions = !graph.Vertices.Contains(end) ? 0 : graph.AllPaths(begin, end).Count;
    var depth = graphExpander.CurrentDepth;
    Console.WriteLine($"{DateTime.Now:h:mm:ss} - processed {graph.Vertices.Count.ToString("N0")} items, {numberOfSolutions} solution(s), depth is {depth}.");

    // PrintWinningPath(graph, begin, end);
  }

  private static void PrintWinningPath(Graph<BoardNode, Move> graph, BoardNode currentNode, BoardNode endNode)
  {
    Console.WriteLine(currentNode.ToString());
    if(currentNode.Equals(endNode))
    {
      return;
    }

    var next = graph.AdjacentVertices(currentNode).FirstOrDefault();
    if (next != default)
    {
      PrintWinningPath(graph, next, endNode); 
    }
  }
}