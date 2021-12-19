using MoreLinq;
using SharpGraph.Core;

namespace Peg_Solitair;

public class Game
{
  private readonly int[][] _grid;
  private readonly IReadOnlyCollection<(int value, int x, int y)> _indexedGrid;

  public Game()
  {
    _grid = new[]
    {
      new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
      new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
      new []{ -1, -1, -1, -1, 0, 1, 2, -1, -1, -1, -1 },
      new []{ -1, -1, -1, -1, 3, 4, 5, -1, -1, -1, -1 },
      new []{ -1, -1, 6, 7, 8, 9, 10, 11, 12, -1, -1 },
      new []{ -1, -1, 13, 14, 15, 16, 17, 18, 19, -1, -1 },
      new []{ -1, -1, 20, 21, 22, 23, 24, 25, 26, -1, -1 },
      new []{ -1, -1, -1, -1, 27, 28, 29, -1, -1, -1, -1 },
      new []{ -1, -1, -1, -1, 30, 31, 32, -1, -1, -1, -1 },
      new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
      new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
    };
    _indexedGrid = _grid.SelectMany((row, y) => row.Select((value, x) => (value, x, y))).ToArray();
  }
  public IEnumerable<Move> GraphExpand(Graph<BoardNode, Move> graph, BoardNode node)
  {
    node.Visited = true;
    var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToArray();
    var emptySpots = node.GetEmptySpots();

    return emptySpots
      .Cartesian( directions, (s, d) => TryStepFromDirection(node, s, d))
      .Where(n =>
      {
        if (n == null)
        {
          return false;
        }

        // Consider rotations and reflections.
        if (graph.Vertices.Contains(n))
        {
          return false;
        }
        var rotate1 = n.CloneToClockWiseRotated(1);
        if (graph.Vertices.Contains(rotate1))
        {
          return false;
        }
        var rotate2 = rotate1.CloneToClockWiseRotated(1);
        if (graph.Vertices.Contains(rotate2))
        {
          return false;
        }
        if (graph.Vertices.Contains(rotate2.CloneToClockWiseRotated(1)))
        {
          return false;
        }
        var flipped = n.CloneToVerticalFlipped();
        if (graph.Vertices.Contains(flipped))
        {
          return false;
        }
        var flippedRotate1 = flipped.CloneToClockWiseRotated(1);
        if (graph.Vertices.Contains(flippedRotate1))
        {
          return false;
        }
        var flippedRotate2 = flippedRotate1.CloneToClockWiseRotated(1);
        if (graph.Vertices.Contains(flippedRotate2))
        {
          return false;
        }

        return !graph.Vertices.Contains(flippedRotate2.CloneToClockWiseRotated(1));
      })
      .Select(n => new Move(node, n));
  }

  private BoardNode TryStepFromDirection(BoardNode currentBoard, int emptySpot, Direction direction)
  {
    // A situation like this: '_ x x' results in 'x _ _'. 
    int back;
    int jumper;
    var index = _indexedGrid.Single(g => g.value == emptySpot);

    switch (direction)
    {
      case Direction.Up:
        back = _grid[index.y - 1][index.x];
        jumper = _grid[index.y - 2][index.x];
        break;
      case Direction.Down:
        back = _grid[index.y + 1][index.x];
        jumper = _grid[index.y + 2][index.x];
        break;
      case Direction.Left:
        back = _grid[index.y][index.x + 1];
        jumper = _grid[index.y][index.x + 2];
        break;
      case Direction.Right:
        back = _grid[index.y][index.x - 1];
        jumper = _grid[index.y][index.x - 2];
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
}