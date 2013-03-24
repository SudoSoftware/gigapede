using System;
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
		protected List<GameItem> contactsLastTime = new List<GameItem>();

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



		protected void SyncLastContacts(List<GameItem> currentContacts)
		{
			List<GameItem> obsoleteContacts = new List<GameItem>();
			foreach (GameItem contact in contactsLastTime)
				if (!currentContacts.Contains(contact))
					obsoleteContacts.Add(contact);

			foreach (GameItem contact in obsoleteContacts)
				contactsLastTime.Remove(contact);

			foreach (GameItem contact in currentContacts)
				if (!contactsLastTime.Contains(contact))
					contactsLastTime.Add(contact);
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



		public RectangleF GetBounds()
		{
			return boundingBox;
		}

		

		public class GameItemAction
		{
			public enum Action
			{
				ADD_ITEM, REMOVE_ITEM, REPLACE_ME
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
