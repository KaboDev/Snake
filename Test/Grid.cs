using System;

namespace Snake
{
    class Grid
    {
        private int _width;
        private int _height;
        private char[,] _grid;

        public Grid(int width, int height)
        {
            _width = width;
            _height = height;
            _grid = new char[width, height];
        }

        public void Clear()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _grid[j, i] = ' ';
                }
            }
            Console.Clear();
        }

        public void Draw()
        {
            int borderSizeX = _width + 2;
            int borderSizeY = _height + 2;

            for (int y = 1; y <= borderSizeY; y++)
            {
                for (int x = 1; x <= borderSizeX; x++)
                {
                    if (y == 1 && x == 1 || y == borderSizeY && x == 1 || y == 1 && x == borderSizeX || y == borderSizeY && x == borderSizeX) Console.Write("+");
                    else if (y == 1 || y == borderSizeY) Console.Write("-");
                    else if (x == 1 || x == borderSizeX) Console.Write("|");
                    else Console.Write(_grid[x - 2, y - 2]);
                }
                Console.WriteLine();
            }
        }

        public void SetCell(int x, int y, char c)
        {
            _grid[x, y] = c;
        }

        public int Width => _width;
        public int Height => _height;
    }
}
