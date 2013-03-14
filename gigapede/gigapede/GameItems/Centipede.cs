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
		//private LinkedList<RectangleF> body = new LinkedList<RectangleF>();
		private bool movingRight = true;
		private float movementTillJump;


		public Centipede(PointF location) :
			base(location)
		{
			ResetJumpWait();
			/*
			RectangleF currentSegment = boundingBox;
			for (int j = 0; j < 10; j++)
			{
				body.AddLast(currentSegment);
				currentSegment = new RectangleF(new PointF(currentSegment.X - boundingBox.Width, currentSegment.Y), currentSegment.Size);
			}*/
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (GetAliveness() <= 0)
				actions.Add(new GameItemAction(this, new Mushroom(boundingBox.Location))); //replace "this" with a new Mushroom
			else
				HandleMovement(info);

			return actions;
		}



		private void HandleMovement(InfoForItem info)
		{
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GameParameters.CENTIPEDE_SPEED;
			movementTillJump -= theta;

			if (movementTillJump <= 0 && CanJump(info))
			{
				Jump(info);
				ResetJumpWait();
			}
		}



		private void Jump(InfoForItem info)
		{
			//PointF headLocation = ; 
			
			PointF nextLoc = boundingBox.Location;//body.First.Value.Location;
			Move(ref nextLoc);

			if (!info.world.IsLegalLocation(new RectangleF(nextLoc, boundingBox.Size)) || info.world.TypeAt(nextLoc, 1f, typeof(Mushroom)))
			{
				nextLoc.X = boundingBox.X;
				nextLoc.Y += originalHeight;
				movingRight = !movingRight;

				if (info.world.TypeAt(nextLoc, 1f, typeof(Mushroom)))
					Move(ref nextLoc);
			}

			boundingBox.Location = nextLoc;

			//body.RemoveLast();
			//body.AddFirst(new RectangleF(nextLoc, boundingBox.Size));
		}



		private void Move(ref PointF pt)
		{
			if (movingRight)
				pt.X += boundingBox.Width;
			else
				pt.X -= boundingBox.Width;
		}



		private bool CanJump(InfoForItem info)
		{
			PointF nextLoc = boundingBox.Location;
			Move(ref nextLoc);
			return !info.world.TypeAt(nextLoc, 1f, typeof(Centipede));
		}



		private void ResetJumpWait()
		{
			movementTillJump = boundingBox.Width;
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
