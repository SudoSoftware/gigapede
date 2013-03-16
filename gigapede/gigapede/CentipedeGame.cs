using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gigapede.GameItems;
using System.Drawing;
using gigapede.Resources;

namespace gigapede
{
	class CentipedeGame : Screen
	{
		public static Texture2D background;
		MyRandom prng = new MyRandom();
		PointF centipedeSpawnLoc = new PointF(0, 0);
		UserInput userInput = new UserInput();
		World world = new World(new RectangleF(new PointF(), GameParameters.TARGET_RESOLUTION));


		public CentipedeGame(ScreenManager manager, Screen exitScreen):
			base(manager, exitScreen)
		{
			Shooter.minY = world.getBounds().Height - GameParameters.DEFAULT_ITEM_WIDTH * GameParameters.EMPTY_FOOTER_ROWS;
			AddWorldContent();
		}



		public override void Update(GameTime gameTime)
		{
			HandleCentipedeSpawning();
			HandleScorpionSpawning();

			userInput.Update();
			world.Update(gameTime, userInput);
		}


		
		public override void Draw()
		{
			SpriteBatch spriteBatch = manager.RM.SpriteB;

			spriteBatch.Begin();
			spriteBatch.Draw(background, GameParameters.screenSize, Microsoft.Xna.Framework.Color.White);
			world.Draw(spriteBatch);
			spriteBatch.End();
		}



		public override void HandleInput(GameTime time, UserInput input)
		{
			if (input.justPressed(UserInput.InputType.ESCAPE))
				ExitScreen();
		}



		public void AddWorldContent()
		{
			world.AddItem(new Spider(new PointF(0, GameParameters.DEFAULT_ITEM_HEIGHT)));

			AddMushrooms();
			//adding of Centipedes and Scorpions are handled in Update function
			world.AddItem(new Shooter(new PointF(
				(world.getBounds().Width + GameParameters.DEFAULT_ITEM_WIDTH) / 2,
				world.getBounds().Height - GameParameters.DEFAULT_ITEM_HEIGHT
			)));
		}
		


		private void AddMushrooms()
		{
			float startY = GameParameters.EMPTY_HEADER_ROWS * GameParameters.DEFAULT_ITEM_HEIGHT;
			float endY = world.getBounds().Height - GameParameters.EMPTY_FOOTER_ROWS * GameParameters.DEFAULT_ITEM_HEIGHT;
			float endX = world.getBounds().Width - GameParameters.DEFAULT_ITEM_WIDTH;

			for (float x = 0; x < endX; x += GameParameters.DEFAULT_ITEM_WIDTH)
				for (float y = startY; y < endY; y += GameParameters.DEFAULT_ITEM_HEIGHT)
					if (prng.nextRange(0, 10) <= 1) // 10% chance of adding
						world.AddItem(new Mushroom(new PointF(x, y)));
		}



		int centipedeCount = 10;
		private void HandleCentipedeSpawning()
		{
			if (centipedeCount > 0 && !world.TypeAt(centipedeSpawnLoc, typeof(Centipede)))
			{
				world.AddItem(new Centipede(centipedeSpawnLoc));
				centipedeCount--;
			}
		}



		private void HandleScorpionSpawning()
		{
			if (prng.nextRange(0, 1000) <= 1) // 0.1% of spawning
			{
				float range = prng.nextRange(GameParameters.EMPTY_HEADER_ROWS, GameParameters.GRID_SIZE - GameParameters.EMPTY_FOOTER_ROWS);
				float x = 0; //temporary
				float y = (float)Math.Round(range) * GameParameters.DEFAULT_ITEM_HEIGHT;

				world.AddItem(new Scorpion(new PointF(x, y)));
			}
		}
	}
}
