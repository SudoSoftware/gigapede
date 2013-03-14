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

			for (int j = 0; j < items.Count; j++)
			{
				List<GameItem.GameItemAction> actions = items[j].Update(new InfoForItem(this, GetContacts(items[j]), gameTime, inputState));
				UpdateItemList(actions, itemsToBeAdded, itemsToBeRemoved);
				CheckAndPerformImmediateReplacements(actions);
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



		public GameItem ItemAt(PointF point, float locationTolerance)
		{
			foreach (GameItem item in items)
				if (Math.Abs(point.X - item.GetLocation().X) <= locationTolerance && Math.Abs(point.Y - item.GetLocation().Y) <= locationTolerance)
					return item;

			return null;
		}



		public RectangleF getBounds()
		{
			return bounds;
		}



		public List<GameItem> GetContacts(GameItem item)
		{
			List<GameItem> contacts = new List<GameItem>();

			if (item.IsMovable())
				foreach (GameItem otherItem in items)
					if (otherItem != item && item.Intersects(otherItem))
						contacts.Add(otherItem);

			return contacts;
		}



		public bool IsLegalLocation(RectangleF proposedBounds)
		{
			RectangleF intersection = new RectangleF(proposedBounds.Location, proposedBounds.Size);
			intersection.Intersect(bounds);
			
			if (RectRoughEquals(intersection, proposedBounds, 1f))
				return true;
			else if (intersection.Width > 0 && intersection.Width <= 1 || intersection.Height > 0 && intersection.Height <= 1) //tolerate a 1-pixel sliver
				return true;
			else
				return false;
		}



		private bool RectRoughEquals(RectangleF a, RectangleF b, float tolerance)
		{
			return Math.Abs(a.X - b.X) <= tolerance && Math.Abs(a.Y - b.Y) <= tolerance
				&& Math.Abs(a.Width - b.Width) <= tolerance && Math.Abs(a.Height - b.Height) <= tolerance;
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



		private void CheckAndPerformImmediateReplacements(List<GameItem.GameItemAction> actions)
		{
			foreach (GameItem.GameItemAction itemAction in actions) //check for items that need immediate replacement
			{
				if (itemAction.action == GameItem.GameItemAction.Action.REPLACE_ITEM)
				{
					int index = items.IndexOf(itemAction.item);
					items[index] = itemAction.secondaryItem;
				}
			}
		}
	}
}
