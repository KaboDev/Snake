using System;
using System.Linq;
using System.Threading;
namespace Snake
{
    class Game
    {
        private Grid _grid;
        private Snake _snake;
        private Position _food;
        private Random _random;
        private int _score;

        private bool _gameOver;

        private int _timerCooldown;

        public Game(int width, int height, int speed) => Initialize(width, height, speed);

        private void Initialize(int width, int height, int speed)
        {
            _timerCooldown = (int)(1 / (float)speed * 1000);
            _grid = new Grid(width, height);
            _snake = new Snake(width / 2, height / 2, 1);
            _random = new Random();
            SpawnFood();
            _score = 0;
        }

        public void Run()
        {
            _gameOver = false;

            ConsoleKeyInfo keyInfo;
            Direction currentDirection = Direction.None;
            Direction newDirection = Direction.None;

            Thread tInput = new Thread(() =>
            {
                while (!_gameOver)
                {
                    if (Console.KeyAvailable)
                    {
                        keyInfo = Console.ReadKey(true);

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                newDirection = Direction.Up;
                                break;
                            case ConsoleKey.DownArrow:
                                newDirection = Direction.Down;
                                break;
                            case ConsoleKey.LeftArrow:
                                newDirection = Direction.Left;
                                break;
                            case ConsoleKey.RightArrow:
                                newDirection = Direction.Right;
                                break;
                            case ConsoleKey.Escape:
                                return;
                        }
                    }
                }
            });
            Thread tGame = new Thread(() =>
            {
                while (true)
                {
                    if (IsValidDirection(currentDirection, newDirection))
                    {
                        currentDirection = newDirection;
                    }

                    _snake.Move(currentDirection);
                    CheckCollision();

                    if (_gameOver) break;

                    UpdateGrid();
                    _grid.Draw();

                    Console.WriteLine($"Score: {_score}");

                    Thread.Sleep(_timerCooldown); // Adjust the speed of the game
                }
            });

            tInput.Start();
            tGame.Start();

            tInput.Join();
            tGame.Join();
        }

        private bool IsValidDirection(Direction current, Direction newDirection)
        {
            if (current == Direction.Up && newDirection == Direction.Down) return false;
            if (current == Direction.Down && newDirection == Direction.Up) return false;
            if (current == Direction.Left && newDirection == Direction.Right) return false;
            if (current == Direction.Right && newDirection == Direction.Left) return false;

            return true;
        }

        private void SpawnFood()
        {
            int x, y;
            do
            {
                x = _random.Next(0, _grid.Width);
                y = _random.Next(0, _grid.Height);
            } while (_snake.GetBody().Any(p => p.X == x && p.Y == y));

            _food = new Position(x, y);
        }

        private void UpdateGrid()
        {
            _grid.Clear();
            _grid.SetCell(_food.X, _food.Y, '@'); // Food
            foreach (Position segment in _snake.GetBody())
            {
                _grid.SetCell(segment.X, segment.Y, '■'); // Snake body
            }
        }

        private void CheckCollision()
        {
            Position head = _snake.GetBody().First();

            // Check if the snake eats the food
            if (head.X == _food.X && head.Y == _food.Y)
            {
                _snake.Grow();
                SpawnFood();
                _score++;
            }

            // Check if the snake collides with wall
            if (head.X < 0 || head.X >= _grid.Width || head.Y < 0 || head.Y >= _grid.Height)
            {
                GameOver();
            }

            // Check if the snake collides with itself
            foreach (Position segment in _snake.GetBody().Skip(1))
            {
                if (segment.X == head.X && segment.Y == head.Y)
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            _gameOver = true;

            Console.CursorVisible = true;

            Console.Clear();
            Console.WriteLine("+--------------------+");
            Console.WriteLine("|     Game Over!     |");
            Console.WriteLine($"|   Your score: {_score.ToString("D2")}   |");
            Console.WriteLine("+--------------------+");
            Console.WriteLine("Press Enter to continue.");
            ConsoleKey key;
            do key = Console.ReadKey(intercept: true).Key; while (key != ConsoleKey.Enter);
        }
    }
}
