using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gigapede.GameItems
{
	class Rocket : GameItem
	{
		public static Texture2D texture;


		public Rocket(Point location) :
			base(new Rectangle(location.X, location.Y, 0, 0))
		{
			boundingBox.Width = texture.Width;
			boundingBox.Height = texture.Height;
		}



		public override List<GameItemAction> Update(World.InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			
			//if (!boundingBox.Intersects(info.worldBounds)) //if rocket out of bounds, remove from world
			//	actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));

			boundingBox.Y -= 1;

			return actions;
		}


		public override bool IsMovable()
		{
			return true;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
