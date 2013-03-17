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
    class KeySelector : MenuButton
    {
        private bool collect_mode;
        private Keys chosen_key;
        private String label_name;
        private UserInput.InputType edit_key;

        public KeySelector(String init_text, UserInput.InputType edit_key)
            : base(init_text + edit_key.ToString())
        {
            collect_mode = false;
            this.edit_key = edit_key;
            label_name = init_text;
        }

        public override void  HandleInput(GameTime time, UserInput input)
        {
            if (input.justPressed(UserInput.InputType.FIRE))
            {
                if (collect_mode)
                {
                    chosen_key = Keyboard.GetState().GetPressedKeys()[0];
                    display_text = label_name + chosen_key.ToString();
                    collect_mode = false;
                }
                else
                {
                    collect_mode = true;
                }
            }
        }
        
    }
}
