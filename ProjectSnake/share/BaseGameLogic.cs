using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share
{
    internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
    {
        protected BaseGameState? currentState { get; private set; }
        protected float time {  get; private set; }
        protected int screenWidth { get; private set; }
        protected int screenHeight { get; private set; }

        public abstract ConsoleColor[] CreatePallet();

        public abstract void OnArrowDown();

        public abstract void OnArrowLeft();

        public abstract void OnArrowRight();

        public abstract void OnArrowUp();

        public void InitializeInput(ConsoleInput consoleInput)
        {
            consoleInput.Subscribe(this);
        }

        public abstract void Update(float deltaTime);

        protected void ChangeState(BaseGameState state)
        {
            currentState?.Reset();
            currentState = state;
        }

        public void DrawNewState(float  deltaTime, ConsoleRenderer renderer)
        {
            time += deltaTime;
            screenWidth = renderer.width;
            screenHeight = renderer.height;
            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);
            this.Update(deltaTime);
        }
    }
}
