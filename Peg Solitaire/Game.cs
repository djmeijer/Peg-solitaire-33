using MoreLinq;
using SharpGraph.Core;

namespace Peg_Solitair;

public class Game
{
  private readonly int[][] _grid;
  private readonly IReadOnlyCollection<(int value, int x, int y)> _indexedGrid;
  private readonly Graph<Board, Move> _graph;
  private readonly GraphExpander _graphExpander;
  private readonly HashSet<string> _cache;
  private readonly Board _beginBoard;
  private readonly Board _endBoard;
  private readonly Direction[] _directions;

  public Game(Board beginBoard, Board endBoard)
  {
    _grid = GridFactory.GetGrid();
    _indexedGrid = GridFactory.GetIndexedGrid();
    _graph = new Graph<Board, Move>();
    _graph.AddVertex(beginBoard);
    _graphExpander = new GraphExpander();
    _cache = new HashSet<string> {beginBoard.Identifier};
    _beginBoard = beginBoard;
    _endBoard = endBoard;
    _directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToArray();
  }

  private IEnumerable<Move> GraphExpand(Board node)
  {
    node.Visited = true;

    return node
      .GetEmptySpots()
      .Cartesian(_directions, (s, d) => TryStepInDirection(node, s, d))
      .Where(n =>
      {
        if (n == null)
        {
          return false;
        }

        // Consider rotations and reflections.
        return !n.GetAllRotationAndReflectionIdentifiers().Any(_cache.Contains);
      })
      .Select(n => new Move(node, n));
  }

  private Board TryStepInDirection(Board currentBoard, int emptySpot, Direction direction)
  {
    // A situation like this: '_ x x' results in 'x _ _'. 
    int back;
    int jumper;
    var (_, x, y) = _indexedGrid.Single(g => g.value == emptySpot);

    switch (direction)
    {
      case Direction.Up:
        back = _grid[y - 1][x];
        jumper = _grid[y - 2][x];
        break;
      case Direction.Down:
        back = _grid[y + 1][x];
        jumper = _grid[y + 2][x];
        break;
      case Direction.Left:
        back = _grid[y][x + 1];
        jumper = _grid[y][x + 2];
        break;
      case Direction.Right:
        back = _grid[y][x - 1];
        jumper = _grid[y][x - 2];
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    }

    // Outside the field.
    if (back == -1 || jumper == -1)
    {
      return null;
    }

    // Not a valid move.
    if (!currentBoard.GetValue(back) || !currentBoard.GetValue(jumper))
    {
      return null;
    }
    
    var newBoard = currentBoard.Jump(emptySpot, back, jumper);

    return newBoard;
  }
  
  private void TimerCallBack(object o)
  {
    // var numberOfSolutions = !_graph.Vertices.Contains(_endBoard) ? 0 : _graph.AllPaths(_beginBoard, _endBoard).Count;
    var depth = _graphExpander.CurrentDepth;
    Console.WriteLine($"{DateTime.Now:h:mm:ss} - processed {_graph.Vertices.Count.ToString("N0")} items, ? solution(s), depth is {depth}.");
  }

  public void FullCompute()
  {
    // Register an timer to update the status to the console periodically.
    new Timer(TimerCallBack, null, 0, 1000);

    _graphExpander.ExpandSinks(_graph, _cache, GraphExpand);
  }

  public IReadOnlyCollection<LinkedList<Move>> GetAllSuccessFulPaths() => _graph.AllPaths(_beginBoard, _endBoard);

  public static void PrintPath(IReadOnlyCollection<Move> path)
  {
    // First position.
    Console.WriteLine(path.First().Source.ToString());

    // All the positions until the end.
    path
      .Select(p => p.Target)
      .ForEach(b => Console.WriteLine(b.ToString()));
  }
}