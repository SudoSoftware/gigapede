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
	class Centipede : DamageableGameItem
	{
		public static Texture2D texture;
		private LinkedList<RectangleF> body = new LinkedList<RectangleF>();
		private bool movingRight = true;
		private float movementTillJump;


		public Centipede(PointF location) :
			base(location)
		{
			ResetJumpWait();

			RectangleF currentSegment = boundingBox;
			for (int j = 0; j < 10; j++)
			{
				body.AddLast(currentSegment);
				currentSegment = new RectangleF(new PointF(currentSegment.X - boundingBox.Width, currentSegment.Y), currentSegment.Size);
			}
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.CENTIPEDE_SPEED;
			movementTillJump -= theta;

			if (movementTillJump <= 0)
			{
				Jump(info);
				ResetJumpWait();
			}

			return base.Update(info);
		}



		private void Jump(InfoForItem info)
		{
			PointF headLocation = body.First.Value.Location;
			
			PointF nextLoc = headLocation;
			Move(ref nextLoc);

			if (!info.world.IsLegalLocation(new RectangleF(nextLoc, boundingBox.Size)) || info.world.ItemAt(nextLoc, 1f) != null)
			{
				nextLoc.X = headLocation.X;
				nextLoc.Y += boundingBox.Height;
				movingRight = !movingRight;

				if (info.world.ItemAt(nextLoc, 1f) != null)
					Move(ref nextLoc);
			}

			boundingBox.Location = nextLoc;

			body.RemoveLast();
			body.AddFirst(new RectangleF(nextLoc, boundingBox.Size));
		}



		private void Move(ref PointF pt)
		{
			if (movingRight)
				pt.X += boundingBox.Width;
			else
				pt.X -= boundingBox.Width;
		}



		private void ResetJumpWait()
		{
			movementTillJump = boundingBox.Width;
		}



		/*public virtual bool Intersects(GameItem otherItem)
		{
			return boundingBox.IntersectsWith(otherItem.boundingBox);
		}*/



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
