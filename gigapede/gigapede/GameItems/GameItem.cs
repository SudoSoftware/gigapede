using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gigapede.GameItems
{
	abstract class GameItem
	{
		protected Rectangle boundingBox;

		abstract public List<GameItemAction> Update(World.InfoForItem info);
		abstract public Texture2D GetTexture();


		public GameItem(Rectangle bounds)
		{
			boundingBox = bounds;
		}


		
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(GetTexture(), boundingBox, Color.White);
		}



		public bool Intersects(GameItem otherItem)
		{
			return boundingBox.Intersects(otherItem.boundingBox);
		}



		public virtual bool IsMovable()
		{
			return false;
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
