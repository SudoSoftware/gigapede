using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Shooter : GameItem
	{
		public static Texture2D texture;
		public static float minY;
		private int extraPowerUsesLeft = 0;

		private bool amAlive = true;
		private int livesLeft = GameParameters.MAX_LIVES;
		private GameItem diedTo;
		private int currentScore = 0;


		public Shooter(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (!amAlive)
				return actions;

			Move(info);

			foreach (GameItem contact in info.contacts)
			{
				Type type = contact.GetType();
				//if (type == typeof(Centipede) || type == typeof(Flea) || type == typeof(Scorpion) || type == typeof(Spider))
				//	Die(contact);
			}

			
			if (info.inputState.justPressed(UserInput.InputType.FIRE))
				SpawnRocket(ref actions);

			return actions;
		}



		private void Move(InfoForItem info)
		{
			float movementTheta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.SHOOTER_MOVEMENT_THETA;
			RectangleF newBounds = new RectangleF(boundingBox.Location, boundingBox.Size);

			if (info.inputState.onNow(UserInput.InputType.LEFT))
				newBounds.X -= movementTheta;
			if (info.inputState.onNow(UserInput.InputType.RIGHT))
				newBounds.X += movementTheta;

			if (!info.world.IsLegalLocation(newBounds) || IntersectsWithMushroom(newBounds, info.world))
				newBounds.X = boundingBox.X;

			if (info.inputState.onNow(UserInput.InputType.UP))
				newBounds.Y -= movementTheta;
			if (info.inputState.onNow(UserInput.InputType.DOWN))
				newBounds.Y += movementTheta;

			if (!info.world.IsLegalLocation(newBounds) || newBounds.Y < minY || IntersectsWithMushroom(newBounds, info.world))
				newBounds.Y = boundingBox.Y;

			boundingBox = newBounds;
		}



		private void SpawnRocket(ref List<GameItemAction> itemActions)
		{
			PointF rocketLocation = new PointF(boundingBox.X, boundingBox.Y - boundingBox.Height * 0.8f);
			bool rocketIsPoweredUp = false;

			if (extraPowerUsesLeft > 0)
			{
				rocketIsPoweredUp = true;
				extraPowerUsesLeft--;
			}
			else
				rocketLocation.X += boundingBox.Width / 3;

			itemActions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Rocket(rocketLocation, rocketIsPoweredUp)));
		}



		private bool IntersectsWithMushroom(RectangleF bounds, World world)
		{
			List<GameItem> mushrooms = world.GetAllItemsOfType(typeof(Mushroom));
			foreach (GameItem mushroom in mushrooms)
				if (bounds.IntersectsWith(mushroom.GetBounds()))
					return true;

			return false;
		}



		public void Powerup()
		{
			extraPowerUsesLeft = GameParameters.ROCKET_POWERUP_USES;
		}



		public bool IsPoweredUp()
		{
			return extraPowerUsesLeft > 0;
		}



		public void AddToScore(int additionalPoints, GameItem source, HeadsUpDisplay hud)
		{
			currentScore += additionalPoints;
			hud.IndicateAdditionalPoints(additionalPoints, source);
		}


		
		public void Die(GameItem itemDiedTo)
		{
			diedTo = itemDiedTo;
			livesLeft--;
			amAlive = false;
		}



		public void Respawn()
		{
			amAlive = true;
			diedTo = null;
		}



		public void GiveAdditionalLife()
		{
			livesLeft++;
		}



		public int GetLivesLeft()
		{
			return livesLeft;
		}


		
		public GameItem GetItemDiedTo()
		{
			return diedTo;
		}



		public int GetCurrentScore()
		{
			return currentScore;
		}



		public bool IsAlive()
		{
			return amAlive;
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
