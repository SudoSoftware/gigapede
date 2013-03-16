using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(ScreenManager manager, Screen exit_screen, MenuStyle style)
            : base(manager, exit_screen, "Main Menu", style)
        {
            this.AddItem(
                new AddScreenButton("Start Game", manager, typeof(CentipedeGame),
                    new Object[] { manager, this }
                )
            );

            this.AddItem(
                new AddScreenButton("High Scores", manager, typeof(HighScoresScreen),
                    new Object[] { manager, this, style }
                )
            );

            this.AddItem(
                new AddScreenButton("Controls", manager, typeof(ControlsScreen),
                    new Object[] { manager, this, style }
                )
            );

            this.AddItem(
                new AddScreenButton("Credits", manager, typeof(CreditsScreen),
                    new Object[] { manager, this, style.head_pos }
                )
            );

            this.AddItem(new MenuQuitButton("Quit", this));
        }
    }
}
