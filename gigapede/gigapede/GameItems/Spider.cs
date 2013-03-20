﻿using System;
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
		private PointF goalLocation;


		public Spider(PointF location) :
			base(location)
		{
			goalLocation = boundingBox.Location;
		}



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
			if (GetVectorTo(goalLocation).Length() < 5)
				ResetGoal(info);
			else
				MoveTowardsGoal(info);
		}



		private void EatMushrooms(List<GameItem> currentContacts, ref List<GameItemAction> actions)
		{
			RemoveObsoleteContacts(currentContacts);

			foreach (GameItem contact in currentContacts)
			{
				if (contact.GetType() == typeof(Mushroom))
				{
					Mushroom mushroomContact = (Mushroom)contact;
					if (!mContactsLastTime.Contains(mushroomContact))
					{ //it just now contacted the mushroom

						if (prng.nextRange(0, 10) <= 1) //10% of eating the mushroom it's touching
							actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, mushroomContact));
						mContactsLastTime.Add(mushroomContact);
					}
				}
			}
		}



		private void ResetGoal(InfoForItem info)
		{
			Shooter shooter = (Shooter)info.world.GetItemOfType(typeof(Shooter));
			Vector2 vToShooter = GetVectorTo(shooter.GetLocation());
			
			Vector2 perpToShooter = new Vector2(-vToShooter.Y, vToShooter.X);
			perpToShooter.Normalize();
			float magnitude = prng.nextRange(-GameParameters.SPIDER_ZIGZAG_COEFF, GameParameters.SPIDER_ZIGZAG_COEFF) * vToShooter.Length();
			Vector2 offsetVector = perpToShooter * magnitude;

			float percentage = 0.5f;// prng.nextRange(0.1f, 0.7f); // (float)prng.nextGaussian(0.5, 0.5);
			PointF ptOnLine = new PointF(vToShooter.X * percentage, vToShooter.Y * percentage);

			goalLocation.X = ptOnLine.X + offsetVector.X;
			goalLocation.Y = ptOnLine.Y + offsetVector.Y;
		}



		private void MoveTowardsGoal(InfoForItem info)
		{
			Vector2 vToGoal = GetVectorTo(goalLocation);
			vToGoal.Normalize();
			vToGoal *= info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.SPIDER_SPEED;

			boundingBox.X += vToGoal.X;
			boundingBox.Y += vToGoal.Y;
		}



		//if the Spider is no longer contacting any of the remembered Mushroms, forget them
		private void RemoveObsoleteContacts(List<GameItem> currentContacts)
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
