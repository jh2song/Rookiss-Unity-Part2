using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01
{
    class Pos
	{
        public int Y { get; set; }
        public int X { get; set; }

        public Pos(int y, int x)
		{
            Y = y;
            X = x;
		}
	}

    struct PQNode : IComparable<PQNode>
	{
        public int F { get; set; }
        public int G { get; set; }
        public int Y { get; set; }
        public int X { get; set; }

		public int CompareTo(PQNode other)
		{
            if (F == other.F)
                return 0;
            else
                return F > other.F ? -1 : 1;
		}
	}

    class Player
    {
        public int StartY { get; private set; }
        public int StartX { get; private set; }
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        public int EndY { get; private set; }
        public int EndX { get; private set; }
        // private int _dir = (int)Direction.U;
        private int[] _deltaY = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
        private int[] _deltaX = new int[] { 0, -1, -1, -1, 0, 1, 1, 1 };
        private int[] _cost = new int[] { 10, 14, 10, 14, 10, 14, 10, 14 };
        private Board _board;

        //enum Direction
        //{
        //    U,
        //    UL,
        //    L,
        //    DL,
        //    D,
        //    DR,
        //    R,
        //    UR
        //}

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

        // A* 알고리즘
        // F = G + H
        // G: 가중치
        // H: 휴리스틱
        public void AStar()
		{
            PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();
            bool[,] closed = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];
            int[,] open = new int[_board.Size, _board.Size];
            for (int i = 0; i < _board.Size; i++)
                for (int j = 0; j < _board.Size; j++)
                    open[i, j] = Int32.MaxValue;

            pq.Push(new PQNode { F = Math.Abs(EndY - StartY) + Math.Abs(EndX - StartX), G = 0, Y = StartY, X = StartX });
            parent[StartY, StartX] = new Pos(StartY, StartX);
            open[StartY, StartX] = 0;

            while (pq.Count > 0)
			{
                PQNode cur = pq.Pop();

                if (closed[cur.Y, cur.X])
                    continue;
                closed[cur.Y, cur.X] = true;

                for (int i = 0; i < _deltaY.Length; i++)
				{
                    int nextY = cur.Y + _deltaY[i];
                    int nextX = cur.X + _deltaX[i];

                    if (nextY < 0 || nextY >= _board.Size || nextX < 0 || nextX >= _board.Size)
                        continue;
                    if (_board.Maze[nextY, nextX] == Board.MazeType.Wall)
                        continue;
                    if (closed[nextY, nextX])
                        continue;

                    int g = cur.G + _cost[i];
                    int h = Math.Abs(EndY - nextY) + Math.Abs(EndX - nextX);

                    if (open[nextY, nextX] < g + h)
                        continue;

                    open[nextY, nextX] = g + h;
                    parent[nextY, nextX] = new Pos(cur.Y, cur.X);
                    pq.Push(new PQNode { F = g + h, G = g, Y = nextY, X = nextX });
                }
			}

            BackFromEnd(parent);
		}

        private List<Pos> Points = new List<Pos>();
        public void BackFromEnd(Pos[,] parent)
		{
            int y = EndY;
            int x = EndX;
            while (parent[y, x].Y != StartY || parent[y, x].X != StartX)
			{
                Points.Add(new Pos(y, x));
                int ty = parent[y, x].Y;
                int tx = parent[y, x].X;
                y = ty;
                x = tx;
			}
            Points.Add(new Pos(y, x));

            Points.Reverse();
		}

        public List<Pos> GetPoints() { return Points; }

        // 오른손 법칙
        //public void Move()
        //{
        //    Board.MazeType[,] maze = _board.Maze;


        //    // 1. 오른쪽이 비어있으면 그 쪽으로 간다.
        //    // 2. 오른쪽이 벽으로 막혀있다면 직선으로 간다.
        //    // 3. 그렇지 않다면 왼쪽으로 회전한다.
        //    if (maze[PosY + _rightY[_dir], PosX + _rightX[_dir]] == Board.MazeType.Empty)
        //    {
        //        _dir = (_dir - 1 + 4) % 4;
        //        PosY = PosY + _frontY[_dir];
        //        PosX = PosX + _frontX[_dir];
        //    }
        //    else if (maze[PosY + _frontY[_dir], PosX + _frontX[_dir]] == Board.MazeType.Empty)
        //    {
        //        PosY = PosY + _frontY[_dir];
        //        PosX = PosX + _frontX[_dir];
        //    }
        //    else
        //        _dir = (_dir + 1) % 4;
        //}
    }
}
