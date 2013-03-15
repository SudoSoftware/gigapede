using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Powerup : GameItem
	{
		public static Texture2D texture;


		public Powerup(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			boundingBox.Y -= info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.GRAVITY;

			List<GameItemAction> actions = new List<GameItemAction>();

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
