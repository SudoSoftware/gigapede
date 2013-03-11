using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace gigapede.GameItems
{
	class Mushroom: GameItem
	{
		public static Texture2D texture;
		private int health = 5;

		public Mushroom(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			return actions;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
