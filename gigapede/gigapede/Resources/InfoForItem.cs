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
			return intersection.Equals(proposedBounds);
		}
	}
}
