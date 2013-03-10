using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using gigapede.GameItems;
using Microsoft.Xna.Framework.Graphics;

namespace gigapede
{
	class World
	{
		private List<GameItem> items = new List<GameItem>();
		private Rectangle worldBounds;


		public World(Rectangle worldBounds)
		{
			this.worldBounds = worldBounds;
		}



		public void Update(GameTime gameTime, UserInput inputState)
		{
			List<GameItem> itemsToBeAdded = new List<GameItem>();
			List<GameItem> itemsToBeRemoved = new List<GameItem>();

			foreach (GameItem item in items)
			{
				List<GameItem.GameItemAction> actions = item.Update(new InfoForItem(GetContacts(item), gameTime, inputState));
				UpdateItemList(actions, itemsToBeAdded, itemsToBeRemoved);
			}

			foreach (GameItem removeMe in itemsToBeRemoved)
				items.Remove(removeMe);

			foreach (GameItem addMe in itemsToBeAdded)
				items.Add(addMe);
		}



		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (GameItem item in items)
				item.Draw(spriteBatch);
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



		public class InfoForItem
		{
			public List<GameItem> contacts;
			public GameTime gameTime;
			public UserInput inputState;


			public InfoForItem(List<GameItem> contacts, GameTime gameTime, UserInput inputState)
			{
				this.contacts = contacts;
				this.gameTime = gameTime;
				this.inputState = inputState;
			}



			public Rectangle worldBounds
			{
				get
				{
					return worldBounds;
				}
			}
		}
	}
}
