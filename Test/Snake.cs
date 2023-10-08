using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    class Snake
    {
        private List<Position> _body;
        private int _growthQueue;

        public Snake(int startX, int startY, int initialLength)
        {
            _body = new List<Position>();
            for (int i = 0; i < initialLength; i++)
            {
                _body.Add(new Position(startX - i, startY));
            }
            _growthQueue = 0;
        }

        public List<Position> GetBody()
        {
            return _body;
        }

        public void Move(Direction direction)
        {
            Position newHead = CalculateNewHead(direction);
            _body.Insert(0, newHead);

            if (_growthQueue > 0)
            {
                _growthQueue--;
            }
            else
            {
                _body.RemoveAt(_body.Count - 1);
            }
        }

        public void Grow()
        {
            _growthQueue += 1;
        }

        private Position CalculateNewHead(Direction direction)
        {
            Position head = _body.First();
            Position newHead = new Position(head.X, head.Y);

            switch (direction)
            {
                case Direction.Up:
                    newHead.Y -= 1;
                    break;
                case Direction.Down:
                    newHead.Y += 1;
                    break;
                case Direction.Left:
                    newHead.X -= 1;
                    break;
                case Direction.Right:
                    newHead.X += 1;
                    break;
            }

            return newHead;
        }
    }

}
