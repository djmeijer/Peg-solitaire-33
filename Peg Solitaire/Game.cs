using System;
using System.Collections;
using System.Linq;

namespace Peg_Solitair
{
  public class Game
  {
    public void Solve(bool debug)
    {
      BitArray boardStart = Factory.GetDefaultBeginBoard();
      BitArray boardWin = Factory.GetDefaultEndBoard();

      var tree = new Tree(boardStart, boardWin);
      tree.Grow(debug);

      Console.WriteLine("Tree is exhaustively searched.");

      if(tree.Root != null)
      {
        Console.WriteLine($"There are/is {tree.Root.SolutionCount} possible way(s) to solve the problem.");
        Console.WriteLine("A possible solution is as follows.");
        PrintWinningPath(tree.Root);
      }
      else
      {
        Console.WriteLine("No solution found.");
      }
      Console.ReadKey();
    }

    private void PrintWinningPath(Node node)
    {
      node.Board.Print();
      if(node.IsWinningNode)
      {
        return;
      }

      Console.ReadKey();
      PrintWinningPath(node.ChildNodes.FirstOrDefault(n => n.Parent == node));
    }
  }
}