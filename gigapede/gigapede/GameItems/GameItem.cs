﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	abstract class GameItem
	{
		protected RectangleF boundingBox = new RectangleF(0, 0, GameParameters.DEFAULT_ITEM_WIDTH, GameParameters.DEFAULT_ITEM_HEIGHT);

		abstract public List<GameItemAction> Update(InfoForItem info);
		abstract public Texture2D GetTexture();


		public GameItem(PointF origin)
		{
			boundingBox.Location = origin;
		}



		public GameItem(RectangleF bounds)
		{
			boundingBox = bounds;
		}


		
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				GetTexture(),
				new Microsoft.Xna.Framework.Rectangle((int)boundingBox.X, (int)boundingBox.Y, (int)boundingBox.Width, (int)boundingBox.Height),
				GetTextureRectangle(),
				Microsoft.Xna.Framework.Color.White
			);
		}



		public virtual bool Intersects(GameItem otherItem)
		{
			return boundingBox.IntersectsWith(otherItem.boundingBox);
		}



		public virtual bool IsMovable()
		{
			return false;
		}



		protected virtual Microsoft.Xna.Framework.Rectangle GetTextureRectangle()
		{
			return GetTexture().Bounds;
		}



		public PointF GetLocation()
		{
			return boundingBox.Location;
		}




		public class GameItemAction
		{
			public enum Action
			{
				ADD_ITEM, REMOVE_ITEM, REPLACE_ITEM
			}

			public GameItem item;
			public GameItem secondaryItem;
			public Action action;


			public GameItemAction(Action action, GameItem item)
			{
				this.action = action;
				this.item = item;
			}



			public GameItemAction(GameItem from, GameItem to):
				this(Action.REPLACE_ITEM, from)
			{
				this.secondaryItem = to;
			}
		}
	}
}
