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
		private List<RectangleF> body = new List<RectangleF>();
		private bool movingRight = true;
		private float movementTillJump;


		public Centipede(PointF location) :
			base(location)
		{
			//preciseOrigin = boundingBox.Location;
			ResetJumpWait();
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.CENTIPEDE_SPEED;
			movementTillJump -= theta;

			if (movementTillJump < 0)
			{
				Jump(info);
				ResetJumpWait();
			}

			return base.Update(info);
		}



		private void Jump(InfoForItem info)
		{
			PointF nextLoc = new PointF(boundingBox.Location.X, boundingBox.Location.Y);
			Move(ref nextLoc);


			if (!info.world.IsLegalLocation(new RectangleF(nextLoc, boundingBox.Size)) || info.world.ItemAt(nextLoc, 1f) != null)
			{
				nextLoc.X = boundingBox.Location.X;
				nextLoc.Y += boundingBox.Height;
				movingRight = !movingRight;

				if (info.world.ItemAt(nextLoc, 1f) != null)
					Move(ref nextLoc);
			}

			boundingBox.Location = nextLoc;
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

			/*
			PointF nextLoc = new PointF(preciseOrigin.X, preciseOrigin.Y);
			
			if (!info.world.IsLegalLocation(new RectangleF(nextLoc, boundingBox.Size)) || IsContactingMushroom(info))
			{
				nextLoc.Y += boundingBox.Height;
				movingRight = !movingRight;

				//GameItem obj = info.world.ItemAt(nextLoc, 1f);
				//if (obj != null && obj.GetType() == typeof(Mushroom))
				if (info.world.GetContacts(
				{
					if (movingRight)
						nextLoc.X += boundingBox.Width + 3;
					else
						nextLoc.X -= boundingBox.Width + 3;
				}
			}

			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.CENTIPEDE_SPEED;
			if (movingRight)
				nextLoc.X += theta;
			else
				nextLoc.X -= theta;

			preciseOrigin = nextLoc;
			boundingBox.Location = preciseOrigin; //temporary, the Centipede should eventually stutter
			
			return base.Update(info);
		}



		private bool IsContactingMushroom(InfoForItem info)
		{
			foreach (GameItem item in info.contacts)
				if (item.GetType() == typeof(Mushroom))
					return true;
			return false;
		}



		private bool BounceOffWalls(InfoForItem info)
		{
			return !info.world.IsLegalLocation(new RectangleF(preciseOrigin, boundingBox.Size));
		}



		private void UpdateLocation(InfoForItem info)
		{
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.CENTIPEDE_SPEED;
			if (movingRight)
				preciseOrigin.X += theta;
			else
				preciseOrigin.X -= theta;

			boundingBox.X = preciseOrigin.X; //temporary, the Centipede should eventually stutter
			boundingBox.Y = preciseOrigin.Y;
		}*/



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
