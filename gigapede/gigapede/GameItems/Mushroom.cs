using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace gigapede.GameItems
{
	class Mushroom: GameItem
	{
		private int health = 5;

		public Mushroom(Point location) :
			base(new Rectangle(location.X, location.Y, 0, 0))
		{
			boundingBox.Width = texture.Width;
			boundingBox.Height = texture.Height;
		}



		public override List<GameItemAction> Update(World.InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			return actions;
		}
	}
}
