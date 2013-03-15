using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
    class AddScreenButton : MenuButton
    {
        ScreenManager manager;
        Type screen_type;
        Object[] arguments;

        public AddScreenButton(String init_text, ScreenManager manager, Type screen_type, Object[] arguments)
            : base(init_text)
        {
            this.screen_type = screen_type;
            this.manager = manager;
            this.arguments = arguments;
        }

        public override void RunAction()
        {
            Object o = Activator.CreateInstance(screen_type, arguments);
            manager.Focus.hidden_p = true;
            manager.AddScreen( (Screen) o);
            manager.FocusScreen( (Screen) o);
        }
    }
}
