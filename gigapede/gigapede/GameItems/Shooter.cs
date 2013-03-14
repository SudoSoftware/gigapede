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


		public Shooter(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			HandleMovement(info);
			
			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (info.inputState.justPressed(UserInput.InputType.FIRE))
			{
				PointF rocketLocation = new PointF(boundingBox.X + boundingBox.Width / 3, boundingBox.Y - boundingBox.Height * 0.8f);
				SizeF rocketSize = new SizeF(GameParameters.DEFAULT_ITEM_WIDTH / 3, GameParameters.DEFAULT_ITEM_HEIGHT);
				actions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Rocket(rocketLocation, rocketSize)));
			}

			return actions;
		}



		private void HandleMovement(InfoForItem info)
		{
			RectangleF newBounds = GetNewLocation(info);
			if (info.world.IsLegalLocation(newBounds) && newBounds.Y >= minY)
				boundingBox = newBounds;
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
