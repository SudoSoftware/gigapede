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
		public const float ROCKET_SPEED = 0.6f;
		public static Texture2D texture;

		public Rocket(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			boundingBox.Y -= info.gameTime.ElapsedGameTime.Milliseconds * ROCKET_SPEED;

			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (!boundingBox.IntersectsWith(info.worldBounds)) //if rocket out of bounds, remove from world
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));

			foreach (GameItem item in info.contacts)
			{
				if (item.GetType() == typeof(Mushroom))
				{
					actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
					((Mushroom)item).Damage();
				}
			}

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
