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
		private static MyRandom prng = new MyRandom();
		public bool IsPoisoned, isRespawning;


		public Mushroom(PointF location) :
			base(location)
		{ }



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			((Shooter)info.world.GetItemOfType(typeof(Shooter))).AddToScore(GameParameters.MUSHROOM_POINTS, this, info.world.getHUD());
			PossiblyGivePowerup(ref itemActions);
			base.Die(ref itemActions, info);
		}



		private void PossiblyGivePowerup(ref List<GameItemAction> itemActions)
		{
			if (prng.nextRange(0, 10) <= 1) //40 is a good value as its a 2.5% chance
				itemActions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Powerup(boundingBox.Location, Powerup.PowerupType.SHOOTER_POWERUP)));
		}



		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				GetTexture(),
				new Microsoft.Xna.Framework.Rectangle((int)boundingBox.X, (int)boundingBox.Y, (int)boundingBox.Width, (int)boundingBox.Height),
				GetTextureRectangle(),
				isRespawning ? Microsoft.Xna.Framework.Color.Blue : Microsoft.Xna.Framework.Color.White
			);
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
