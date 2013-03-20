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
		public static Texture2D primaryTexture;
		public static Texture2D secondaryTexture;
		private bool IsPoweredUp;


		public Rocket(PointF location, bool poweredUp) :
			base(location)
		{
			if (!poweredUp)
				boundingBox.Width = GameParameters.DEFAULT_ITEM_WIDTH / 3;
			IsPoweredUp = poweredUp;
		}



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
					DamageableGameItem damageableItem = (DamageableGameItem)item;

					if (itemType == typeof(Centipede))
						damageableItem.KillOff();
					else
					{
						if (IsPoweredUp)
							damageableItem.KillOff();
						else
							damageableItem.Damage();
					}

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
			if (IsPoweredUp)
				return secondaryTexture;
			else
				return primaryTexture;
		}
	}
}
