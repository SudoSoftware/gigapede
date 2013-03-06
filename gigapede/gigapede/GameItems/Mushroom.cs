using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace gigapede.GameItems
{
	class Mushroom: GameItem
	{
		private int health = 5;

		public List<GameItemAction> Update(World.InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			return actions;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, re
		}
	}
}
