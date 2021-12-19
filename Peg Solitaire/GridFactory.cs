namespace Peg_Solitair;

public static class GridFactory
{
    public static int[][] GetGrid() =>
        new[]
        {
            new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new []{ -1, -1, -1, -1, 0, 1, 2, -1, -1, -1, -1 },
            new []{ -1, -1, -1, -1, 3, 4, 5, -1, -1, -1, -1 },
            new []{ -1, -1, 6, 7, 8, 9, 10, 11, 12, -1, -1 },
            new []{ -1, -1, 13, 14, 15, 16, 17, 18, 19, -1, -1 },
            new []{ -1, -1, 20, 21, 22, 23, 24, 25, 26, -1, -1 },
            new []{ -1, -1, -1, -1, 27, 28, 29, -1, -1, -1, -1 },
            new []{ -1, -1, -1, -1, 30, 31, 32, -1, -1, -1, -1 },
            new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            new []{ -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
        };

    public static IReadOnlyCollection<(int value, int x, int y)> GetIndexedGrid() => GetGrid()
        .SelectMany((row, y) => row.Select((value, x) => (value, x, y)))
        .ToArray();
}