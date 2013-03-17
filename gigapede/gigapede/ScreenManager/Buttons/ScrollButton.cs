using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
    class ScrollButton : MenuItem
    {
        protected int current_index;
        protected List<String> options;

        public ScrollButton (String init_option)
            : base (init_option)
        {
            current_index = 0;
            options = new List<String>();
            options.Add(init_option);
        }

        public ScrollButton(List<String> init_options)
            : base(init_options[0])
        {
           current_index = 0;
           options = init_options;
            
            if (init_options.Count == 0)
                options.Add("Default Option");
        }

        public void AddOption (String name, int index=-1)
        {
            if (options.Count == 0)
                display_text = name;

            if (index < 0)
                index = options.Count;

            options.Add(name);
        }

        // Not Yet Implemented
        public void RemoveOption (String name)
        {
        }

        public void ScrollLeft ()
        {
            if (current_index < 0)
                return;

            current_index--;

            if (current_index < 0)
                current_index = options.Count-1;
        }

        public void ScrollRight ()
        {
            if (current_index < 0)
                return;

            current_index++;

            if (current_index == options.Count)
                current_index = 0;
        }

        public void RunAction ()
        {
        }

        public override void HandleInput(GameTime time, UserInput input)
        {
            if (options.Count == 0)
                return;

            if (input.justPressed(UserInput.InputType.FIRE))
                RunAction();
            else if (input.justPressed(UserInput.InputType.LEFT))
            {
                ScrollLeft();
                display_text = options[current_index];
            }
            else if (input.justPressed(UserInput.InputType.RIGHT))
            {
                ScrollRight();
                display_text = options[current_index];
            }
        }
    }
}
