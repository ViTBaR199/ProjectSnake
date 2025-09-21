using share;
using snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake.snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState _gameplayState = new SnakeGameplayState();

        public override void OnArrowDown()
        {
            if(currentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            if (currentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            if (currentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Right);
        }

        public override void OnArrowUp()
        {
            if (currentState != _gameplayState) return;
            _gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void Update(float deltaTime)
        {
            if(currentState != _gameplayState) this.GotoGameplay();
        }

        public void GotoGameplay()
        {
            _gameplayState.fieldHeight = screenHeight;
            _gameplayState.fieldWidth = screenWidth;
            this.ChangeState(_gameplayState);
            _gameplayState.Reset();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return
                [
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.Blue,
                ConsoleColor.Yellow
                ];
        }
    }
}
