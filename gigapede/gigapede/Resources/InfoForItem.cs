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
		public List<GameItem> contacts;
		public GameTime gameTime;
		public UserInput inputState;
		public World world;


		public InfoForItem(World world, List<GameItem> contacts, GameTime gameTime, UserInput inputState)
		{
			this.world = world;
			this.contacts = contacts;
			this.gameTime = gameTime;
			this.inputState = inputState;
		}
	}
}
