using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using gigapede.GameItems;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace gigapede
{
	class World
	{
		protected List<GameItem> items = new List<GameItem>();
		private RectangleF bounds;


		public World(RectangleF worldBounds)
		{
			this.bounds = worldBounds;
		}



		public void Update(GameTime gameTime, UserInput inputState)
		{
			List<GameItem> itemsToBeAdded = new List<GameItem>();
			List<GameItem> itemsToBeRemoved = new List<GameItem>();

			foreach (GameItem item in items)
			{
				List<GameItem.GameItemAction> actions = item.Update(new InfoForItem(bounds, GetContacts(item), gameTime, inputState));
				UpdateItemList(actions, itemsToBeAdded, itemsToBeRemoved);
			}

			foreach (GameItem item in itemsToBeRemoved)
				RemoveItem(item);

			foreach (GameItem item in itemsToBeAdded)
				AddItem(item);
		}



		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (GameItem item in items)
				item.Draw(spriteBatch);
		}



		public void AddItem(GameItem item)
		{
			items.Add(item);
		}



		public void RemoveItem(GameItem item)
		{
			items.Remove(item);
		}



		public RectangleF getBounds()
		{
			return bounds;
		}



		private void UpdateItemList(List<GameItem.GameItemAction> actions, List<GameItem> itemsToBeAdded, List<GameItem> itemsToBeRemoved)
		{
			foreach (GameItem.GameItemAction itemAction in actions)
			{
				if (itemAction.action == GameItem.GameItemAction.Action.ADD_ITEM)
					itemsToBeAdded.Add(itemAction.item);

				if (itemAction.action == GameItem.GameItemAction.Action.REMOVE_ITEM)
					itemsToBeRemoved.Add(itemAction.item);
			}
		}



		private List<GameItem> GetContacts(GameItem item)
		{
			List<GameItem> contacts = new List<GameItem>();

			if (item.IsMovable())
			{
				foreach (GameItem otherItem in items)
				{
					if (otherItem != item && item.Intersects(otherItem))
						contacts.Add(otherItem);
				}
			}

			return contacts;
		}
	}
}
