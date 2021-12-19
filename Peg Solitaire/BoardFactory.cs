using System.Collections;

namespace Peg_Solitair;

public static class BoardFactory
{
  public static Board GetStartBoard()
  {
    var board = GetBitArray(true);
    board[16] = false;
    return new Board(board);
  }

  public static Board GetEndBoard()
  {
    var board = GetBitArray(false);
    board[16] = true;
    return new Board(board);
  }

  public static Board GetHatShape()
  {
    var board = GetBitArray(false);
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
    return new Board(board);
  }

  public static Board GetTrivialBoard()
  {
    var board = GetBitArray(false);
    board[4] = true;
    board[16] = true;
    board[28] = true;
    board[31] = true;
    return new Board(board);
  }

  private static BitArray GetBitArray(bool initialValue) => new(Enumerable.Repeat(initialValue, 33).ToArray());
}