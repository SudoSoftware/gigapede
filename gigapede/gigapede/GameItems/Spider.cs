using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;
using Microsoft.Xna.Framework;

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

			Vector2 vector = GetVectorToShooter(info);
			vector.Normalize();
			vector *= info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.SPIDER_SPEED;

			boundingBox.X += vector.X;
			boundingBox.Y += vector.Y;

			//Mushroom contactingMushroom = GetContactingMushroom(info.contacts);
			//if (contactingMushroom != null && prng.nextRange(0, 5) <= 1) //20% of eating the mushroom it's touching
			//	actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, contactingMushroom));

			return actions;
		}



		private Vector2 GetVectorToShooter(InfoForItem info)
		{
			Shooter shooter = (Shooter)info.world.GetItemOfType(typeof(Shooter));
			PointF shooterLoc = shooter.GetLocation();

			Vector2 vector = new Vector2(shooterLoc.X, shooterLoc.Y);
			vector.X -= boundingBox.X;
			vector.Y -= boundingBox.Y;

			return vector;
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
			((Shooter)info.world.GetItemOfType(typeof(Shooter))).AddToScore(GameParameters.SPIDER_MID_POINTS, this, info.world.getHUD()); //todo: implement distance judgement
			base.Die(ref itemActions, info);
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
