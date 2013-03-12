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
    class MenuScreen : Screen
    {
	private String head_text;

        private MenuStyle style;

	private List<MenuItem> menu_items;

	public MenuScreen (ScreenManager manager, Screen exit_screen, String head_text, MenuStyle style) : base (manager, exit_screen)
	{
	    this.head_text = head_text;
	    this.style = style;
	}

	public void ChangeStyle (MenuStyle style)
	{
	    this.style = style;
	}

	public void AddItem (MenuItem new_item, int position = -1)
	{
        if (position < 0)
            position = menu_items.Count;

	    menu_items.Insert(position, new_item);
	}

	public void KillItem (MenuItem dead_item)
	{
        menu_items.Remove(dead_item);
	}

	public void SetItemActive (MenuItem item, bool active)
	{
	    if (menu_items.Contains(item))
                item.SetActive(active);
	}

	public override void HandleInput (GameTime time, UserInput input)
	{
	    // Based on input, move the highlighted item across the screen.
	    // If the input is not used here, pass it to the item.

        foreach (MenuItem x in menu_items)
            x.HandleInput(time, input);
	}

	public override void Draw ()
	{
	    SpriteBatch sb = manager.RM.SpriteB;
	    SpriteFont head_font = style.head_font;
        Color head_color = style.head_color;

	    Vector2 position = new Vector2(style.head_pos.X, style.head_pos.Y);
	    sb.DrawString(head_font, head_text, position, head_color);
        
	    position = new Vector2(style.menu_start.X, style.menu_start.Y);

	    foreach (MenuItem x in menu_items)
	    {
	        x.Draw(manager, style, position);
		    position.X += style.menu_start.X;
		    position.Y += style.menu_start.Y;
	    }
	}

    }
}
