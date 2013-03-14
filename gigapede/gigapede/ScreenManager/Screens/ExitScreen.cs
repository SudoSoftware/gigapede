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
    class ExitScreen : Screen
    {
        public ExitScreen (ScreenManager manager, Screen exit_screen) : base (manager, null)
        {
        }

        public override void HandleInput (GameTime time, UserInput input)
        {
            manager.GM.Exit();
        }
    }
}
