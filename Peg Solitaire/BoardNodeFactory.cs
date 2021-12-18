using System.Collections;

namespace Peg_Solitair;

public static class BoardNodeFactory
{
  public static BoardNode GetDefaultBeginBoard()
  {
    var board = GetBitArray(true);
    board[16] = false;
    return new BoardNode(board);
  }

  public static BoardNode GetDefaultEndBoard()
  {
    var board = GetBitArray(false);
    board[16] = true;
    return new BoardNode(board);
  }

  public static BoardNode GetHatShape()
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
    return new BoardNode(board);
  }

  public static BoardNode GetTrivialBoard()
  {
    var board = GetBitArray(false);
    board[4] = true;
    board[16] = true;
    board[28] = true;
    board[31] = true;
    return new BoardNode(board);
  }

  private static BitArray GetBitArray(bool initialValue) => new(Enumerable.Repeat(initialValue, 33).ToArray());
}