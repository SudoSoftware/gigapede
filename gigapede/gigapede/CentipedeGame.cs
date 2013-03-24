using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gigapede.GameItems;
using System.Drawing;
using gigapede.Resources;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
	class CentipedeGame : Screen
	{
		public static Texture2D background;
		private MyRandom prng = new MyRandom();
		private PointF centipedeSpawnLoc = new PointF(0, 0);
		private World world = new World(new RectangleF(new PointF(), GameParameters.TARGET_RESOLUTION));
		private UserInput userInput = new UserInput();

        public static Song game_theme;


		public CentipedeGame(ScreenManager manager, Screen exitScreen) :
			this(manager, exitScreen, false)
		{ }


		public CentipedeGame(ScreenManager manager, Screen exitScreen, bool startsInAttractMode) :
			base(manager, exitScreen)
		{
			Shooter.minY = world.getBounds().Height - GameParameters.DEFAULT_ITEM_HEIGHT * GameParameters.EMPTY_FOOTER_ROWS;
			AddWorldContent();
			if (startsInAttractMode)
				userInput = new AI(ref world);

            // Soundtrack.
            if (game_theme == null)
                game_theme = manager.RM.Content.Load<Song>(
                    "default/" + Resources.GameParameters.DEFAULT_GAME_SONG
                );
		}



        public override void GotFocus()
        {
            if (manager.current_song != game_theme)
                MediaPlayer.Play(game_theme);

            manager.current_song = game_theme;
        }
        


		public override void Update(GameTime gameTime)
		{
			if (!world.IsPlayerAlive())
				HandlePlayerRespawn(gameTime);
			
			UpdateGame(gameTime);
		}



		private DateTime respawnStart;
		private void HandlePlayerRespawn(GameTime gameTime)
		{
			world.RemoveAllOfType(typeof(Centipede));
			world.RemoveAllOfType(typeof(Rocket));
			world.RemoveAllOfType(typeof(Flea));
			world.RemoveAllOfType(typeof(Scorpion));
			world.RemoveAllOfType(typeof(Spider));

			world.getHUD().IndicateLostLife();

		}



		private void UpdateGame(GameTime gameTime)
		{
			if (userInput.GetType() != typeof(AI) && userInput.GetTimeSinceLastInput().TotalSeconds >= 10)
			{
				userInput = new AI(ref world);
				world.getHUD().SetAttactMode(true);
			}

			HandleCentipedeSpawning();
			HandleScorpionSpawning();
			HandleFleaSpawning();

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



		public override void HandleInput(GameTime time, UserInput notUsing)
		{
			if (userInput.justPressed(UserInput.InputType.ESCAPE))
				ExitScreen();
		}



		public void AddWorldContent()
		{
			world.AddItem(new Spider(new PointF(0, GameParameters.DEFAULT_ITEM_HEIGHT)));

			AddMushrooms();
			//adding of Centipedes and Scorpions are handled in Update function

			Shooter player = AddShooter();
			world.setHUD(new HeadsUpDisplay(ref player));
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



		//spawns a new Shooter, centered in the empty footer, but avoids spawning on top of existing GameItems
		public Shooter AddShooter()
		{
			int centerColumn = (int)(world.getBounds().Width / GameParameters.DEFAULT_ITEM_WIDTH) / 2;

			PointF shooterSpawnLoc = new PointF(
				centerColumn * GameParameters.DEFAULT_ITEM_WIDTH,
				world.getBounds().Height - (float)Math.Ceiling((double)GameParameters.EMPTY_FOOTER_ROWS / 2) * GameParameters.DEFAULT_ITEM_HEIGHT
			);
			
			//avoid spawning on top of any existing GameItem
			float originalSpawnX = shooterSpawnLoc.X;
			for (int offset = 0; offset < centerColumn; offset++)
			{
				shooterSpawnLoc.X = originalSpawnX + GameParameters.DEFAULT_ITEM_WIDTH * (offset + 1);
				if (world.ItemAt(shooterSpawnLoc) == null)
					break;

				shooterSpawnLoc.X = originalSpawnX + GameParameters.DEFAULT_ITEM_WIDTH * (offset + 1) * -1;
				if (world.ItemAt(shooterSpawnLoc) == null)
					break;
			}

			Shooter player = new Shooter(shooterSpawnLoc);
			world.AddItem(player);
			return player;
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



		private DateTime fleaLastSpawned = DateTime.Now;
		private void HandleFleaSpawning()
		{
			if (!Flea.SpawnIsAppropriate(world))
				return;

			if (DateTime.Now.Subtract(fleaLastSpawned).TotalMilliseconds >= prng.nextGaussian(4000, 1))
			{
				float x = (int)prng.nextRange(0, GameParameters.GRID_SIZE) * GameParameters.DEFAULT_ITEM_WIDTH;
				
				world.AddItem(new Flea(new PointF(x, 0)));
				fleaLastSpawned = DateTime.Now;
			}
		}
	}
}
