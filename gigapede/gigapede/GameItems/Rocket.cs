using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Rocket : GameItem
	{
		public static Texture2D texture;

		public Rocket(PointF location) :
			base(location)
		{ }



		public Rocket(PointF location, SizeF size) :
			base(new RectangleF(location, size))
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			boundingBox.Y -= info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.ROCKET_SPEED;

			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (!info.world.IsLegalLocation(boundingBox)) //if rocket out of bounds, remove from world
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));

			foreach (GameItem item in info.contacts)
			{
				Type itemType = item.GetType();
				if (itemType.IsSubclassOf(typeof(DamageableGameItem)))
				{
					if (itemType == typeof(Mushroom))
						((Mushroom)item).Damage();
					else if (itemType == typeof(Centipede))
						((Centipede)item).KillOff();

					actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
					break; //makes it only destroy one item
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
