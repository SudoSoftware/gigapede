using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace gigapede.GameItems
{
	class Mushroom: GameItem
	{
		private const int MAX_HEALTH = 5;
		public static Texture2D texture;
		private int currentHealth = MAX_HEALTH;

		public Mushroom(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			foreach (GameItem item in info.contacts)
			{
				//if (item.GetType() == typeof(Rocket))
				{
					actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, item));
					currentHealth--;
				}
			}

			if (currentHealth <= 0)
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
			
			return actions;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
