using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gigapede
{
	class World
	{
		private List<gigapede.GameItem.GameItem> items;

		public void Update(GameTime gameTime, UserInput inputState)
		{
			foreach (gigapede.GameItem.GameItem item in items)
			{
				List<gigapede.GameItem.GameItem.GameItemAction> actions = item.Update(gameTime, GetContacts(item), inputState);
			}
		}



		public void Draw()
		{
			foreach (gigapede.GameItem.GameItem item in items)
				item.Draw();
		}



		private gigapede.GameItem.GameItem GetContacts(gigapede.GameItem.GameItem item)
		{
			foreach (gigapede.GameItem.GameItem otherItem in items)
			{
				if (otherItem != item && item.Intersects(otherItem))
					return otherItem;
			}

			return null;
		}
	}
}
