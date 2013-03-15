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
	class Shooter : GameItem
	{
		public static Texture2D texture;
		public static float minY;
		private int extraPowerUsesLeft = 0;


		public Shooter(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			HandleMovement(info);
			
			List<GameItemAction> actions = new List<GameItemAction>();
			if (info.inputState.justPressed(UserInput.InputType.FIRE))
				SpawnRocket(ref actions);
			return actions;
		}



		private void HandleMovement(InfoForItem info)
		{
			RectangleF newBounds = GetNewLocation(info);
			if (info.world.IsLegalLocation(newBounds) && newBounds.Y >= minY)
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



		private RectangleF GetNewLocation(InfoForItem info)
		{
			float movementTheta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.SHOOTER_MOVEMENT_THETA;
			RectangleF newBounds = new RectangleF(boundingBox.Location, boundingBox.Size);

			if (info.inputState.onNow(UserInput.InputType.LEFT))
				newBounds.X -= movementTheta;
			if (info.inputState.onNow(UserInput.InputType.RIGHT))
				newBounds.X += movementTheta;
			if (info.inputState.onNow(UserInput.InputType.UP))
				newBounds.Y -= movementTheta;
			if (info.inputState.onNow(UserInput.InputType.DOWN))
				newBounds.Y += movementTheta;

			return newBounds;
		}



		public void Powerup()
		{
			extraPowerUsesLeft = GameParameters.ROCKET_POWERUP_USES;
		}



		public bool IsPoweredUp()
		{
			return extraPowerUsesLeft > 0;
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
