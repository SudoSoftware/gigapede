using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace gigapede.GameItems
{
	class Shooter : GameItem
	{
		public const float MOVEMENT_SPEED = 0.25f;
		public static Texture2D texture;


		public Shooter(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			HandleMovement(info);
			
			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (info.inputState.justPressed(UserInput.InputType.FIRE))
				actions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Rocket(boundingBox.Location)));

			return actions;
		}



		private void HandleMovement(InfoForItem info)
		{
			RectangleF newBounds = GetNewLocation(info);
			if (IsLegalLocation(newBounds, info.worldBounds))
				boundingBox = newBounds;
		}



		private bool IsLegalLocation(RectangleF proposedBounds, RectangleF worldBoundingBox)
		{
			RectangleF intersection = new RectangleF(proposedBounds.Location, proposedBounds.Size);
			intersection.Intersect(worldBoundingBox);
			return intersection.Equals(proposedBounds) && proposedBounds.Y >= worldBoundingBox.Height * 0.7 ;
		}



		private RectangleF GetNewLocation(InfoForItem info)
		{
			float movementTheta = info.gameTime.ElapsedGameTime.Milliseconds * MOVEMENT_SPEED;
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
