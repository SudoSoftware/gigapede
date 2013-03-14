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
        private int current_index;
        private List<String> options;
        private Hashtable optionhash;


        public ScrollButton (String init_string) : base (init_string)
        {
            current_index = -1;
            options = new List<string>();
            optionhash = new Hashtable();
        }

        public void AddAction (String name, int action, int index=-1)
        {
            if (index < 0)
                index = options.Count;

            options.Add(name);
            //optionhash.Add(name, Action);
        }

        // Not Yet Implemented
        public void RemoveAction (String name)
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
            if (current_index < 0)
                return;

            //optionhash[options[current_index]];
        }

        public override void HandleInput(GameTime time, UserInput input)
        {
            if (options.Count == 0)
                return;

            if (input.justPressed(UserInput.InputType.FIRE))
                RunAction();
            else if (input.justPressed(UserInput.InputType.LEFT))
                ScrollLeft();
            else if (input.justPressed(UserInput.InputType.RIGHT))
                ScrollRight();
        }
    }
}
