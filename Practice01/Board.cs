using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01
{
    class Board
    {
        enum MazeType
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
        
        private Player Player;
        public int Size { get; private set; } // 가로나 세로의 사이즈
        private MazeType[,] _board;


        public Board(int size, Player player)
        {
            Size = size;
            Player = player;

            _board = new MazeType[size, size];

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
                        _board[y, x] = MazeType.Wall;
                    }
                    else
                    {
                        _board[y, x] = MazeType.Empty;
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
                        _board[y, x + 1] = MazeType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        _board[y + 1, x] = MazeType.Empty;
                        continue;
                    }

                    Direction dir = (Direction)rand.Next(0, 2);
                    switch (dir)
                    {
                        case Direction.Down:
                            int offset = rand.Next(0, count);
                            _board[y + 1, x - offset * 2] = MazeType.Empty;
                            count = 1;
                            break;

                        case Direction.Right:
                            _board[y, x + 1] = MazeType.Empty;
                            count++;
                            break;
                    }
                }
            }

        }

        public void Render()
        {
            ConsoleColor prev = Console.ForegroundColor;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    switch (_board[y, x])
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
        }
    }
}
