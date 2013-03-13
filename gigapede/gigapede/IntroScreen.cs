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
    class IntroScreen : Screen
    {
        private DateTime start_time;
        private Screen main_menu;

        private String display_string;

        public IntroScreen(ScreenManager manager, Screen exit_screen, Screen menu_screen)
            : base(manager, exit_screen)
        {
            start_time = DateTime.Now;
            this.main_menu = menu_screen;
        }

        public override void ExitScreen()
        {
            manager.KillScreen(this);
            manager.AddScreen(exit_screen);
            manager.AddScreen(main_menu);
            manager.FocusScreen(main_menu);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            TimeSpan intro_length = DateTime.Now - start_time;

            display_string = 
@"Library Computer Access/Retrival System
USS Enterprise-D (NCC 1701-D)\n
Current Bride Crew:
    Rear Admiral James T. Kirk
    Captain Jean-Luc Picard
    Cmdr. William Riker
    Acting Ensign Crusher
    Ensign Victors
    Ensign Michaelson
Welcome to the Starfleet Planar Combat Simulator";

            if (intro_length.Seconds >= 20)
                ExitScreen();
        }

        public override void Draw()
        {
            SpriteBatch sb = manager.RM.SpriteB;

            SpriteFont font = manager.RM.Content.Load<SpriteFont>("LcarsFont");

            sb.Begin();
            sb.DrawString(font, display_string, new Vector2(270, 220), Color.Orange); 
            sb.End();
        }
    }
}
