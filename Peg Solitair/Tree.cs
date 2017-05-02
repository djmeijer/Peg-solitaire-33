using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Peg_Solitair
{
  public class Tree
  {
    private ulong _itemsProcessed;
    private readonly Dictionary<long, Node> _cache;
    private int _maxDepth;
    private readonly long _winningBoardHashCode;
    private Timer _timer;
    private readonly List<Tuple<bool?, bool?>> _directions;

    /// <summary>
    ///   Create a new tree structure.
    /// </summary>
    /// <param name="boardStart">Board start can be any board configuration.</param>
    /// <param name="boardWin">The desired end state of the board.</param>
    public Tree(BitArray boardStart, BitArray boardWin)
    {
      _timer = new Timer(TimerCallback, null, 0, 5000);

      Root = new Node(boardStart, null);
      _winningBoardHashCode = boardWin.GetBoardHash();
      _cache = new Dictionary<long, Node> { { boardStart.GetBoardHash(), Root } };
      _maxDepth = 0;
      _directions = new List<Tuple<bool?, bool?>>
      {
        new Tuple<bool?, bool?>(null, true),
        new Tuple<bool?, bool?>(true, null),
        new Tuple<bool?, bool?>(null, false),
        new Tuple<bool?, bool?>(false, null)
      };
    }

    private void TimerCallback(object o)
    {
      if(_cache != null)
      {
        Console.WriteLine($"{DateTime.Now:h:mm:ss} - processed {_itemsProcessed} items, {_cache.Count} unique nodes with {Root.SolutionCount} solution(s).");
      }
    }

    public Node Root { get; }

    public void Grow(bool debug)
    {
      AddChildNodes(Root, 0, debug);
      _timer.Dispose();
    }

    private void AddChildNodes(Node node, int depth, bool debug)
    {
      node.IsProcessed = true;
      if(depth > _maxDepth)
      {
        _maxDepth = depth;

        Console.WriteLine($"Depth {_maxDepth} reached with {_cache.Count} searched nodes.");
      }

      var childNodes = new List<Node>();

      foreach(var emptySpot in GetEmptySpots(node))
      {
        foreach(var direction in _directions)
        {
          Node n = TryStepFromDirection(node, emptySpot, direction.Item1, direction.Item2);
          if(n != null)
          {
            childNodes.Add(n);
          }
        }
      }

      if(!childNodes.Any())
      {
        return;
      }

      node.ChildNodes = childNodes;

      if(debug)
      {
        Console.WriteLine($"Root with {node.ChildNodes.Count} child(ren).");
        node.Board.Print();
        Console.WriteLine("Boards");
        node.ChildNodes.ForEach(n => n.Board.Print());
        Console.ReadKey();
      }

      foreach(var n in node.ChildNodes.Where(n => !n.IsProcessed))
      {
        AddChildNodes(n, depth + 1, debug);
      }
      node.Cleanup();
    }

    private Node TryStepFromDirection(Node node, int emptySpot, bool? x, bool? y)
    {
      int back, jumper;

      if(x == true)
      {
        if(new List<int> { 1, 2, 4, 5, 11, 12, 18, 19, 25, 26, 28, 29, 31, 32 }.Contains(emptySpot))
        {
          return null;
        }
        back = emptySpot + 1;
        jumper = emptySpot + 2;
      }
      else if(x == false)
      {
        if(new List<int> { 0, 1, 3, 4, 6, 7, 13, 14, 20, 21, 27, 28, 30, 31 }.Contains(emptySpot))
        {
          return null;
        }
        back = emptySpot - 1;
        jumper = emptySpot - 2;
      }
      else
      {
        if(y == true)
        {
          if(new List<int> { 6, 13, 7, 14, 0, 3, 1, 4, 2, 5, 11, 18, 12, 19 }.Contains(emptySpot))
          {
            return null;
          }
          if(emptySpot >= 8 && emptySpot <= 10)
          {
            back = emptySpot - 5;
            jumper = emptySpot - 8;
          }
          else if(emptySpot >= 15 && emptySpot <= 17)
          {
            back = emptySpot - 7;
            jumper = emptySpot - 12;
          }
          else if(emptySpot >= 20 && emptySpot <= 26)
          {
            back = emptySpot - 7;
            jumper = emptySpot - 14;
          }
          else if(emptySpot >= 27 && emptySpot <= 29)
          {
            back = emptySpot - 5;
            jumper = emptySpot - 12;
          }
          else
          {
            back = emptySpot - 3;
            jumper = emptySpot - 8;
          }
        }
        else
        {
          if(new List<int> { 13, 20, 14, 21, 27, 30, 28, 31, 29, 32, 18, 25, 19, 26 }.Contains(emptySpot))
          {
            return null;
          }
          if(emptySpot >= 0 && emptySpot <= 2)
          {
            back = emptySpot + 3;
            jumper = emptySpot + 8;
          }
          else if(emptySpot >= 3 && emptySpot <= 5)
          {
            back = emptySpot + 5;
            jumper = emptySpot + 12;
          }
          else if(emptySpot >= 6 && emptySpot <= 12)
          {
            back = emptySpot + 7;
            jumper = emptySpot + 14;
          }
          else if(emptySpot >= 15 && emptySpot <= 17)
          {
            back = emptySpot + 7;
            jumper = emptySpot + 12;
          }
          else
          {
            back = emptySpot + 5;
            jumper = emptySpot + 8;
          }
        }
      }

      BitArray board = node.Board;

      if(!board[back] || !board[jumper])
      {
        return null;
      }

      _itemsProcessed++;

      BitArray newBoard = (BitArray)board.Clone();
      newBoard[emptySpot] = true;
      newBoard[back] = false;
      newBoard[jumper] = false;

      long boardHash = newBoard.GetBoardHash();
      Node value;

      // Consider rotations and reflections.
      if(_cache.TryGetValue(boardHash, out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      if(_cache.TryGetValue(newBoard.GetVerticalFlippedBoard().GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      BitArray rotate1 = newBoard.GetClockWiseRotatedBoard();
      if(_cache.TryGetValue(rotate1.GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      if(_cache.TryGetValue(rotate1.GetVerticalFlippedBoard().GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      BitArray rotate2 = rotate1.GetClockWiseRotatedBoard();
      if(_cache.TryGetValue(rotate2.GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      if(_cache.TryGetValue(rotate2.GetVerticalFlippedBoard().GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      BitArray rotate3 = rotate2.GetClockWiseRotatedBoard();
      if(_cache.TryGetValue(rotate3.GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }
      if(_cache.TryGetValue(rotate3.GetVerticalFlippedBoard().GetBoardHash(), out value))
      {
        if(value.IsWinningNode || value.SolutionCount > 0)
        {
          node.UpdateSolutionsCount();
        }
        return value;
      }

      var newNode = new Node(newBoard, node);
      _cache.Add(boardHash, newNode);
      UpdateWinningData(boardHash, newNode);

      return newNode;
    }

    private void UpdateWinningData(long boardHash, Node node)
    {
      if(boardHash == _winningBoardHashCode)
      {
        Console.WriteLine("Solution found.");
        node.IsWinningNode = true;
        node.UpdateSolutionsCount();
      }
    }

    private IEnumerable<int> GetEmptySpots(Node node)
    {
      BitArray board = node.Board;
      for(int n = 0; n < 33; n++)
      {
        if(!board[n])
        {
          yield return n;
        }
      }
    }
  }
}