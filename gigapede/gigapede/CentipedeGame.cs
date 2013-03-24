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

			timeAtNextScorpionSpawn = DateTime.Now.Add(new TimeSpan(0, 0, 8)); //6 seconds from now
			timeAtNextSpiderSpawn = DateTime.Now.Add(new TimeSpan(0, 0, 6)); //6 seconds from now
			timeAtNextFleaSpawn = DateTime.Now.Add(new TimeSpan(0, 0, 3)); //3 seconds from now
        }
        


		public override void Update(GameTime gameTime)
		{
			if (!world.IsPlayerAlive())
				HandlePlayerRespawn(gameTime);
			
			UpdateGame(gameTime);
		}



		private List<GameItem> mushrooms = new List<GameItem>();
		private DateTime timeAtNextSum;
		private void HandlePlayerRespawn(GameTime gameTime)
		{
			world.RemoveAllOfType(typeof(Centipede));
			world.RemoveAllOfType(typeof(Rocket));
			world.RemoveAllOfType(typeof(Flea));
			world.RemoveAllOfType(typeof(Scorpion));
			world.RemoveAllOfType(typeof(Spider));

			Shooter shooter = (Shooter)world.GetItemOfType(typeof(Shooter));
			world.getHUD().IndicateLostLife();
			if (shooter.GetLivesLeft() <= 0)
				ExitScreen();

			if (mushrooms.Count == 0)
			{
				mushrooms = world.GetAllItemsOfType(typeof(Mushroom));
				timeAtNextSum = DateTime.Now.Add(new TimeSpan(0, 0, 0, 0, 500));
			}

			if (DateTime.Now.Subtract(timeAtNextSum).TotalSeconds >= 0)
			{
				foreach (GameItem item in mushrooms)
				{
					Mushroom mushroom = (Mushroom)item;
					if (mushroom.GetAliveness() < 1)
					{
						shooter.AddToScore(GameParameters.MUSHROOM_POINTS, mushroom, world.getHUD());
						mushroom.ResetHealth();
						mushroom.isRespawning = true;
						timeAtNextSum = DateTime.Now.Add(new TimeSpan(0, 0, 0, 0, 500));
						break;
					}
					else
						mushroom.isRespawning = false;
				}
			}

			if (DateTime.Now.Subtract(shooter.GetDeathDate()).TotalSeconds >= 2)
			{
				shooter.Respawn();
				world.getHUD().IndicateRespawn();
			}
		}



		private void UpdateGame(GameTime gameTime)
		{
			if (userInput.GetType() != typeof(AI) && userInput.GetTimeSinceLastInput().TotalSeconds >= 10)
			{
				userInput = new AI(ref world);
				world.getHUD().SetAttactMode(true);
			}

			HandleCentipedeSpawning();

			if (userInput.GetType() != typeof(AI))
			{
				HandleSpiderSpawning();
				HandleScorpionSpawning();
				HandleFleaSpawning();
			}

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
			AddMushrooms();
			//spawning of Centipedes, Spiders, and Scorpions are handled in Update function

			Shooter player = new Shooter(GetNewShooterLocation());
			world.AddItem(player);
			world.setHUD(new HeadsUpDisplay(ref player));
		}



		private void AddMushrooms()
		{
			float startY = GameParameters.EMPTY_HEADER_ROWS * GameParameters.DEFAULT_ITEM_HEIGHT;
			float endY = world.getBounds().Height - GameParameters.EMPTY_FOOTER_ROWS * GameParameters.DEFAULT_ITEM_HEIGHT;
			float endX = world.getBounds().Width - GameParameters.DEFAULT_ITEM_WIDTH;

			for (float x = 0; x < endX; x += GameParameters.DEFAULT_ITEM_WIDTH)
				for (float y = startY; y < endY; y += GameParameters.DEFAULT_ITEM_HEIGHT)
					if (prng.nextRange(0, 15) <= 1) //7.5% chance of adding
						world.AddItem(new Mushroom(new PointF(x, y)));
		}



		//spawns a new Shooter, centered in the empty footer, but avoids spawning on top of existing GameItems
		public PointF GetNewShooterLocation()
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

			return shooterSpawnLoc;
		}



		bool shouldSpawnCentipede;
		int centipedeSegmentsToSpawn;
		private void HandleCentipedeSpawning()
		{
			if (world.CountTypes(typeof(Centipede)) <= 1)
			{
				shouldSpawnCentipede = true;
				centipedeSegmentsToSpawn = GameParameters.DEFAULT_CENTIPEDE_LENGTH;
			}

			if (centipedeSegmentsToSpawn == 0)
				shouldSpawnCentipede = false;

			if (shouldSpawnCentipede && !world.TypeAt(centipedeSpawnLoc, typeof(Centipede)))
			{
				world.AddItem(new Centipede(centipedeSpawnLoc));
				centipedeSegmentsToSpawn--;
			}
		}



		private DateTime timeAtNextSpiderSpawn;
		private void HandleSpiderSpawning()
		{
			if (world.CountTypes(typeof(Spider)) > 0)
				return; //only one spider can be in the world at a time

			if (DateTime.Now.Subtract(timeAtNextSpiderSpawn).TotalSeconds >= 0)
			{
				bool spawnsOnLeft = prng.nextRange(0, 2) <= 1;
				float spawnX = spawnsOnLeft ? 0 : world.getBounds().Right - GameParameters.DEFAULT_ITEM_WIDTH;

				float spawnY = (int)Math.Round(world.getBounds().Bottom * 2 / 3);
				spawnY += prng.nextRange(-GameParameters.DEFAULT_ITEM_HEIGHT, GameParameters.DEFAULT_ITEM_HEIGHT);

				world.AddItem(new Spider(new PointF(spawnX, spawnY)));
				TimeSpan span = new TimeSpan(0, 0, (int)Math.Round(prng.nextRange(3, 6)));
				timeAtNextSpiderSpawn = DateTime.Now.Add(span);
			}
		}



		private DateTime timeAtNextScorpionSpawn;
		private void HandleScorpionSpawning()
		{
			if (world.CountTypes(typeof(Scorpion)) > 0)
				return; //only one Scorpion in the world at a time

			if (DateTime.Now.Subtract(timeAtNextScorpionSpawn).TotalSeconds >= 0)
			{
				bool spawnsOnLeft = prng.nextRange(0, 2) <= 1;
				float spawnX = spawnsOnLeft ? 0 : world.getBounds().Right - GameParameters.DEFAULT_ITEM_WIDTH;

				float range = prng.nextRange(GameParameters.EMPTY_HEADER_ROWS, GameParameters.GRID_SIZE - GameParameters.EMPTY_FOOTER_ROWS);
				float y = (float)Math.Round(range) * GameParameters.DEFAULT_ITEM_HEIGHT;

				world.AddItem(new Scorpion(new PointF(spawnX, y), (spawnX == 0)));
				timeAtNextScorpionSpawn = DateTime.Now.Add(new TimeSpan(0, 0, (int)Math.Round(prng.nextRange(8, 12))));
			}
		}



		private DateTime timeAtNextFleaSpawn;
		private void HandleFleaSpawning()
		{
			if (!Flea.SpawnIsAppropriate(world) || world.CountTypes(typeof(Flea)) > 0)
				return;

			if (DateTime.Now.Subtract(timeAtNextFleaSpawn).TotalSeconds >= 0)
			{
				float x = (int)prng.nextRange(0, GameParameters.GRID_SIZE) * GameParameters.DEFAULT_ITEM_WIDTH;
				world.AddItem(new Flea(new PointF(x, 0)));
				timeAtNextFleaSpawn = DateTime.Now.Add(new TimeSpan(0, 0, (int)Math.Round(prng.nextRange(1, 3))));
			}
		}
	}
}
