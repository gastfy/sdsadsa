using System.Threading;

namespace ConsoleApp13
{
    internal class Snake
    {
        private int BORDER_MAX_X;
        private int BORDER_MAX_Y;

        public void init(int MAX_X, int MAX_Y)
        {
            BORDER_MAX_X = MAX_X;
            BORDER_MAX_Y = MAX_Y;
        }

        private int X = 5;
        private int Y = 6;
        private int length = 1;
        private bool Escape = true;
        private string direction = "Right";
        List<(int, int)> snakeTrail = new List<(int, int)>();
        List<(int, int)> borders = new List<(int, int)>();

        public void show()
        {
            for(int i = 0; i < BORDER_MAX_X; i++)
            {
                borders.Add((i, 0));
                Console.SetCursorPosition(i, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("#");
                Console.ResetColor();
            }
            for (int i = 0; i < BORDER_MAX_X; i++)
            {
                borders.Add((i, BORDER_MAX_Y - 1));
                Console.SetCursorPosition(i, BORDER_MAX_Y-1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("#");
                Console.ResetColor();
            }
            for (int i = 0; i < BORDER_MAX_Y; i++)
            {
                borders.Add((0, i));
                Console.SetCursorPosition(0, i);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("#");
                Console.ResetColor();
            }
            for (int i = 0; i < BORDER_MAX_Y; i++)
            {
                borders.Add((BORDER_MAX_X-1, i));
                Console.SetCursorPosition(BORDER_MAX_X-1, i);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("#");
                Console.ResetColor();
            }
            snakeMove();
        }

        private void snakeMove()
        {

            Thread thread = new Thread(new ThreadStart(Update));
            Thread thread1 = new Thread(new ThreadStart(FoodSpawn));
            thread1.Start();
            thread.Start();
            while (Escape)
            {  
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    direction = "Up";
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    direction = "Down";
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    direction = "Left";
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    direction = "Right";
                }
            }

        }
        private void Update()
        {
            while (Escape)
            {
                snakeTrail.Add((X, Y));
                if(snakeTrail.Count > length) { 
                    Console.SetCursorPosition(snakeTrail[0].Item1, snakeTrail[0].Item2);
                    Console.Write(' ');
                    snakeTrail.Remove(snakeTrail[0]); 
                }
                for (int i = length-2; i > 0; i--)
                {
                    if((X,Y) == snakeTrail[i]) { Escape = false; break; }
                }
                foreach ((int, int) item in borders)
                {
                    if (item == (X, Y))
                    {
                        Escape = false;
                        break;
                    }
                }
                Console.SetCursorPosition(X, Y);
                Console.Write('0');
                if (direction == "Right") { X += 1; }
                if (direction == "Up") { Y -= 1; }
                if (direction == "Down") { Y += 1; }
                if (direction == "Left") { X -= 1; }
                Thread.Sleep(350);
            }
        }

        private void FoodSpawn()
        {
            Random rand = new Random();
            int FoodX = rand.Next(1, BORDER_MAX_X - 1);
            int FoodY = rand.Next(1, BORDER_MAX_Y - 1);
            Console.SetCursorPosition(FoodX, FoodY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('@');
            Console.ResetColor();
            Console.SetCursorPosition(BORDER_MAX_X + 10, 0);
            Console.Write("Ваше количество очков: " + (length - 1));
            while (true)
            {
                if (Escape == false) { break; }
                if((X,Y) == (FoodX, FoodY))
                {
                    length += 1;
                    FoodX = rand.Next(1, BORDER_MAX_X-1);
                    FoodY = rand.Next(1, BORDER_MAX_Y-1);
                    Console.SetCursorPosition(FoodX, FoodY);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('@');
                    Console.ResetColor();
                    Console.SetCursorPosition(BORDER_MAX_X + 10, 0);
                    Console.Write("Ваше количество очков: " + (length - 1));
                }


            }

        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            Console.Write("Введите MAX_X(min 10): ");
            int MAX_X = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите MAX_Y(min 10): ");
            int MAX_Y = Convert.ToInt32(Console.ReadLine());
            snake.init(MAX_X, MAX_Y);
            Console.Clear();
            snake.show();
        }
    }
}
/*
Хватит стараться, хватит пытаться
Уже ничего не произойдёт
Мне бы не сдаться, мне бы остаться
Но больше не биться головой об лёд
*/