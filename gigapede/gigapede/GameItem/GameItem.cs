using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gigapede.GameItem
{
	class GameItem
	{
		private Rectangle boundingBox;

		private GameItem()
		{ }


		public GameItem(Rectangle size)
		{
			boundingBox = size;
		}



		public List<GameItemAction> Update(GameTime gameTime, GameItem itemTouching, UserInput inputState)
		{
			return null;
		}



		public void Draw()
		{
		}



		public bool Intersects(GameItem otherItem)
		{
			return boundingBox.Intersects(otherItem.boundingBox);
		}



		public class GameItemAction
		{
			public enum Action
			{
				ADD_ITEM, REMOVE_ITEM
			}


			public GameItem item;
			public Action action;

			public GameItemAction(Action action, GameItem item)
			{
				this.action = action;
				this.item = item;
			}
		}
	}
}
