using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Scorpion : DamageableGameItem
	{
		public static Texture2D texture;


		public Scorpion(PointF location) :
			base(location)
		{ }



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
