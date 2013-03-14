using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Scorpion : JumpingGameItem
	{
		public static Texture2D texture;


		public Scorpion(PointF location) :
			base(location)
		{ }



		protected override void Jump(InfoForItem info)
		{
			PointF nextLoc = boundingBox.Location;
			Move(ref nextLoc);

			GameItem item = info.world.ItemAt(nextLoc, 1f);
			if (item != null && item.GetType() == typeof(Mushroom))
				((Mushroom)item).IsPoisoned = true;

			boundingBox.Location = nextLoc;
		}



		public override float GetSpeed()
		{
			return GameParameters.SCORPION_SPEED;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
