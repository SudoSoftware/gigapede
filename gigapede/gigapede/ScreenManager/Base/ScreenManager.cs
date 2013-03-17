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

        // The parent Game.
        Game gm;

        // The resource manager.
        ResourceManager rm;

        // The audio manager.
        SoundtrackManager am;

        // The current song.
        public Song current_song;

        // Input Class
        UserInput input;

        // The screen which currently has focus.
        public Screen Focus
        {
            get { return focus; }
        }

        // The game accessor.
        public Game GM
        {
            get { return gm; }
        }

        // The resource manager's accessor.
        public ResourceManager RM
        {
            get { return rm; }
        }

        // The audio manager's accessor.
        public SoundtrackManager AM
        {
            get { return am; }
        }

        // Constructor
        public ScreenManager(Game game, GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteb)
        {
            gm = game;
            rm = new ResourceManager(graphics, content, spriteb);
            am = new SoundtrackManager();

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
            focus.GotFocus();
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
