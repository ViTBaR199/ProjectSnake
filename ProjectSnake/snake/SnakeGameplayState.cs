using share;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public int fieldWidth;
        public int fieldHeight;
        const char squareSymbol = '■';
        const char circleSymbol = '0';
        public bool gameOver;
        public bool hasWon;
        public int level;

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
        private Cell _apple = new Cell();
        private Random _random = new Random();

        public override void Reset()
        {
            _body.Clear();
            var middleY = fieldHeight / 2;
            var middleX = fieldWidth / 2;

            _currentDir = SnakeDir.Up;
            gameOver = false;
            hasWon = false;

            _body.Add(new Cell(middleX, middleY));
            _apple = new Cell(middleX - 5, middleY - 5);
            _timeToMove = 0f;
        }

        public override void Update(float deltaTIme)
        {
            _timeToMove -= deltaTIme;
            if (_timeToMove > 0 || gameOver)
                return;
            _timeToMove = 1f / (level + 4f);

            var head = _body[0];
            Cell nextCell = ShiftTo(head, _currentDir);

            if (nextCell.Equals(_apple))
            {
                _body.Insert(0, _apple);
                if (_body.Count > level + 3) 
                    hasWon = true;

                GenerateApple();
                return;
            }

            if (nextCell.y < 0 || nextCell.x < 0 || nextCell.y >= fieldHeight || nextCell.x >= fieldWidth)
            {
                gameOver = true;
                return;
            }

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);
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
                    return new Cell(start.x, start.y - 1);
                case SnakeDir.Down:
                    return new Cell(start.x, start.y + 1);
                case SnakeDir.Left:
                    return new Cell(start.x - 1, start.y);
                case SnakeDir.Right:
                    return new Cell(start.x + 1, start.y);
            }

            return start;
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            renderer.DrawString($"Score: {_body.Count - 1}", 2, 1, ConsoleColor.Blue);

            renderer.DrawString($"Level: {level}", 2, 3 , ConsoleColor.Blue);

            foreach(var cell in _body)
            {
                renderer.SetPixel(cell.x, cell.y, squareSymbol, 3);
            }
            renderer.SetPixel(_apple.x, _apple.y, circleSymbol, 1);
        }

        private void GenerateApple()
        {
            Cell cell;
            cell.x = _random.Next(fieldWidth);
            cell.y = _random.Next(fieldHeight);

            if (_body[0].Equals(cell))
            {
                if (cell.y > fieldHeight / 2) 
                    cell.y--;
                else cell.y++;
            }

            _apple = cell;
        }

        public override bool IsDone()
        {
            return gameOver || hasWon;
        }
    }
}
