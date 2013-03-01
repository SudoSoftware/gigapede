using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using gigapede.GameItems;

namespace gigapede
{
	class World
	{
		private List<GameItem> items;

		public void Update(GameTime gameTime, UserInput inputState)
		{
			foreach (GameItem item in items)
			{
				List<GameItem.GameItemAction> actions = item.Update(gameTime, GetContacts(item), inputState);
			}
		}



		public void Draw()
		{
			foreach (GameItem item in items)
				item.Draw();
		}



		private GameItem GetContacts(GameItem item)
		{
			foreach (GameItem otherItem in items)
			{
				if (otherItem != item && item.Intersects(otherItem))
					return otherItem;
			}

			return null;
		}
	}
}
