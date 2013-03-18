using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace gigapede
{
	class UserInput
	{
		public enum InputType
		{
			UP, DOWN, LEFT, RIGHT,
			FIRE, ESCAPE
		}


		protected List<InputType> lastState = new List<InputType>();
		protected List<InputType> currentState = new List<InputType>();
		protected DateTime lastInputTime = new DateTime();


        public static Keys LeftKey;
        public static Keys RightKey;
        public static Keys UpKey;
        public static Keys DownKey;
        public static Keys EscKey;
        public static Keys FireKey;


		public UserInput():
			this(true)
		{ }



		public UserInput(bool immediateUpdate)
		{
			if (immediateUpdate)
				Update();
		}



        public void SetInputKey(InputType input, Keys key)
        {
            switch (input)
            {
                case InputType.LEFT:
                LeftKey = key;
                break;

                case InputType.RIGHT:
                RightKey = key;
                break;

                case InputType.UP:
                UpKey = key;
                break;

                case InputType.DOWN:
                DownKey = key;
                break;

                case InputType.ESCAPE:
                EscKey = key;
                break;

                case InputType.FIRE:
                FireKey = key;
                break;
            }
        }



		public void Update()
		{
			lastState.Clear();
			lastState.AddRange(currentState);
			currentState.Clear();

			UpdateState();

			if (currentState.Count > 0)
				lastInputTime = DateTime.Now;
		}



		protected virtual void UpdateState()
		{
			checkKeyboard(Keyboard.GetState());
			checkGamepad(GamePad.GetState(PlayerIndex.One));
		}



		public bool justPressed(InputType type)
		{
			return onNow(type) && !onLastTime(type);
		}



		public bool onLastTime(InputType type)
		{
			return lastState.Contains(type);
		}



		public bool onNow(InputType type)
		{
			return currentState.Contains(type);
		}



		private void checkKeyboard(KeyboardState keyboardState)
		{
			if (keyboardState.IsKeyDown(EscKey))
				currentState.Add(InputType.ESCAPE);

			if (keyboardState.IsKeyDown(UpKey))
				currentState.Add(InputType.UP);

			if (keyboardState.IsKeyDown(DownKey))
				currentState.Add(InputType.DOWN);

			if (keyboardState.IsKeyDown(LeftKey))
				currentState.Add(InputType.LEFT);

			if (keyboardState.IsKeyDown(RightKey))
				currentState.Add(InputType.RIGHT);

			if (keyboardState.IsKeyDown(FireKey) || keyboardState.IsKeyDown(Keys.Enter))
				currentState.Add(InputType.FIRE);
		}



		private void checkGamepad(GamePadState gamepadState)
		{
			if (gamepadState.Buttons.Back == ButtonState.Pressed)
				currentState.Add(InputType.ESCAPE);

			if (gamepadState.Buttons.Y == ButtonState.Pressed || gamepadState.ThumbSticks.Left.Y > 0)
				currentState.Add(InputType.UP);

			if (gamepadState.Buttons.A == ButtonState.Pressed || gamepadState.ThumbSticks.Left.Y < 0)
				currentState.Add(InputType.DOWN);

			if (gamepadState.Buttons.X == ButtonState.Pressed || gamepadState.ThumbSticks.Left.X < 0)
				currentState.Add(InputType.LEFT);

			if (gamepadState.Buttons.B == ButtonState.Pressed || gamepadState.ThumbSticks.Left.X > 0)
				currentState.Add(InputType.RIGHT);

			if (gamepadState.Triggers.Left == 1 || gamepadState.Triggers.Right == 1)
				currentState.Add(InputType.FIRE);
		}



		public TimeSpan GetTimeSinceLastInput()
		{
			return DateTime.Now.Subtract(lastInputTime);
		}
	}
}
