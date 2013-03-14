using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gigapede.Resources
{
	class GameParameters
	{
		//behavior
		public const float CENTIPEDE_SPEED = 0.4f;
		public const float SCORPION_SPEED = 0.3f;
		public const float ROCKET_SPEED = 0.8f;
		public const float SHOOTER_MOVEMENT_THETA = 0.25f;
		public const int MAX_DAMAGEABLE_HEALTH = 4;

		//world parameters
		public const int GRID_SIZE = 24; //size of N-by-N grid. The original game was around 30 or 35
		public const int EMPTY_HEADER_ROWS = 2; //free space for centipede
		public const int EMPTY_FOOTER_ROWS = 5; //free space for shooter movement

		//screen
		public static readonly Size TARGET_RESOLUTION = new Size(1024, 768);
		public static readonly Microsoft.Xna.Framework.Rectangle screenSize = new Microsoft.Xna.Framework.Rectangle(0, 0, TARGET_RESOLUTION.Width, TARGET_RESOLUTION.Height);
		public static readonly int DEFAULT_ITEM_WIDTH = (int)(TARGET_RESOLUTION.Width / GRID_SIZE);
		public static readonly int DEFAULT_ITEM_HEIGHT = DEFAULT_ITEM_WIDTH;

		//other
		public static readonly float LOC_TOLERANCE = 1f;
	}
}
