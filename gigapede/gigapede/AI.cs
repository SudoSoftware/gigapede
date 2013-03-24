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
		private MyRandom prng = new MyRandom();
		private DateTime lastFired = DateTime.Now, bornTime = DateTime.Now;
		private double millisTillFire = 500;
		private Vector2 goalLocation;
		private readonly float maxX, minY, maxY;


		public AI(ref World gameWorld):
			base(false)
		{
			world = gameWorld;

			PointF shooterLoc = world.GetItemOfType(typeof(Shooter)).GetLocation();
			goalLocation = new Vector2(shooterLoc.X, shooterLoc.Y);

			maxX = world.getBounds().Right - GameParameters.DEFAULT_ITEM_WIDTH;
			minY = world.getBounds().Bottom - GameParameters.DEFAULT_ITEM_HEIGHT * GameParameters.EMPTY_FOOTER_ROWS;
			maxY = world.getBounds().Bottom - GameParameters.DEFAULT_ITEM_HEIGHT;
		}



		protected override void UpdateState()
		{
			CheckForHumanInput();
			UpdateDecisions();
			Navigate();
			Fire();
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
				goalLocation = new Vector2(prng.nextRange(0, maxX), prng.nextRange(minY, maxY));
		}



		private void Navigate()
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



		private bool alreadySpawnedSpider = false;
		private void Fire()
		{
			if (DateTime.Now.Subtract(bornTime).TotalSeconds >= 60 || world.CountTypes(typeof(Centipede)) <= 4)
			{
				if (!alreadySpawnedSpider && DateTime.Now.Subtract(bornTime).TotalSeconds >= 60)
				{
					world.AddItem(new Spider(new PointF(), true));
					alreadySpawnedSpider = true;
				}

				return; //stop firing to try to stay within the assignment parameters
			}

			if (DateTime.Now.Subtract(lastFired).TotalMilliseconds > millisTillFire)
			{
				currentState.Add(InputType.FIRE);
				lastFired = DateTime.Now;
				millisTillFire = prng.nextGaussian(100, 100);
			}
		}



		private Vector2 GetTravelVector()
		{
			GameItem shooter = world.GetItemOfType(typeof(Shooter));
			PointF shooterLoc = shooter.GetLocation();

			Vector2 vector = new Vector2(shooterLoc.X, shooterLoc.Y);
			vector = goalLocation - vector;
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



		public DateTime GetBornTime()
		{
			return bornTime;
		}
	}
}
