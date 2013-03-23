using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
    class ToggleButton : MenuButton
    {
        protected bool state;
        protected String option_text;

        public ToggleButton(String option_text, bool init_state) 
            : base(option_text + " : " + init_state.ToString())
        {
            state = init_state;
            this.option_text = option_text;
        }

        public override void RunAction()
        {
            base.RunAction();

            state = !state;
            display_text = option_text + " : " + state.ToString();
        }
    }
}
