using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.Resources;

namespace gigapede
{
    class CustomTexturesButton : ToggleButton
    {
        ResourceManager manager;

        public CustomTexturesButton(ResourceManager manager)
            : base("Custom Textures", GameParameters.USING_CUSTOM_TEXTURES)
        {
            this.manager = manager;
        }

        public override void  RunAction()
        {
 	        base.RunAction();

            if (state)
            {
                manager.LoadResources("custom/");
            }
            else
            {
                manager.LoadResources("default/");
            }

			//display_text = "Custom Textures : " + state;
			GameParameters.USING_CUSTOM_TEXTURES = state;
        }
    }
}
