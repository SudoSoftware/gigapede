using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace gigapede.GameItems
{
	abstract class GameItem
	{
		public const int DEFAULT_WIDTH = 50;
		public const int DEFAULT_HEIGHT = DEFAULT_WIDTH;

		protected RectangleF boundingBox = new RectangleF(0, 0, DEFAULT_WIDTH, DEFAULT_HEIGHT);

		abstract public List<GameItemAction> Update(InfoForItem info);
		abstract public Texture2D GetTexture();


		public GameItem(PointF origin)
		{
			boundingBox.Location = origin;
		}



		public GameItem(RectangleF bounds)
		{
			boundingBox = bounds;
			new RectangleF();

		}


		
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				GetTexture(),
				new Microsoft.Xna.Framework.Rectangle((int)boundingBox.X, (int)boundingBox.Y, (int)boundingBox.Width, (int)boundingBox.Height),
				Microsoft.Xna.Framework.Color.White
			);
		}



		public bool Intersects(GameItem otherItem)
		{
			return boundingBox.IntersectsWith(otherItem.boundingBox);
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
