using System;
using System.Collections;

namespace Peg_Solitair
{
  public static class Printer
  {
    public static void Print(this BitArray board)
    {
      var printValue = $"    {B(board[0])} {B(board[1])} {B(board[2])}    \n";
      printValue += $"    {B(board[3])} {B(board[4])} {B(board[5])}    \n";
      printValue += $"{B(board[6])} {B(board[7])} {B(board[8])} {B(board[9])} {B(board[10])} {B(board[11])} {B(board[12])}\n";
      printValue += $"{B(board[13])} {B(board[14])} {B(board[15])} {B(board[16])} {B(board[17])} {B(board[18])} {B(board[19])}\n";
      printValue += $"{B(board[20])} {B(board[21])} {B(board[22])} {B(board[23])} {B(board[24])} {B(board[25])} {B(board[26])}\n";
      printValue += $"    {B(board[27])} {B(board[28])} {B(board[29])}    \n";
      printValue += $"    {B(board[30])} {B(board[31])} {B(board[32])}    \n";

      Console.WriteLine(printValue);
    }

    private static char B(bool value) => value ? '1' : '0';
  }
}