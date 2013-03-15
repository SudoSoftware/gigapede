using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
    class UserInput
    {
        public enum InputType
        {
            UP, DOWN, LEFT, RIGHT,
            FIRE, ESCAPE
        }


        private List<InputType> lastState = new List<InputType>();
        private List<InputType> currentState = new List<InputType>();


        public void Update()
        {
            lastState.Clear();
            lastState.AddRange(currentState);
            currentState.Clear();

            checkKeyboard(Keyboard.GetState());
            //checkGamepad(GamePad.GetState(PlayerIndex.One)); //TODO: FINALIZE
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
            if (keyboardState.IsKeyDown(Keys.Escape))
                currentState.Add(InputType.ESCAPE);

            if (keyboardState.IsKeyDown(Keys.Up))
                currentState.Add(InputType.UP);

            if (keyboardState.IsKeyDown(Keys.Down))
                currentState.Add(InputType.DOWN);

            if (keyboardState.IsKeyDown(Keys.Left))
                currentState.Add(InputType.LEFT);

            if (keyboardState.IsKeyDown(Keys.Right))
                currentState.Add(InputType.RIGHT);

            if (keyboardState.IsKeyDown(Keys.Space))
                currentState.Add(InputType.FIRE);
        }



        private void checkGamepad(GamePadState gamepadState)
        {
            if (gamepadState.Buttons.Back == ButtonState.Pressed)
                currentState.Add(InputType.ESCAPE);

            if (gamepadState.Triggers.Left == 1 || gamepadState.Triggers.Right == 1)
                currentState.Add(InputType.FIRE);

            //TODO: IMPLEMENT MOVEMENT
        }
    }
}
