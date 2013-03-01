using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gigapede.GameItem
{
	class GameItem
	{
		private Rectangle boundingBox;

		public GameItem(Rectangle size)
		{
			boundingBox = size;
		}

		public void update()
		{
		}

		public void draw()
		{
		}

		public bool Intersects(GameItem otherItem)
		{
			return boundingBox.Intersects(otherItem.boundingBox);
		}
	}
}
