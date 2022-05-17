using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01
{
    class Player
    {
        public int StartY { get; private set; }
        public int StartX { get; private set; }

        public Player(int startY, int startX)
        {
            StartY = startY;
            StartX = startX;
        }
    }
}
