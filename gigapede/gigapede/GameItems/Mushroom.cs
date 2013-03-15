using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Mushroom : DamageableGameItem
	{
		public static Texture2D normalTexture;
		public static Texture2D poisonedTexture;
		private MyRandom prng = new MyRandom();
		public bool IsPoisoned;


		public Mushroom(PointF location) :
			base(location)
		{ }



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			info.world.getHUD().AddToScore(GameParameters.MUSHROOM_POINTS, this);
			PossiblyGivePowerup(ref itemActions);
			base.Die(ref itemActions, info);
		}



		private void PossiblyGivePowerup(ref List<GameItemAction> itemActions)
		{
			if (prng.nextRange(0, 10) <= 1) //10% chance
				itemActions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Powerup(boundingBox.Location, Powerup.PowerupType.ROCKET_BOOST)));
		}



		public override Texture2D GetTexture()
		{
			if (IsPoisoned)
				return poisonedTexture;
			else
				return normalTexture;
		}
	}
}
