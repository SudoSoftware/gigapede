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
        private bool capture_mode;
        private String label_name;
        private UserInput.InputType edit_key;

        public KeySelector(String init_text, UserInput.InputType edit_key)
            : base(init_text + edit_key.ToString())
        {
            capture_mode = false;
            this.edit_key = edit_key;
            label_name = init_text;
        }

        public override void  HandleInput(GameTime time, UserInput input)
        {
            Keys[] temp = Keyboard.GetState().GetPressedKeys();

            if (temp.Length > 0)
            {
                Keys key = temp[0];
                if (capture_mode && key != Keys.Enter)
                {
                    input.SetInputKey(edit_key, key);
                    display_text = label_name + key.ToString();
                    capture_mode = false;
                }
                else if (input.justPressed(UserInput.InputType.FIRE))
                    capture_mode = true;
            }
        }
        
    }
}
