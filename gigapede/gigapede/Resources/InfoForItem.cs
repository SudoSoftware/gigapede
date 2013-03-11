using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.GameItems;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace gigapede
{
	class InfoForItem
	{
		public RectangleF worldBounds;
		public List<GameItem> contacts;
		public GameTime gameTime;
		public UserInput inputState;


		public InfoForItem(RectangleF worldBounds, List<GameItem> contacts, GameTime gameTime, UserInput inputState)
		{
			this.worldBounds = worldBounds;
			this.contacts = contacts;
			this.gameTime = gameTime;
			this.inputState = inputState;
		}



		public bool IsLegalLocation(RectangleF proposedBounds)
		{
			RectangleF intersection = new RectangleF(proposedBounds.Location, proposedBounds.Size);
			intersection.Intersect(worldBounds);

			if (RectRoughEquals(intersection, proposedBounds, 0.0001f))
				return true;
			else if (intersection.Width <= 1 || intersection.Height <= 1) //tolerate a 1-pixel sliver
				return true;
			else
				return false;
		}



		private bool RectRoughEquals(RectangleF a, RectangleF b, float tolerance)
		{
			return Math.Abs(a.X - b.X) <= tolerance && Math.Abs(a.Y - b.Y) <= tolerance
				&& Math.Abs(a.Width - b.Width) <= tolerance && Math.Abs(a.Height - b.Height) <= tolerance;
		}
	}
}
