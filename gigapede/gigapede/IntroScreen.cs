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

            if ((DateTime.Now - start_time).Seconds >= 3)
                ExitScreen();
        }

        public override void Draw()
        {
            base.Draw();

            manager.RM.Graphics.GraphicsDevice.Clear(Color.PowderBlue);
        }
    }
}
