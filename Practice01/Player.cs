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
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        public int EndY { get; private set; }
        public int EndX { get; private set; }
        private int _dir = (int)Direction.Up;
        private int[] _frontY = new int[] { -1, 0, 1, 0 };
        private int[] _frontX = new int[] { 0, -1, 0, 1 };
        private int[] _rightY = new int[] { 0, -1, 0, 1 };
        private int[] _rightX = new int[] { 1, 0, -1, 0 };

        private Board _board;

        // 오른쪽 -> 왼쪽 (반시계방향)
        enum Direction
        {
            Up,
            Left,
            Down,
            Right
        }

        public Player(int startY, int startX, int endY, int endX)
        {
            StartY = startY;
            StartX = startX;

            PosY = StartY;
            PosX = StartX;

            EndY = endY;
            EndX = endX;
        }

        public void SetBoard(Board board)
        {
            _board = board;
        }

        // 오른손 법칙
        public void Move()
        {
            Board.MazeType[,] maze = _board.Maze;


            // 1. 오른쪽이 비어있으면 그 쪽으로 간다.
            // 2. 오른쪽이 벽으로 막혀있다면 직선으로 간다.
            // 3. 그렇지 않다면 왼쪽으로 회전한다.
            if (maze[PosY + _rightY[_dir], PosX + _rightX[_dir]] == Board.MazeType.Empty)
            {
                _dir = (_dir - 1 + 4) % 4;
                PosY = PosY + _frontY[_dir];
                PosX = PosX + _frontX[_dir];
            }
            else if (maze[PosY + _frontY[_dir], PosX + _frontX[_dir]] == Board.MazeType.Empty)
            {
                PosY = PosY + _frontY[_dir];
                PosX = PosX + _frontX[_dir];
            }
            else
                _dir = (_dir + 1) % 4;
        }
    }
}
