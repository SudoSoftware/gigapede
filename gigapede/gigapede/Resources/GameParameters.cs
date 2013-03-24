using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace gigapede.Resources
{
	class GameParameters
	{
        //difficulty
        public static Difficulty difficulty = Difficulty.Easy;

		//behavior
<<<<<<< HEAD
		public static float CENTIPEDE_SPEED = 0.4f;
		public static float SCORPION_SPEED = 0.3f;
		public static float SPIDER_SPEED = 0.25f;
		public static float ROCKET_SPEED = 0.8f;
		public static float FLEA_SPEED = 0.3f;
		public const float SPIDER_ZIGZAG_COEFF = 0.5f;
=======
		public const float CENTIPEDE_SPEED = 0.4f;
		public const float SCORPION_SPEED = 0.3f;
		public const float SPIDER_SPEED = 0.25f;
		public const float ROCKET_SPEED = 0.8f;
		public const float FLEA_SPEED = 0.3f;
>>>>>>> gameplay continues (fixed #30)
		public const float SHOOTER_MOVEMENT_THETA = 0.32f;
		public const float POWERUP_INITIAL_UPWARD_THRUST = -0.3f;
		public const float GRAVITY = 0.02f;

		//world parameters
		public const int GRID_SIZE = 24; //size of N-by-N grid. The original game was around 30 or 35. Must be >= 3
		public const int EMPTY_HEADER_ROWS = 2; //free space for centipede
		public const int EMPTY_FOOTER_ROWS = 5; //free space for shooter movement
		public const int MAX_DAMAGEABLE_HEALTH = 4;
		public const int MAX_LIVES = 3;
		public const int ROCKET_POWERUP_USES = 10;
		public const int DEFAULT_CENTIPEDE_LENGTH = 10;

		//scoring
		public const int CENTIPEDE_POINTS = 10;
		public const int FLEA_POINTS = 200;
		public const int MUSHROOM_POINTS = 1;
		public const int SCORPION_POINTS = 1000;
		public const int SPIDER_CLOSE_POINTS = 900;
		public const int SPIDER_MID_POINTS = 600;
		public const int SPIDER_FAR_POINTS = 300;

		//screen
		public static readonly Size TARGET_RESOLUTION = new Size(1024, 768);
		public static readonly Microsoft.Xna.Framework.Rectangle screenSize = new Microsoft.Xna.Framework.Rectangle(0, 0, TARGET_RESOLUTION.Width, TARGET_RESOLUTION.Height);
		public static readonly float DEFAULT_ITEM_WIDTH = TARGET_RESOLUTION.Width / GRID_SIZE;
		public static readonly float DEFAULT_ITEM_HEIGHT = DEFAULT_ITEM_WIDTH;


        //menu parameters
        public static readonly Vector2 DEFAULT_TITLE_FACTOR = new Vector2 ((float)2.7 / 8, (float)2.7 / 6);
        public static readonly Vector2 DEFAULT_MENU_FACTOR = new Vector2 ((float)3.0 / 8, (float)3.1 / 6);
        public static readonly Vector2 DEFAULT_MENU_ITEM_DISPLACEMENT = new Vector2(0, ((float)1.0 / 20));
        public static readonly String DEFAULT_TITLE_FONT = "TitleFont";
        public static readonly String DEFAULT_MENU_FONT = "MenuFont";
        public static readonly String DEFAULT_LCARS_FONT = "LcarsFont";
        public static readonly String DEFAULT_MENU_BACKGROUND = "lcars";
        public static readonly Microsoft.Xna.Framework.Color DEFAULT_TITLE_COLOR =
            Microsoft.Xna.Framework.Color.Orange;
        public static readonly Microsoft.Xna.Framework.Color DEFAULT_MENU_COLOR =
            Microsoft.Xna.Framework.Color.Orange;
        public static readonly Microsoft.Xna.Framework.Color DEFAULT_SELECTED_ITEM_COLOR =
            Microsoft.Xna.Framework.Color.OrangeRed;


        //soundtrack
        public static readonly String DEFAULT_MENU_SONG = "music/firstContact";
        public static readonly String DEFAULT_GAME_SONG = "music/atdoomsgate";


		//other
		public const float LOCATION_TOLERANCE = 1f;
		public const int ALERT_MILLISECONDS = 1500; //how long the alerts are shown for
	}
}
