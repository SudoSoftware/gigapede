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
        protected bool active;

	    protected String display_text;

        public MenuItem (String init_text)
	    {
            active = true;
	        display_text = init_text;
	    }

        public void SetActive(bool setbool)
        {
            active = setbool;
        }

    	public virtual void HandleInput (GameTime time, UserInput input)
	    {
    	}

    	public virtual void Draw (ScreenManager manager, MenuStyle style, Vector2 position, bool selected)
    	{
    	    SpriteBatch sb = manager.RM.SpriteB;
    	    String font = style.menu_font;
            Color color = style.menu_color;
            if (selected)
                color = style.selected_color;

            sb.Begin();
            sb.DrawString(
                (SpriteFont)manager.RM.FontHash[font],
                display_text,
                position,
                color
            );
            sb.End();
    	}
    }
}
