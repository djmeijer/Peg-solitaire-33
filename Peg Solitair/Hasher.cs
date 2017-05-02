using System;
using System.Collections;

namespace Peg_Solitair
{
  public static class Hasher
  {
    public static long GetBoardHash(this BitArray board)
    {
      var value = new BitArray(64) { [0] = true };
      for(int i = 0; i < 33; i++)
      {
        value[i + 1] = board[i];
      }

      var array = new byte[8];
      value.CopyTo(array, 0);
      return BitConverter.ToInt64(array, 0);
    }

    public static BitArray GetClockWiseRotatedBoard(this BitArray board)
    {
      var ret = (BitArray)board.Clone();
      ret[0] = board[20];
      ret[1] = board[13];
      ret[2] = board[6];

      ret[3] = board[21];
      ret[4] = board[14];
      ret[5] = board[7];

      ret[6] = board[30];
      ret[7] = board[27];
      ret[8] = board[22];
      ret[9] = board[15];
      ret[10] = board[8];
      ret[11] = board[3];
      ret[12] = board[0];

      ret[13] = board[31];
      ret[14] = board[28];
      ret[15] = board[23];
      ret[16] = board[16];
      ret[17] = board[9];
      ret[18] = board[4];
      ret[19] = board[1];

      ret[20] = board[32];
      ret[21] = board[29];
      ret[22] = board[24];
      ret[23] = board[17];
      ret[24] = board[10];
      ret[25] = board[5];
      ret[26] = board[2];

      ret[27] = board[25];
      ret[28] = board[18];
      ret[29] = board[11];

      ret[30] = board[26];
      ret[31] = board[19];
      ret[32] = board[12];

      return ret;
    }

    public static BitArray GetVerticalFlippedBoard(this BitArray board)
    {
      var ret = (BitArray)board.Clone();
      ret[0] = board[2];
      ret[1] = board[1];
      ret[2] = board[0];

      ret[3] = board[5];
      ret[4] = board[4];
      ret[5] = board[3];

      ret[6] = board[12];
      ret[7] = board[11];
      ret[8] = board[10];
      ret[9] = board[9];
      ret[10] = board[8];
      ret[11] = board[7];
      ret[12] = board[6];

      ret[13] = board[19];
      ret[14] = board[18];
      ret[15] = board[17];
      ret[16] = board[16];
      ret[17] = board[15];
      ret[18] = board[14];
      ret[19] = board[13];

      ret[20] = board[26];
      ret[21] = board[25];
      ret[22] = board[24];
      ret[23] = board[23];
      ret[24] = board[22];
      ret[25] = board[21];
      ret[26] = board[20];

      ret[27] = board[29];
      ret[28] = board[28];
      ret[29] = board[27];

      ret[30] = board[32];
      ret[31] = board[31];
      ret[32] = board[30];

      return ret;
    }
  }
}