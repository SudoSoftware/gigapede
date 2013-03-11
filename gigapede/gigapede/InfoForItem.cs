using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.GameItems;
using Microsoft.Xna.Framework;

namespace gigapede
{
	class InfoForItem
	{
		public Rectangle worldBounds;
		public List<GameItem> contacts;
		public GameTime gameTime;
		public UserInput inputState;

		public InfoForItem(Rectangle worldBounds, List<GameItem> contacts, GameTime gameTime, UserInput inputState)
		{
			this.worldBounds = worldBounds;
			this.contacts = contacts;
			this.gameTime = gameTime;
			this.inputState = inputState;
		}
	}
}
