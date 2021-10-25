using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Game
{
    class Program
    {
        public static Position currentPosition { get; set; }
        public static List<Position> allPoints = new List<Position>();
        public static int step = 0;
        public static Random rnd = new Random();
        public static int numShapes = rnd.Next(2, 6);
        public static int numGoodSteps = 0;

        public class Shape
        {
            public int Left { get; set; } 
            public int Top { get; set; } 
            public int size { get; set; } 
            public char TheChar { get; set; } 
            public ConsoleColor color { get; set; }
            public Position start { get; set; }
            public Shape()
            {
                Random rnd = new Random();
                Left = rnd.Next(0,Console.WindowWidth );
                Top = rnd.Next(0,Console.WindowHeight);
                start = new Position(Left, Top);
                while (!isEmptyPosition(start))
                {
                    Left = rnd.Next(0, Console.WindowWidth);
                    Top = rnd.Next(0, Console.WindowHeight);
                    start = new Position(Left, Top);

                }
                color =  GetRandomConsoleColor();
                Console.ForegroundColor = color;
               
                
            }
        }
        private static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(rnd.Next(1,consoleColors.Length)); //not black
        }

        public class Line:Shape
        {
            public Line()
            {
                size = rnd.Next(2,11);
                for (int i=0;i < size;i++)
                {
                    if (start.X + i < Console.WindowWidth)
                    {

                        Console.SetCursorPosition(start.X + i, start.Y);
                        allPoints.Add(new Position(start.X + i, start.Y));
                        Console.Write('=');
                    }


                }
              
            }
        }

        public class Triangle : Shape
        {
            public Triangle()
            {
                size = rnd.Next(2, 10);
                for (int i = 0; i < size; i++)
                {
                    if (start.Y+i< Console.WindowHeight)
                    {

                    
                    for (int j = 0; j <= i; j++)
                    {
                        if (start.X +j < Console.WindowWidth)
                        {
                        Console.SetCursorPosition(start.X+j, start.Y+i);
                        allPoints.Add(new Position(start.X + j, start.Y + i));
                        Console.Write('#');
                        }

                    }
                    }


                }

            }
        }

        public class rectangle : Shape
        {
            public int length { get; set; }
            public rectangle()
            {
                size = rnd.Next(2, Console.WindowWidth/4);
                length = rnd.Next(1, 11);
                for (int i = 0; i < size; i++)
                {
                    if (start.X + i <= Console.WindowWidth)
                    {

                        for (int j = 0; j < length; j++)
                        {

                            if (start.Y + j <= Console.WindowHeight)
                            {
                                Console.SetCursorPosition(start.X + i, start.Y + j);
                                allPoints.Add(new Position(start.X + i, start.Y + j));
                                Console.Write('ם');
                            }

                        }
                    }


                }

            }
        }

        public class square : Shape
        {
            public square()
            {
                size = rnd.Next(3, 11);

                for (int i = 0; i < size; i++)
                {
                    if (start.X + i < Console.WindowWidth)
                    {


                        for (int j = 0; j < size; j++)
                        {
                            if (start.Y + j < Console.WindowHeight)
                            {
                                Console.SetCursorPosition(start.X + i, start.Y + j);
                                allPoints.Add(new Position(start.X + i, start.Y + j));
                                Console.Write('ם');
                            }
                            else break;
                        }
                    }


                }

            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Windows-1255");

            initGame();

            //Console.Read();
     

            while (true)
            {
                var key = Console.ReadKey(true);
                PaintBlue(currentPosition);
                switch (key.Key)
                {

                    case ConsoleKey.UpArrow:
                        Move(0, -1);
                        break;
                    case ConsoleKey.RightArrow:
                        Move(1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        Move(0, 1);
                    
                        break;
                    case ConsoleKey.LeftArrow:
                        Move(-1, 0);
                        break;
                }
                //Thread.Sleep(50);

                // valid = false;
            }
        }
        static void initGame()
        {
            Console.Clear();
            allPoints.Clear();
            numShapes += 1;
            Console.WriteLine(numShapes);
            if (numShapes == 15)
            {
                GameOver();
            }
            for (int i = 0; i < numShapes; i++)
            {

                switch (rnd.Next(1, 5))
                {
                    case 1:
                        new square();
                        break;
                    case 2:
                        new rectangle();
                        break;
                    case 3:
                        new Triangle();

                        break;
                    case 4:
                        new Line();
                        break;
                }
            }
            Thread.Sleep(1000);
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 25);

            currentPosition = new Position();
             int j = 0;
            while (!isEmptyPosition(currentPosition))
            {
                j += 1;
                if (j == 30)
                {
                    GameOver();
                }
                currentPosition = new Position();
                Thread.Sleep(50);
            }
            allPoints.Add(currentPosition);
            Console.SetCursorPosition(currentPosition.X, currentPosition.Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('*');

            


        }

        static void PaintBlue(Position pos)
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write('*');
        }
        static void Move(int x, int y)

        {
            if (currentPosition.X + x >= 0 && currentPosition.X + x <= Console.WindowWidth
               && currentPosition.Y + y >= 0 && currentPosition.Y + y <= Console.WindowHeight)
            {
            

            Position newPos = new Position(currentPosition.X + x, currentPosition.Y + y);
              if (isEmptyPosition(newPos))
                {
                numGoodSteps += 1;
            Console.SetCursorPosition(newPos.X, newPos.Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('*');

            currentPosition = newPos;
                allPoints.Add(currentPosition);

            }
            else
            {
                Thread.Sleep(2000);
                initGame();
            }
        }
        }
        static bool isEmptyPosition(Position pos)
        {
            bool empty = true;
            if (allPoints.Any(p => p.X == pos.X && p.Y == pos.Y))
            {
                empty = false;
            }

            return empty;
        }
        static void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game Over");
            Console.WriteLine("Your score: " + numGoodSteps);
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

    }
    class Position
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top
        public Position()
        {
            Random rnd = new Random();
            X = rnd.Next(Console.WindowWidth);
           Y = rnd.Next(Console.WindowHeight);
        }
        public Position(int x, int y)
        {
           
            X = x;
            Y = y;
        }
    }
}
