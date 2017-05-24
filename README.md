# Peg solitaire (a.k.a. Hi-Q)

A C# implementation for solving the English version of this game with 32 pegs and 33 holes.

```C#
[  ] [  ] [0 ] [1 ] [2 ] [  ] [  ]
[  ] [  ] [3 ] [4 ] [5 ] [  ] [  ]
[6 ] [7 ] [8 ] [9 ] [10] [11] [12]
[13] [14] [15] [16] [17] [18] [19]
[20] [21] [22] [23] [24] [25] [26]
[  ] [  ] [27] [28] [29] [  ] [  ]
[  ] [  ] [30] [31] [32] [  ] [  ]
```

It searches (DFS) the complete solution tree (185.700.000+ nodes) taking rotation/reflection variations into account.

The running time is ± 15 minutes
- has been compiled using VS2017, release mode x64
- claims up to 5 GB of RAM
- uses a 2,3 GHz Intel Core i7 (1 core effectively)

The main cause of the RAM usage is the use of a non-optimized recursive function which creates a giant stack.
