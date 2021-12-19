using System.Globalization;

namespace Peg_Solitair;

public static class Program
{
  static void Main()
  {
    // Set culture for formatting number with thousand separator.
    CultureInfo.DefaultThreadCurrentCulture = new("nl-NL");

    // Initialize a game with start and end position.
    var startBoard = BoardFactory.GetStartBoard();
    var endBoard = BoardFactory.GetEndBoard();
    var game = new Game(startBoard, endBoard);

    // This starts the heavy lifting.
    game.FullCompute();

    // Get all the successful paths.
    var allPaths = game.GetAllSuccessFulPaths();

    // Show all the results and a successful path.
    Console.WriteLine("Game space is exhaustively searched.");
    Console.WriteLine($"There are {allPaths.Count} possible ways to solve the problem.");
    Console.WriteLine("A possible solution is as follows.");
    Game.PrintPath(allPaths.First());

    Console.ReadKey();
  }
}