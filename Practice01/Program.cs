using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice01
{
    class Program
    {
        static void Main(string[] args)
        {
            // 입력

            // 로직
            Player player = new Player(1, 1, 23, 23);
            Board board = new Board(25, player);
            player.SetBoard(board);
            // 렌더링
            Console.CursorVisible = false;

            const int MAX_TIME = 1000 / 30;
            int lastTick = System.Environment.TickCount;
            while (true)
            {
                Console.SetCursorPosition(0, 0);

                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < MAX_TIME)
                    continue;
                lastTick = currentTick;

                board.Render();
                if (player.PosY == 23 && player.PosX == 23)
                    return;
                player.Move();
            }
            
        }
    }
}
