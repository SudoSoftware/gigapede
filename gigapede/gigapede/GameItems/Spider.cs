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
		private PointF goalLocation;


		public Spider(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (GetAliveness() <= 0)
				Die(ref actions, info);

			Move(info);
			EatMushrooms(info.contacts, ref actions);
			SyncLastContacts(info.contacts);

			return actions;
		}



		private void Move(InfoForItem info)
		{
			if (GetVectorTo(goalLocation).Length() < 5)
				ResetGoalLocation(info);
			else
				MoveTowardsGoal(info);
		}



		private void ResetGoalLocation(InfoForItem info)
		{
			Shooter shooter = (Shooter)info.world.GetItemOfType(typeof(Shooter));
			PointF shooterLoc = shooter.GetLocation();

			PointF locAboveShooter = new PointF(shooterLoc.X, Shooter.minY - GameParameters.DEFAULT_ITEM_HEIGHT);
			Vector2 offsetVector = prng.nextCircleVector() * (float)prng.nextGaussian(GameParameters.DEFAULT_ITEM_HEIGHT * 2, 3);

			goalLocation.X = locAboveShooter.X + offsetVector.X;
			goalLocation.Y = locAboveShooter.Y + offsetVector.Y;
		}



		private void MoveTowardsGoal(InfoForItem info)
		{
			Vector2 vToGoal = GetVectorTo(goalLocation);
			vToGoal.Normalize();
			vToGoal *= (float)info.gameTime.ElapsedGameTime.TotalMilliseconds * GameParameters.SPIDER_SPEED;

			boundingBox.X += vToGoal.X;
			boundingBox.Y += vToGoal.Y;
		}



		private void EatMushrooms(List<GameItem> currentContacts, ref List<GameItemAction> actions)
		{
			foreach (GameItem contact in currentContacts)
				if (contact.GetType() == typeof(Mushroom)
					&& !contactsLastTime.Contains(contact) && prng.nextRange(0, 10) <= 1) //10% of eating the mushroom it just touched
						actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, contact));
		}



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			((Shooter)info.world.GetItemOfType(typeof(Shooter))).AddToScore(GameParameters.SPIDER_MID_POINTS, this, info.world.getHUD()); //todo: implement distance judgement
			base.Die(ref itemActions, info);
		}



		private Vector2 GetVectorTo(PointF location)
		{
			Vector2 vector = new Vector2(location.X, location.Y);
			vector.X -= boundingBox.X;
			vector.Y -= boundingBox.Y;
			return vector;
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
