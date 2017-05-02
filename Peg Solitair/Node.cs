using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Peg_Solitair
{
  public class Node
  {
    public Node(BitArray board, Node parent)
    {
      Board = board;
      Parent = parent;
    }

    public BitArray Board { get; }

    public Node Parent { get; }

    public List<Node> ChildNodes { get; set; }

    public bool IsWinningNode { get; set; }

    public void Cleanup() => ChildNodes.RemoveAll(n => (n.ChildNodes == null || !n.ChildNodes.Any()) && !n.IsWinningNode);

    public bool IsProcessed { get; set; }

    public int SolutionCount { get; set; }

    public void UpdateSolutionsCount()
    {
      if(Parent != null)
      {
        Parent.SolutionCount++;
        Parent.UpdateSolutionsCount();
      }
    }
  }
}