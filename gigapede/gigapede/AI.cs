using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigapede
{
	class AI : UserInput
	{
		private World world;


		public AI(ref World gameWorld)
		{
			world = gameWorld;
		}


		protected override void UpdateState()
		{
			
		}
	}
}
