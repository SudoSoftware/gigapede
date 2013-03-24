using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
    class DifficultyButton : ScrollButton
    {
        public DifficultyButton()
            : base(new List<String>() {"Easy", "Hard", "l33t"})
        {
            display_text = "Difficulty: Easy";
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime time, UserInput input)
        {
            if (options.Count == 0)
                return;

            if (input.justPressed(UserInput.InputType.FIRE))
                RunAction();
            else if (input.justPressed(UserInput.InputType.LEFT))
            {
                ScrollLeft();
                display_text = "Difficulty: " + options[current_index];
            }
            else if (input.justPressed(UserInput.InputType.RIGHT))
            {
                ScrollRight();
                display_text = "Difficulty: " + options[current_index];
            }
        }

        public override void RunAction()
        {
            switch (current_index)
            {
                case 0:
                    // Easy Settings
                    break;
                case 1:
                    // Hard Settings
                    break;
                case 2:
                    // l33t Settings
                    break;
            }
        }
    }
}
