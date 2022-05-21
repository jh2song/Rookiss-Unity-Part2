using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01
{
    class Board
    {
        public enum MazeType
        {
            Empty,
            Wall
        }

        enum Direction
        {
            Right,
            Down
        }

        const char SYMBOL = '\u25A0';
        
        private Player _player;
        public int Size { get; private set; } // 가로나 세로의 사이즈
        public MazeType[,] Maze;

        public Board(int size, Player player)
        {
            Size = size;
            _player = player;

            Maze = new MazeType[size, size];

            // 미로 생성
            SetSideWinder();
        }

        void SetSideWinder()
        {
            // SideWinder 미로 생성 알고리즘에서는 사이즈가 홀수여야만 한다.
            if (Size % 2 == 0)
                throw new Exception();
            
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    // y, x 둘 중 하나가 짝수면 벽, 아니면 빈 공간
                    if (y % 2 == 0 || x % 2 == 0)
                    {
                        Maze[y, x] = MazeType.Wall;
                    }
                    else
                    {
                        Maze[y, x] = MazeType.Empty;
                    }
                }
            }

            Random rand = new Random();

            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (y % 2 == 0 || x % 2 == 0) // 벽 예외처리
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Maze[y, x + 1] = MazeType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Maze[y + 1, x] = MazeType.Empty;
                        continue;
                    }

                    Direction dir = (Direction)rand.Next(0, 2);
                    switch (dir)
                    {
                        case Direction.Down:
                            int offset = rand.Next(0, count);
                            Maze[y + 1, x - offset * 2] = MazeType.Empty;
                            count = 1;
                            break;

                        case Direction.Right:
                            Maze[y, x + 1] = MazeType.Empty;
                            count++;
                            break;
                    }
                }
            }

        }

        const int MOVE_TICK = 30;
        static int _sumTick = 0;
        int idx = 0;

        public void Render(int deltaTick)
        {
            if (idx >= _player.GetPoints().Count)
			{
                // 초기화
                _player = new Player(1, 1, 23, 23);
                Board _board = new Board(25, _player);
                _player.SetBoard(_board);
                SetSideWinder();
                _player.AStar();
                idx = 0;
                return;
			}

            _sumTick += deltaTick;
            if (deltaTick < _sumTick)
                return;
            _sumTick = 0;

            ConsoleColor prev = Console.ForegroundColor;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (y == _player.GetPoints()[idx].Y && x == _player.GetPoints()[idx].X)
					{
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(SYMBOL);
                        continue;
					}

                    if (y == _player.EndY && x == _player.EndX)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(SYMBOL);
                        continue;
                    }

                    switch (Maze[y, x])
                    {
                        case MazeType.Empty:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(SYMBOL);
                            break;
                        case MazeType.Wall:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(SYMBOL);
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = prev;
            idx++;
        }
    }
}
