using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.GameItems;
using Microsoft.Xna.Framework;
using System.Drawing;
using gigapede.Resources;

namespace gigapede
{
	class AI : UserInput
	{
		private World world;
		private Vector2 goalLocation, bottomRight, bottomLeft;


		public AI(ref World gameWorld):
			base(false)
		{
			world = gameWorld;

			bottomRight = new Vector2(world.getBounds().Right - GameParameters.DEFAULT_ITEM_WIDTH, world.getBounds().Bottom - GameParameters.DEFAULT_ITEM_HEIGHT);
			bottomLeft = new Vector2(0, bottomRight.Y);
			goalLocation = bottomRight;
		}



		protected override void UpdateState()
		{
			CheckForHumanInput();
			UpdateDecisions();
			PressKeys();
		}



		private void CheckForHumanInput()
		{
			base.UpdateState();
			bool humanInputHappened = currentState.Count > 0;

			currentState.Clear();
			if (humanInputHappened)
				currentState.Add(InputType.ESCAPE);
		}



		private void UpdateDecisions()
		{
			if (GetDistanceToGo() < 5)
			{
				if (goalLocation == bottomRight)
					goalLocation = bottomLeft;
				else
					goalLocation = bottomRight;
			}
		}



		private void PressKeys()
		{
			Vector2 travelVector = GetTravelVector();

			if (Math.Abs(travelVector.X) >= 3)
			{
				if (travelVector.X > 0)
					currentState.Add(InputType.RIGHT);
				else
					currentState.Add(InputType.LEFT);
			}

			if (Math.Abs(travelVector.Y) >= 3)
			{
				if (travelVector.Y < 0)
					currentState.Add(InputType.UP);
				else
					currentState.Add(InputType.DOWN);
			}
		}



		private Vector2 GetTravelVector()
		{
			GameItem shooter = world.GetItemOfType(typeof(Shooter));
			PointF shooterLoc = shooter.GetLocation();

			Vector2 vector = new Vector2(shooterLoc.X, shooterLoc.Y);
			vector = goalLocation - vector;
			System.Diagnostics.Debug.WriteLine(shooterLoc+"	"+vector + "	" + goalLocation + "	" + vector.Length());
			return vector;
		}



		private float GetDistanceToGo()
		{
			return GetTravelVector().Length();
		}



		private void MoveTowardsLocation(Vector2 desiredLocation)
		{
			goalLocation = desiredLocation;
		}
	}
}
