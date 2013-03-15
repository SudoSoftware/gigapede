using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
    class MenuQuitButton : MenuButton
    {
        MenuScreen parent_screen;

        public MenuQuitButton(String item_text, MenuScreen screen)
            : base(item_text)
        {
            parent_screen = screen;
        }

        public override void RunAction ()
        {
            parent_screen.ExitScreen();
        }
    }
}
