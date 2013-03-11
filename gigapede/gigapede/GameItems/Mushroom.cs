using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace gigapede.GameItems
{
	class Mushroom : DamageableGameItem
	{
		public static Texture2D texture;
		private RectangleF backupBounds;


		public Mushroom(PointF location) :
			base(location)
		{ }



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
