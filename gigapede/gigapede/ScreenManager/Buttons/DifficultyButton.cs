using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.Resources;

namespace gigapede
{
    enum Difficulty
    {
        Easy, Hard, l33t
    }

    class DifficultyButton : ScrollButton
    {
        public DifficultyButton()
            : base(new List<String>() {"Easy", "Hard", "l33t"})
        {
            current_index = (int)GameParameters.difficulty;
            display_text = "Difficulty: " + options[(int)GameParameters.difficulty];
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
            GameParameters.difficulty = (Difficulty) current_index;

            switch (current_index)
            {
                    // Easy
                case 0:
                    GameParameters.CENTIPEDE_SPEED = 0.4f;
		            GameParameters.SCORPION_SPEED = 0.3f;
		            GameParameters.SPIDER_SPEED = 0.25f;
		            GameParameters.ROCKET_SPEED = 0.8f;
                    GameParameters.FLEA_SPEED = 0.3f;
                    break;

                    // Hard
                case 1:
                    GameParameters.CENTIPEDE_SPEED = 0.6f;
		            GameParameters.SCORPION_SPEED = 0.2f;
		            GameParameters.SPIDER_SPEED = 0.45f;
		            GameParameters.ROCKET_SPEED = 1.0f;
                    GameParameters.FLEA_SPEED = 0.5f;
                    break;

                    // L33t
                case 2:
                    GameParameters.CENTIPEDE_SPEED = 0.8f;
		            GameParameters.SCORPION_SPEED = 0.6f;
		            GameParameters.SPIDER_SPEED = 0.5f;
		            GameParameters.ROCKET_SPEED = 1.6f;
                    GameParameters.FLEA_SPEED = 0.6f;
                    break;
            }
        }
    }
}
