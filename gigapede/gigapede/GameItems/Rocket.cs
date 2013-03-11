using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace gigapede.GameItems
{
	class Rocket : GameItem
	{
		public const float ROCKET_SPEED = 0.5f;
		public static Texture2D texture;

		public Rocket(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (!boundingBox.IntersectsWith(info.worldBounds)) //if rocket out of bounds, remove from world
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));

			boundingBox.Y -= info.gameTime.ElapsedGameTime.Milliseconds * ROCKET_SPEED;

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
