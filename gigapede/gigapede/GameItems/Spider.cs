using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Spider : DamageableGameItem
	{
		public static Texture2D texture;
		private MyRandom prng = new MyRandom();


		public Spider(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (GetAliveness() <= 0)
				Die(ref actions, info);

			Mushroom contactingMushroom = GetContactingMushroom(info.contacts);
			if (contactingMushroom != null && prng.nextRange(0, 5) <= 1) //20% of eating the mushroom it's touching
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, contactingMushroom));

			return actions;
		}



		private Mushroom GetContactingMushroom(List<GameItem> contacts)
		{
			foreach (GameItem item in contacts)
				if (item.GetType() == typeof(Mushroom))
					return (Mushroom)item;
			return null;
		}



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			info.world.getHUD().AddToScore(GameParameters.SPIDER_MID_POINTS); //todo: implement distance judgement
			base.Die(ref itemActions, info);
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
