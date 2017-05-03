# Peg solitaire (a.k.a. Hi-Q)

A C# implementation for solving the English version of this game with 32 pegs and 33 holes.

[  ] [  ] [0 ] [1 ] [2 ] [  ] [  ] 

[  ] [  ] [3 ] [4 ] [5 ] [  ] [  ] 
[6 ] [7 ] [8 ] [9 ] [10] [11] [12] 
[13] [14] [15] [16] [17] [18] [19] 
[20] [21] [22] [23] [24] [25] [26] 
[  ] [  ] [27] [28] [29] [  ] [  ] 
[  ] [  ] [30] [31] [32] [  ] [  ] 

It searches (DFS) the complete solution tree (180.000.000+ nodes) taking rotation/reflection variations into account.

Running time is ± 15 minutes using ± 5 GB of RAM on recent hardware (Intel Core i7).
