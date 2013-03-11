using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gigapede.GameItems
{
	class Shooter : GameItem
	{
		public static Texture2D texture;


		public Shooter(Point location) :
			base(new Rectangle(location.X, location.Y, 0, 0))
		{
			boundingBox.Width = texture.Width;
			boundingBox.Height = texture.Height;
		}



		public override List<GameItemAction> Update(World.InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (info.inputState.onNow(UserInput.InputType.LEFT))
				boundingBox.X--;
			if (info.inputState.onNow(UserInput.InputType.RIGHT))
				boundingBox.X++;
			if (info.inputState.onNow(UserInput.InputType.UP))
				boundingBox.Y--;
			if (info.inputState.onNow(UserInput.InputType.DOWN))
				boundingBox.Y++;

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
