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
    class CreditsScreen : Screen
    {
        Vector2 position;
        String display_string;

        public CreditsScreen(ScreenManager manager, Screen exit_screen, Vector2 position)
            : base(manager, exit_screen)
        {
            this.position = position;


            display_string =
@"Library Computer Access/Retrival System
USS Enterprise-D (NCC 1701-D)
Processing Request 'GetCredits':
Returned:
    The Starfleet Planar Combat Simulator was developed by:
        Ensign Parker C. Michaelson
        Ensign Jesse Victors
        Commander James D. Mathias (supervisory)";
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
