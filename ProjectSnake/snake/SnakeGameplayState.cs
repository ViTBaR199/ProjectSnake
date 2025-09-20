using share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    enum SnakeDir
    {
        Up, Down, Left, Right
    }
    internal class SnakeGameplayState : BaseGameState
    {
        private struct Cell
        {
            public int x; public int y;
            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private List<Cell> _body = new List<Cell>();
        private SnakeDir _currentDir = SnakeDir.Up;
        private float _timeToMove = 0f;

        public override void Reset()
        {
            _body.Clear();
            _currentDir = SnakeDir.Up;
            _body.Add(new Cell(0, 0));
            _timeToMove = 0f;
        }

        public override void Update(float deltaTIme)
        {
            _timeToMove -= deltaTIme;
            if (_timeToMove > 0)
                return;
            _timeToMove = 1f / 5f;

            var head = _body[0];
            Cell nextCell = ShiftTo(head, _currentDir);
            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
            Console.WriteLine($"Элемент {0} имеет координаты: {_body[0].x}, {_body[0].y}");
        }

        public void SetDirection (SnakeDir dir)
        {
            _currentDir = dir;
        }

        private Cell ShiftTo(Cell start, SnakeDir toDir)
        {
            switch (toDir)
            {
                case SnakeDir.Up:
                    return new Cell(start.x, start.y + 1);
                case SnakeDir.Down:
                    return new Cell(start.x, start.y - 1);
                case SnakeDir.Left:
                    return new Cell(start.x - 1, start.y);
                case SnakeDir.Right:
                    return new Cell(start.x + 1, start.y);
            }

            return start;
        }
    }
}
