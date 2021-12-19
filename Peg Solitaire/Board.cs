using System.Collections;
using System.Text;
using SharpGraph.Core;

namespace Peg_Solitair;

/// <summary>
///     A board is represented by an array of length 33.
///     [  ] [  ] [0 ] [1 ] [2 ] [  ] [  ]
///     [  ] [  ] [3 ] [4 ] [5 ] [  ] [  ]
///     [6 ] [7 ] [8 ] [9 ] [10] [11] [12]
///     [13] [14] [15] [16] [17] [18] [19]
///     [20] [21] [22] [23] [24] [25] [26]
///     [  ] [  ] [27] [28] [29] [  ] [  ]
///     [  ] [  ] [30] [31] [32] [  ] [  ]
/// </summary>
public class Board : Vertex
{
    private readonly BitArray _board;

    public Board(BitArray board) : base(ToIdentifier(board)) => _board = board;

    public bool Visited { get; set; }

    public bool GetValue(int position) => _board[position];

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"    {B(_board[0])} {B(_board[1])} {B(_board[2])}    \n");
        builder.Append($"    {B(_board[3])} {B(_board[4])} {B(_board[5])}    \n");
        builder.Append($"{B(_board[6])} {B(_board[7])} {B(_board[8])} {B(_board[9])} {B(_board[10])} {B(_board[11])} {B(_board[12])}\n");
        builder.Append($"{B(_board[13])} {B(_board[14])} {B(_board[15])} {B(_board[16])} {B(_board[17])} {B(_board[18])} {B(_board[19])}\n");
        builder.Append($"{B(_board[20])} {B(_board[21])} {B(_board[22])} {B(_board[23])} {B(_board[24])} {B(_board[25])} {B(_board[26])}\n");
        builder.Append($"    {B(_board[27])} {B(_board[28])} {B(_board[29])}    \n");
        builder.Append($"    {B(_board[30])} {B(_board[31])} {B(_board[32])}    \n");

        return builder.ToString();
    }

    public static string ToIdentifier(BitArray board)
    {
        var builder = new StringBuilder();
        builder.AppendJoin(string.Empty, Enumerable.Range(0, 33).Select(i => B(board[i])));
        return builder.ToString();
    }

    public bool Equals(Board other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return other.GetType() == GetType() &&
               string.Equals(Identifier, other.Identifier, StringComparison.OrdinalIgnoreCase) &&
               ToString().Equals(other.ToString());
    }

    public override bool Equals(object obj) => Equals(obj as Board);

    public override int GetHashCode() => ToString().GetHashCode();

    private static char B(bool value) => value ? '1' : '0';

    public IEnumerable<int> GetEmptySpots() =>
        Enumerable.Range(0, 32).Where(p => !GetValue(p));

    public Board Jump(int emptySpot, int back, int jumper)
    {
        var newBoard = (BitArray) _board.Clone();
        newBoard[emptySpot] = true;
        newBoard[back] = false;
        newBoard[jumper] = false;

        return new Board(newBoard);
    }

    public IReadOnlyCollection<string> GetAllRotationAndReflectionIdentifiers()
    {
        return new []
        {
            Identifier,
            Rotate1(),
            Rotate2(),
            Rotate3(),
            VerticalFlip()
        };

        string Rotate1()
        {
            var originalIdentifier = Identifier.ToCharArray();
            var newIdentifier = Identifier.ToCharArray();
            newIdentifier[0] = originalIdentifier[20];
            newIdentifier[1] = originalIdentifier[13];
            newIdentifier[2] = originalIdentifier[6];
            newIdentifier[3] = originalIdentifier[21];
            newIdentifier[4] = originalIdentifier[14];
            newIdentifier[5] = originalIdentifier[7];
            newIdentifier[6] = originalIdentifier[30];
            newIdentifier[7] = originalIdentifier[27];
            newIdentifier[8] = originalIdentifier[22];
            newIdentifier[9] = originalIdentifier[15];
            newIdentifier[10] = originalIdentifier[8];
            newIdentifier[11] = originalIdentifier[3];
            newIdentifier[12] = originalIdentifier[0];
            newIdentifier[13] = originalIdentifier[31];
            newIdentifier[14] = originalIdentifier[28];
            newIdentifier[15] = originalIdentifier[23];
            // newIdentifier[16] = originalIdentifier[16];
            newIdentifier[17] = originalIdentifier[9];
            newIdentifier[18] = originalIdentifier[4];
            newIdentifier[19] = originalIdentifier[1];
            newIdentifier[20] = originalIdentifier[32];
            newIdentifier[21] = originalIdentifier[29];
            newIdentifier[22] = originalIdentifier[24];
            newIdentifier[23] = originalIdentifier[17];
            newIdentifier[24] = originalIdentifier[10];
            newIdentifier[25] = originalIdentifier[5];
            newIdentifier[26] = originalIdentifier[2];
            newIdentifier[27] = originalIdentifier[25];
            newIdentifier[28] = originalIdentifier[18];
            newIdentifier[29] = originalIdentifier[11];
            newIdentifier[30] = originalIdentifier[26];
            newIdentifier[31] = originalIdentifier[19];
            newIdentifier[32] = originalIdentifier[12];
            return new string(newIdentifier);
        }

        string Rotate2() => new(Identifier.Reverse().ToArray());

        string Rotate3()
        {
            var originalIdentifier = Identifier.ToCharArray();
            var newIdentifier = Identifier.ToCharArray();
            newIdentifier[0] = originalIdentifier[12];
            newIdentifier[1] = originalIdentifier[19];
            newIdentifier[2] = originalIdentifier[26];
            newIdentifier[3] = originalIdentifier[11];
            newIdentifier[4] = originalIdentifier[18];
            newIdentifier[5] = originalIdentifier[25];
            newIdentifier[6] = originalIdentifier[2];
            newIdentifier[7] = originalIdentifier[5];
            newIdentifier[8] = originalIdentifier[10];
            newIdentifier[9] = originalIdentifier[17];
            newIdentifier[10] = originalIdentifier[24];
            newIdentifier[11] = originalIdentifier[29];
            newIdentifier[12] = originalIdentifier[32];
            newIdentifier[13] = originalIdentifier[1];
            newIdentifier[14] = originalIdentifier[4];
            newIdentifier[15] = originalIdentifier[9];
            // newIdentifier[16] = originalIdentifier[16];
            newIdentifier[17] = originalIdentifier[23];
            newIdentifier[18] = originalIdentifier[28];
            newIdentifier[19] = originalIdentifier[31];
            newIdentifier[20] = originalIdentifier[0];
            newIdentifier[21] = originalIdentifier[3];
            newIdentifier[22] = originalIdentifier[8];
            newIdentifier[23] = originalIdentifier[15];
            newIdentifier[24] = originalIdentifier[22];
            newIdentifier[25] = originalIdentifier[27];
            newIdentifier[26] = originalIdentifier[30];
            newIdentifier[27] = originalIdentifier[7];
            newIdentifier[28] = originalIdentifier[14];
            newIdentifier[29] = originalIdentifier[21];
            newIdentifier[30] = originalIdentifier[6];
            newIdentifier[31] = originalIdentifier[13];
            newIdentifier[32] = originalIdentifier[20];
            return new string(newIdentifier);
        }

        string VerticalFlip()
        {
            var originalIdentifier = Identifier.ToCharArray();
            var newIdentifier = Identifier.ToCharArray();
            newIdentifier[0] = originalIdentifier[2];
            newIdentifier[1] = originalIdentifier[1];
            newIdentifier[2] = originalIdentifier[0];
            newIdentifier[3] = originalIdentifier[5];
            newIdentifier[4] = originalIdentifier[4];
            newIdentifier[5] = originalIdentifier[3];
            newIdentifier[6] = originalIdentifier[12];
            newIdentifier[7] = originalIdentifier[11];
            newIdentifier[8] = originalIdentifier[10];
            newIdentifier[9] = originalIdentifier[9];
            newIdentifier[10] = originalIdentifier[8];
            newIdentifier[11] = originalIdentifier[7];
            newIdentifier[12] = originalIdentifier[6];
            newIdentifier[13] = originalIdentifier[19];
            newIdentifier[14] = originalIdentifier[18];
            newIdentifier[15] = originalIdentifier[17];
            newIdentifier[16] = originalIdentifier[16];
            newIdentifier[17] = originalIdentifier[15];
            newIdentifier[18] = originalIdentifier[14];
            newIdentifier[19] = originalIdentifier[13];
            newIdentifier[20] = originalIdentifier[26];
            newIdentifier[21] = originalIdentifier[25];
            newIdentifier[22] = originalIdentifier[24];
            newIdentifier[23] = originalIdentifier[23];
            newIdentifier[24] = originalIdentifier[22];
            newIdentifier[25] = originalIdentifier[21];
            newIdentifier[26] = originalIdentifier[20];
            newIdentifier[27] = originalIdentifier[29];
            newIdentifier[28] = originalIdentifier[28];
            newIdentifier[29] = originalIdentifier[27];
            newIdentifier[30] = originalIdentifier[32];
            newIdentifier[31] = originalIdentifier[31];
            newIdentifier[32] = originalIdentifier[30];

            return new string(newIdentifier);
        }
    }
}