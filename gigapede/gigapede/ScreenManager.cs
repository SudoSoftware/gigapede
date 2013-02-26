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
        // The resource manager.
        ResourceManager rm;

        // The resource manager's accessor.
        public ResourceManager RM
        {
            get { return rm; }
        }

        // Constructor
        public ScreenManager(GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteb)
        {
            rm = new ResourceManager(graphics, content, spriteb);
        }
    }
}
