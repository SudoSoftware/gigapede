using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
    class MenuItem
    {
        private bool active;

	    private String display_text;

        public MenuItem (String init_text)
	    {
            active = true;
	        display_text = init_text;
	    }

        public void SetActive(bool setbool)
        {
            active = setbool;
        }

    	public void HandleInput (GameTime time, UserInput input)
	    {
    	}

    	public void Draw (ScreenManager manager, MenuStyle style, Vector2 position)
    	{
    	    SpriteBatch sb = manager.RM.SpriteB;
    	    SpriteFont font = style.font;
            Color color = style.menu_color;

            sb.DrawString(font, display_text, position, color);
    	}
    }
}
