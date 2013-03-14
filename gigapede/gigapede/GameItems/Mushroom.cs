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
		public static Texture2D normalTexture;
		public static Texture2D poisonedTexture;
		public bool IsPoisoned;


		public Mushroom(PointF location) :
			base(location)
		{ }



		public override Texture2D GetTexture()
		{
			if (IsPoisoned)
				return poisonedTexture;
			else
				return normalTexture;
		}
	}
}
