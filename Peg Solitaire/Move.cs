using SharpGraph.Core;

namespace Peg_Solitair;

public class Move : Arrow<Board>
{
    public Move(Board source, Board target)
        : base(source, target, "move")
    {
    }
}