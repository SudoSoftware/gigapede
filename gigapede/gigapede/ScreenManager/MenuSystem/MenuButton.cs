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
    class MenuButton : MenuItem
    {
        public MenuButton(String item_text)
            : base(item_text)
        {
            this.display_text = item_text;
        }

        public void SetText(String new_text)
        {
            display_text = new_text;
        }

        public virtual void RunAction()
        {
        }

        public override void HandleInput(GameTime time, UserInput input)
        {
            if (input.justPressed(UserInput.InputType.FIRE))
                RunAction();
        }
    }
}
