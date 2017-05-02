using System.Collections;
using System.Linq;

namespace Peg_Solitair
{
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
  public static class Factory
  {
    public static BitArray GetDefaultBeginBoard()
    {
      BitArray board = GetBitArray(true);
      board[16] = false;
      return board;
    }

    public static BitArray GetDefaultEndBoard()
    {
      BitArray board = GetBitArray(false);
      board[16] = true;
      return board;
    }

    public static BitArray GetHatShape()
    {
      BitArray board = GetBitArray(false);
      board[4] = true;
      board[8] = true;
      board[9] = true;
      board[10] = true;
      board[14] = true;
      board[15] = true;
      board[16] = true;
      board[17] = true;
      board[18] = true;
      board[22] = true;
      board[24] = true;
      return board;
    }

    public static BitArray GetTrivialBoard()
    {
      BitArray board = GetBitArray(false);
      board[4] = true;
      board[16] = true;
      board[28] = true;
      board[31] = true;
      return board;
    }

    private static BitArray GetBitArray(bool initializeValue)
    {
      return new BitArray(Enumerable.Repeat(initializeValue, 33).ToArray());
    }
  }
}