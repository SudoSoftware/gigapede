using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	abstract class JumpingGameItem : DamageableGameItem
	{
		protected bool movingRight = true;
		protected float movementTillJump;


		public JumpingGameItem(PointF location) :
			base(location)
		{
			ResetJumpWait();
		}


		public abstract float GetSpeed();


		public override List<GameItemAction> Update(InfoForItem info)
		{
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * GetSpeed();
			movementTillJump -= theta;

			if (movementTillJump <= 0 && CanJump(info))
			{
				Jump(info);
				ResetJumpWait();
			}

			return base.Update(info);
		}


		protected abstract void Jump(InfoForItem info);


		protected void Move(ref PointF pt)
		{
			if (movingRight)
				pt.X += boundingBox.Width;
			else
				pt.X -= boundingBox.Width;
		}



		protected bool CanJump(InfoForItem info)
		{
			PointF nextLoc = boundingBox.Location;
			Move(ref nextLoc);
			return !info.world.TypeAt(nextLoc, this.GetType());
		}



		protected void ResetJumpWait()
		{
			movementTillJump = boundingBox.Width;
		}



		public override bool IsMovable()
		{
			return true;
		}
	}
}
