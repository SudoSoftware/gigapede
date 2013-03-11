using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace gigapede.GameItems
{
	class Centipede : DamageableGameItem
	{
		public const float CENTIPEDE_SPEED = 0.3f;

		public static Texture2D texture;
		//private List<RectangleF> positions;
		private PointF preciseOrigin;
		private bool movingRight = true;



		public Centipede(PointF location) :
			base(location)
		{
			preciseOrigin = boundingBox.Location;
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			BounceOffMushrooms(info);
			BounceOffWalls(info);
			UpdateLocation(info);

			return base.Update(info);
		}



		private void BounceOffMushrooms(InfoForItem info)
		{
			foreach (GameItem item in info.contacts)
			{
				if (item.GetType() == typeof(Mushroom))
				{
					preciseOrigin.Y += boundingBox.Width;
					movingRight = !movingRight; //invert
					break;
				}
			}
		}



		private void BounceOffWalls(InfoForItem info)
		{
			if (!info.IsLegalLocation(new RectangleF(preciseOrigin, boundingBox.Size)))
			{
				preciseOrigin.Y += boundingBox.Height;
				movingRight = !movingRight; //invert
			}
		}



		private void UpdateLocation(InfoForItem info)
		{
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * CENTIPEDE_SPEED;
			if (movingRight)
				preciseOrigin.X += theta;
			else
				preciseOrigin.X -= theta;

			boundingBox.X = preciseOrigin.X; //temporary, the Centipede should eventually stutter
			boundingBox.Y = preciseOrigin.Y;
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
