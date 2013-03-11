using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gigapede.GameItems
{
	class Centipede : GameItem
	{
		public static Texture2D texture;
		private List<Rectangle> positions;


		public Centipede(Point location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
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
