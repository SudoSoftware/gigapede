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
	class Centipede : JumpingGameItem
	{
		public static Texture2D texture;


		public Centipede(PointF location) :
			base(location)
		{ }



		protected override void Die(ref List<GameItemAction> itemActions)
		{
			itemActions.Add(new GameItemAction(this, new Mushroom(boundingBox.Location))); //replace "this" with a new Mushroom
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
