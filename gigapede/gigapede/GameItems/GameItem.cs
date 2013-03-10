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
		protected Texture2D texture;
		protected Rectangle boundingBox;

		abstract public List<GameItemAction> Update(World.InfoForItem info);


		private GameItem(Texture2D texture, Rectangle boundingBox)
		{
			this.texture = texture;
			this.boundingBox = boundingBox;
		}



		public GameItem(Rectangle size)
		{
			boundingBox = size;
		}


		
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, boundingBox, Color.White);
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
