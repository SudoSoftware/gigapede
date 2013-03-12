using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
    /*
     * This class is a top level class managing screen display and resource management.
     * */
    class ScreenManager
    {
	    // Screen with input focus.
       	Screen focus;

    	// Queue of Screens.
    	List<Screen> screenqueue;

        // The resource manager.
        ResourceManager rm;

        // Input Class
        UserInput input;

        // The resource manager's accessor.
        public ResourceManager RM
        {
            get { return rm; }
        }

        // Constructor
        public ScreenManager(GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteb)
        {
            rm = new ResourceManager(graphics, content, spriteb);

            screenqueue = new List<Screen>();

            input = new UserInput();
        }

    	public void AddScreen (Screen new_screen)
    	{
           if (!screenqueue.Contains(new_screen))
             screenqueue.Add(new_screen);
	    }

    	public void KillScreen (Screen dead_screen)
    	{
    	    screenqueue.Remove(dead_screen);
    	}

    	public void FocusScreen (Screen focus_screen)
    	{
           focus = focus_screen;
    	}

    	public void Update (GameTime time)
    	{
    	    foreach (Screen x in screenqueue.ToArray())
    	        x.Update (time);

            input.Update();

            if (focus != null)
    	        focus.HandleInput(time, input);
    	}

    	public void Draw ()
    	{
    	    foreach (Screen x in screenqueue)
	            if (! x.hidden_p) x.Draw();
	    }
    }
}
