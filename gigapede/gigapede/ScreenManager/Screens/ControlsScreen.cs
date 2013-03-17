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
    class ControlsScreen : MenuScreen
    {
        public ControlsScreen(ScreenManager manager, Screen exit_screen, MenuStyle style)
            : base(manager, exit_screen, "Controls", style)
        {
            ScrollButton test_scroll = new ScrollButton("Scroll");
            test_scroll.AddOption("xkcd");
            test_scroll.AddOption("ussr");

            MenuItem spacer = new MenuItem("");

            this.AddItem( new MenuItem("Fire:") );
            this.AddItem(
                test_scroll
            );

            this.AddItem(new MenuItem("ESC:"));
            this.AddItem(
                test_scroll
            );

            this.AddItem(new MenuItem("Up:"));
            this.AddItem(
                test_scroll
            );

            this.AddItem(new MenuItem("Down:"));
            this.AddItem(
                test_scroll
            );

            this.AddItem(new MenuItem("Left:"));
            this.AddItem(
                test_scroll
            );

            this.AddItem(new MenuItem("Right:"));
            this.AddItem(
                test_scroll
            );
            this.AddItem(spacer);

            this.AddItem(
                new MenuQuitButton("Return to Main Menu", this)
            );
        }
    }
}
