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
		public const float MOVEMENT_SPEED = 0.1f;
		public static Texture2D texture;


		public Shooter(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			float movementTheta = info.gameTime.ElapsedGameTime.Milliseconds * MOVEMENT_SPEED;

			if (info.inputState.onNow(UserInput.InputType.LEFT))
				boundingBox.X -= movementTheta;
			if (info.inputState.onNow(UserInput.InputType.RIGHT))
				boundingBox.X += movementTheta;
			if (info.inputState.onNow(UserInput.InputType.UP))
				boundingBox.Y -= movementTheta;
			if (info.inputState.onNow(UserInput.InputType.DOWN))
				boundingBox.Y += movementTheta;

			if (info.inputState.justPressed(UserInput.InputType.FIRE))
				actions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Rocket(boundingBox.Location)));

			return actions;
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
