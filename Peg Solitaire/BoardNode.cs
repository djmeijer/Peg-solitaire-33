using System.Collections;
using System.Text;
using SharpGraph.Core;

namespace Peg_Solitair;
/// <summary>
///   A board is represented by an array of length 33.
///   [  ] [  ] [0 ] [1 ] [2 ] [  ] [  ]
///   [  ] [  ] [3 ] [4 ] [5 ] [  ] [  ]
///   [6 ] [7 ] [8 ] [9 ] [10] [11] [12]
///   [13] [14] [15] [16] [17] [18] [19]
///   [20] [21] [22] [23] [24] [25] [26]
///   [  ] [  ] [27] [28] [29] [  ] [  ]
///   [  ] [  ] [30] [31] [32] [  ] [  ]
/// </summary>
public class BoardNode : Vertex
{
    private readonly BitArray _board;

    public BoardNode(BitArray board)
        : base(GetShortIdentifier(board))
    {
        _board = board;
    }

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

    public static string GetShortIdentifier(BitArray board)
    {
        var identifier = new StringBuilder(33);
        foreach (bool o in board)
        {
            identifier.Append(o);
        }

        return identifier.ToString();
    }
    
    public BoardNode CloneToClockWiseRotated(int? count = null)
    {
      var newBoard = (BitArray)_board.Clone();
      newBoard[0] = _board[20];
      newBoard[1] = _board[13];
      newBoard[2] = _board[6];
      newBoard[3] = _board[21];
      newBoard[4] = _board[14];
      newBoard[5] = _board[7];
      newBoard[6] = _board[30];
      newBoard[7] = _board[27];
      newBoard[8] = _board[22];
      newBoard[9] = _board[15];
      newBoard[10] = _board[8];
      newBoard[11] = _board[3];
      newBoard[12] = _board[0];
      newBoard[13] = _board[31];
      newBoard[14] = _board[28];
      newBoard[15] = _board[23];
      newBoard[16] = _board[16];
      newBoard[17] = _board[9];
      newBoard[18] = _board[4];
      newBoard[19] = _board[1];
      newBoard[20] = _board[32];
      newBoard[21] = _board[29];
      newBoard[22] = _board[24];
      newBoard[23] = _board[17];
      newBoard[24] = _board[10];
      newBoard[25] = _board[5];
      newBoard[26] = _board[2];
      newBoard[27] = _board[25];
      newBoard[28] = _board[18];
      newBoard[29] = _board[11];
      newBoard[30] = _board[26];
      newBoard[31] = _board[19];
      newBoard[32] = _board[12];

      var newBoardNode = new BoardNode(newBoard);

      return count is > 0 ? newBoardNode.CloneToClockWiseRotated(count - 1) : newBoardNode;
    }

    public BoardNode CloneToVerticalFlipped()
    {
      var newBoard = (BitArray)_board.Clone();
      newBoard[0] = _board[2];
      newBoard[1] = _board[1];
      newBoard[2] = _board[0];
      newBoard[3] = _board[5];
      newBoard[4] = _board[4];
      newBoard[5] = _board[3];
      newBoard[6] = _board[12];
      newBoard[7] = _board[11];
      newBoard[8] = _board[10];
      newBoard[9] = _board[9];
      newBoard[10] = _board[8];
      newBoard[11] = _board[7];
      newBoard[12] = _board[6];
      newBoard[13] = _board[19];
      newBoard[14] = _board[18];
      newBoard[15] = _board[17];
      newBoard[16] = _board[16];
      newBoard[17] = _board[15];
      newBoard[18] = _board[14];
      newBoard[19] = _board[13];
      newBoard[20] = _board[26];
      newBoard[21] = _board[25];
      newBoard[22] = _board[24];
      newBoard[23] = _board[23];
      newBoard[24] = _board[22];
      newBoard[25] = _board[21];
      newBoard[26] = _board[20];
      newBoard[27] = _board[29];
      newBoard[28] = _board[28];
      newBoard[29] = _board[27];
      newBoard[30] = _board[32];
      newBoard[31] = _board[31];
      newBoard[32] = _board[30];

      return new BoardNode(newBoard);
    }

    private static char B(bool value) => value ? '1' : '0';

    public IEnumerable<int> GetEmptySpots() =>
        Enumerable.Range(0, 32).Where(p => !GetValue(p));

    public BoardNode Jump(int emptySpot, int back, int jumper)
    {
        var newBoard = (BitArray)_board.Clone();
        newBoard[emptySpot] = true;
        newBoard[back] = false;
        newBoard[jumper] = false;

        return new BoardNode(newBoard);
    }
    
    public bool Visited { get; set; }
}