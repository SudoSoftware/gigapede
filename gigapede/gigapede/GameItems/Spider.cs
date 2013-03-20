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
		private List<Mushroom> mContactsLastTime = new List<Mushroom>();


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

			return actions;
		}



		private void Move(InfoForItem info)
		{
			Vector2 vector = GetVectorToShooter(info);
			vector.Normalize();
			vector *= info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.SPIDER_SPEED;

			boundingBox.X += vector.X;
			boundingBox.Y += vector.Y;
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



		private void EatMushrooms(List<GameItem> currentContacts, ref List<GameItemAction> actions)
		{
			removeObsoleteContacts(currentContacts);

			foreach (GameItem contact in currentContacts)
			{
				if (contact.GetType() == typeof(Mushroom))
				{
					Mushroom mushroomContact = (Mushroom)contact;
					if (!mContactsLastTime.Contains(mushroomContact))
					{ //it just now contacted the mushroom

						if (prng.nextRange(0, 5) <= 1) //20% of eating the mushroom it's touching
							actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, mushroomContact));
						mContactsLastTime.Add(mushroomContact);
					}
				}
			}
		}



		//if the Spider is no longer contacting any of the remembered Mushroms, forget them
		private void removeObsoleteContacts(List<GameItem> currentContacts)
		{
			List<Mushroom> obsoleteContacts = new List<Mushroom>();

			foreach (Mushroom mushroom in mContactsLastTime)
				if (!currentContacts.Contains(mushroom))
					obsoleteContacts.Add(mushroom);

			foreach (Mushroom mushroom in obsoleteContacts)
				mContactsLastTime.Remove(mushroom);
		}



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			((Shooter)info.world.GetItemOfType(typeof(Shooter))).AddToScore(GameParameters.SPIDER_MID_POINTS, this, info.world.getHUD()); //todo: implement distance judgement
			base.Die(ref itemActions, info);
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
