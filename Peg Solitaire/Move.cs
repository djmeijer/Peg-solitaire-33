using SharpGraph.Core;

namespace Peg_Solitair;

public class Move : Arrow<BoardNode>
{
    public Move(BoardNode source, BoardNode target)
        : base(source, target)
    {
    }
}