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
        private Vector2 position;

        private String display_string;

        public IntroScreen(ScreenManager manager, Screen exit_screen, Vector2 position)
            : base(manager, exit_screen)
        {
            start_time = DateTime.Now;
            this.position = position;

            display_string =
@"Library Computer Access/Retrival System
USS Enterprise-D (NCC 1701-D)
Current Bridge Crew:
    Rear Admiral James T. Kirk
    Captain Jean-Luc Picard
    Commander William Riker
    Acting Ensign Crusher
    Ensign Victors
    Ensign Michaelson
Welcome to the Starfleet Planar Combat Simulator";
        }

        public override void ExitScreen()
        {
            manager.KillScreen(this);
            manager.AddScreen(exit_screen);
            manager.FocusScreen(exit_screen);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            TimeSpan intro_length = DateTime.Now - start_time;

            if (intro_length.Seconds >= 6)
                ExitScreen();
        }

        public override void Draw()
        {
            SpriteBatch sb = manager.RM.SpriteB;

            SpriteFont font = manager.RM.Content.Load<SpriteFont>("LcarsFont");

            sb.Begin();
            sb.DrawString(font, display_string, position, Color.Orange);
            sb.End();
        }
    }
}
