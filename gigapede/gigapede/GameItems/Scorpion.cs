using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Scorpion : JumpingGameItem
	{
		public static Texture2D texture;


		public Scorpion(PointF location, bool movingRight) :
			base(location)
		{
			this.movingRight = movingRight;
		}


		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (GetAliveness() <= 0)
				Die(ref actions, info);

			if (!info.world.IsLegalLocation(boundingBox))
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));

			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GetSpeed();
			movementTillJump -= theta;

			if (movementTillJump <= 0)
			{
				Jump(info);
				ResetJumpWait();
			}

			return actions;
		}



		protected override void Jump(InfoForItem info)
		{
			PointF nextLoc = boundingBox.Location;
			Move(ref nextLoc);

			GameItem item = info.world.ItemAt(nextLoc);
			if (item != null && item.GetType() == typeof(Mushroom))
				((Mushroom)item).IsPoisoned = true;

			boundingBox.Location = nextLoc;
		}



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			((Shooter)info.world.GetItemOfType(typeof(Shooter))).AddToScore(GameParameters.SCORPION_POINTS, this, info.world.getHUD());
			base.Die(ref itemActions, info);
		}



		public override float GetSpeed()
		{
			return GameParameters.SCORPION_SPEED;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
