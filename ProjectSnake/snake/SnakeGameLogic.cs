using share;
using Shared;
using snake;
using System.Security;

namespace ProjectSnake.snake
{
    internal class SnakeGameLogic : BaseGameLogic
    {
        private SnakeGameplayState _gameplayState = new SnakeGameplayState();
        private bool newGamePending = false;
        private int currentLevel;
        private ShowTextState showTextState = new ShowTextState(2f);

        private void GoToNextLevel()
        {
            currentLevel++;
            newGamePending = false;
            showTextState.text = $"Level: {currentLevel}!";
            ChangeState(showTextState);
        }
        private void GoToGameOver()
        {
            currentLevel = 0;
            newGamePending = true;
            showTextState.text = $"Game Over!";
            ChangeState(showTextState);
        }

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
            if (currentState != null && !currentState.IsDone())
                return;

            if (currentState == null || currentState == _gameplayState && !_gameplayState.gameOver)
                this.GoToNextLevel();

            else if (currentState == _gameplayState && _gameplayState.gameOver)
                this.GoToGameOver();

            else if (currentState != _gameplayState && newGamePending)
                this.GoToNextLevel();

            else if (currentState != _gameplayState && !newGamePending)
                this.GotoGameplay();
        }

        public void GotoGameplay()
        {
            _gameplayState.level = currentLevel;
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
