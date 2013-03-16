using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.GameItems;

namespace gigapede
{
	class AI : UserInput
	{
		private World world;
		bool movingRight = true;


		public AI(ref World gameWorld)
		{
			world = gameWorld;
			System.Diagnostics.Debug.WriteLine(world + "	" + gameWorld);
		}



		protected override void UpdateState()
		{
			GameItem shooter = world.GetItemOfType(typeof(Shooter));
			if (!world.IsLegalLocation(shooter.GetBounds()))
				movingRight = !movingRight;

			if (movingRight)
				currentState.Add(InputType.RIGHT);
			else
				currentState.Add(InputType.LEFT);

			//base.UpdateState();
		}
	}
}
