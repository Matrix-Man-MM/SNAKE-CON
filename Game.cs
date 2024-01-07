using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAKE_CON
{
    public class Game
    {
        LinkedList<(int, int)> snake = new LinkedList<(int, int)>();
        (int, int) food = (0, 0);
        (int, int) dir = (0, 1);
        int width = 40;
        int height = 20;
        ConsoleColor current_color = Console.ForegroundColor;

        public void PlayGame()
        {
            Console.CursorVisible = false;
            InitGame();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    dir = key switch
                    {
                        ConsoleKey.UpArrow when dir != (1, 0) => (-1, 0),
                        ConsoleKey.DownArrow when dir != (-1, 0) => (1, 0),
                        ConsoleKey.LeftArrow when dir != (0, 1) => (0, -1),
                        ConsoleKey.RightArrow when dir != (0, -1) => (0, 1),
                        _ => dir
                    };
                }

                var new_head = (snake.First.Value.Item1 + dir.Item1, snake.First.Value.Item2 + dir.Item2);

                if (new_head == food)
                    PlaceFood();
                else
                {
                    snake.RemoveLast();
                }

                if (new_head.Item1 < 0 || new_head.Item1 >= height || new_head.Item2 < 0 || new_head.Item2 >= width || snake.Contains(new_head))
                    break;

                snake.AddFirst(new_head);
                RenderGame();
                Thread.Sleep(100);
            }
        }

        private void InitGame()
        {
            snake.Clear();
            snake.AddFirst((height / 2, width / 2));

            PlaceFood();
        }

        private void PlaceFood()
        {
            Random rand = new Random();

            do
            {
                food = (rand.Next(height), rand.Next(width));
            } while (snake.Contains(food));
        }

        private void RenderGame()
        {
            Console.Clear();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (snake.Contains((i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ForegroundColor = current_color;
                    }
                    else if (food == (i, j))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("*");
                        Console.ForegroundColor = current_color;
                    }
                    else
                        Console.Write(" ");
                }

                Console.WriteLine();
            }
        }
    }
}
