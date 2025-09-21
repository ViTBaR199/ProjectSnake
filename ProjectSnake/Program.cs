using ProjectSnake.snake;
using share;
using Shared;

internal class Program
{
    const float targetFrameTime = 1f / 60f;
    static void Main()
    {
        SnakeGameLogic gameLogic = new SnakeGameLogic();
        ConsoleInput input = new ConsoleInput();
        ConsoleColor[] pallete = gameLogic.CreatePallet();

        ConsoleRenderer renderer0 = new ConsoleRenderer(pallete);
        ConsoleRenderer renderer1 = new ConsoleRenderer(pallete);

        var prevRenderer = renderer0;
        var currRenderer = renderer1;
        

        gameLogic.InitializeInput(input);

        var lastFrameTime = DateTime.Now;

        while (true)
        {
            var frameStartTime = DateTime.Now;
            var deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;

            input.Update();
            gameLogic.DrawNewState(deltaTime, currRenderer);
            lastFrameTime = frameStartTime;

            if (!currRenderer.Equals(prevRenderer)) currRenderer.Render();

            var tmp = prevRenderer;
            prevRenderer = currRenderer;
            currRenderer = tmp;
            currRenderer.Clear();

            
            
            var nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
            var endFrameTime = DateTime.Now;

            if (nextFrameTime > endFrameTime)
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
        }
    }
}