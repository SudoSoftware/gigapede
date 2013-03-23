using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
    class OptionsScreen : MenuScreen
    {
        public OptionsScreen(ScreenManager manager, Screen exit_screen, MenuStyle style)
            : base(manager, exit_screen, "Options", style)
        {
            this.AddItem(
                new AddScreenButton("Controls", manager, typeof(ControlsScreen),
                    new Object[] { manager, this, style }
                )
            );

            this.AddItem(
                new CustomTexturesButton(manager.RM)
            );

            this.AddItem(new MenuQuitButton("Quit", this));
        }
}
}
