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
    class BackgroundScreen : Screen
    {
        public BackgroundScreen(ScreenManager manager)
            : base(manager, null)
        {
        }

        public override void Draw()
        {
            SpriteBatch sb = manager.RM.SpriteB;
            GraphicsDeviceManager gm = manager.RM.Graphics;

            sb.Begin();
            sb.Draw(manager.RM.Background,
                new Rectangle(0, 0,
                    gm.PreferredBackBufferWidth, gm.PreferredBackBufferHeight),
                    Color.White);
            sb.End();
        }
    }
}
