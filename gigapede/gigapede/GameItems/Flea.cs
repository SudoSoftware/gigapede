using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Flea : JumpingGameItem
	{
		public static Texture2D texture;


		public Flea(PointF location) :
			base(location)
		{ }



		protected override void Jump(InfoForItem info)
		{

		}


		
		public override float GetSpeed()
		{
			return GameParameters.FLEA_SPEED;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
