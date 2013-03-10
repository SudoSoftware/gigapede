using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace gigapede.GameItems
{
	class Centipede : GameItem
	{
		private List<Rectangle> positions;


		public Centipede(Point location) :
			base(new Rectangle(location.X, location.Y, 0, 0))
		{
			boundingBox.Width = texture.Width;
			boundingBox.Height = texture.Height;
		}



		public override List<GameItemAction> Update(World.InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			return actions;
		}



		public override bool IsMovable()
		{
			return true;
		}
	}
}
