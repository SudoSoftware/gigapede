﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Centipede : JumpingGameItem
	{
		public static Texture2D texture;
		private static MyRandom prng = new MyRandom();


		public Centipede(PointF location) :
			base(location)
		{ }



		protected override void Jump(InfoForItem info)
		{
			PointF nextLoc = boundingBox.Location;
			Move(ref nextLoc);

			if (!info.world.IsLegalLocation(new RectangleF(nextLoc, boundingBox.Size)) || info.world.TypeAt(nextLoc, typeof(Mushroom)))
			{
				nextLoc.X = boundingBox.X;
				nextLoc.Y += originalHeight;
				movingRight = !movingRight;

				if (info.world.TypeAt(nextLoc, typeof(Mushroom)))
					Move(ref nextLoc);
			}

			boundingBox.Location = nextLoc;
		}



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			info.world.getHUD().AddToScore(GameParameters.CENTIPEDE_POINTS, this);
			PossiblyGivePowerup(ref itemActions);
			itemActions.Add(new GameItemAction(GameItemAction.Action.REPLACE_ME, new Mushroom(boundingBox.Location)));
		}



		private void PossiblyGivePowerup(ref List<GameItemAction> itemActions)
		{
			if (prng.nextRange(0, 50) <= 1) //2% chance
				itemActions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Powerup(boundingBox.Location, Powerup.PowerupType.EXTRA_LIFE)));
		}



		public override float GetSpeed()
		{
			return GameParameters.CENTIPEDE_SPEED;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
