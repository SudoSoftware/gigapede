using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gigapede.GameItems
{
	class Rocket : GameItem
	{
		public Rocket(Point location) :
			base(new Rectangle(location.X, location.Y, 10, 40))
		{ }
	}
}
