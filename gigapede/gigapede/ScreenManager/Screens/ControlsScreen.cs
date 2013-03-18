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
            this.AddItem( new KeySelector("Left:    ", UserInput.InputType.LEFT) );

            this.AddItem( new KeySelector("Right:   ", UserInput.InputType.RIGHT) );

            this.AddItem( new KeySelector("Up:      ", UserInput.InputType.UP) );

            this.AddItem( new KeySelector("Down:    ", UserInput.InputType.DOWN) );

            this.AddItem( new KeySelector("Escape:  ", UserInput.InputType.ESCAPE) );

            this.AddItem( new KeySelector("Fire:    ", UserInput.InputType.FIRE) );

            this.AddItem(
                new MenuQuitButton("Return to Main Menu", this)
            );
        }
    }
}
