using System;

namespace Minesweeper
{
    public struct Tile 
    {
        public string symbol;
        public bool sweeped;
        public bool hasSweepedNeighbours;
        public bool sweepedByNeighbour;
        public bool flagged;
        public bool mine;
        public int neighbouringMines;
    }
}