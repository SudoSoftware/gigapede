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
	public class CentipedeGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D background;
		MyRandom prng = new MyRandom();
		PointF centipedeSpawnLoc = new PointF(0, 0);
		UserInput userInput;
		World world;


		public CentipedeGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			
			graphics.PreferredBackBufferWidth = GameParameters.TARGET_RESOLUTION.Width;
			graphics.PreferredBackBufferHeight = GameParameters.TARGET_RESOLUTION.Height;
			graphics.IsFullScreen = true;
		}



		protected override void Initialize()
		{
			userInput = new UserInput();
			world = new World(new RectangleF(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

			Shooter.minY = world.getBounds().Height - GameParameters.DEFAULT_ITEM_WIDTH * GameParameters.EMPTY_FOOTER_ROWS;

			base.Initialize();
		}



		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//load all textures
			background = this.Content.Load<Texture2D>("images/starfield");
			Centipede.texture = this.Content.Load<Texture2D>("images/klingon bird of prey");
			Flea.texture = this.Content.Load<Texture2D>("images/romulan warbird");
			Mushroom.normalTexture = this.Content.Load<Texture2D>("images/asteroid1");
			Mushroom.poisonedTexture = this.Content.Load<Texture2D>("images/asteroid2");
			Powerup.texture = this.Content.Load<Texture2D>("images/US coin");
			Rocket.primaryTexture = this.Content.Load<Texture2D>("images/phaser");
			Rocket.secondaryTexture = this.Content.Load<Texture2D>("images/photon torpedo");
			Scorpion.texture = this.Content.Load<Texture2D>("images/borg cube1");
			Shooter.texture = this.Content.Load<Texture2D>("images/enterprise");
			Spider.texture = this.Content.Load<Texture2D>("images/ferangi vessel");

			HeadsUpDisplay.font = Content.Load<SpriteFont>("temporaryFont");

			AddWorldContent();
		}



		protected override void UnloadContent()
		{
			this.Content.Unload();
		}



		public void AddWorldContent()
		{
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



		protected override void Update(GameTime gameTime)
		{
			userInput.Update();

			if (userInput.justPressed(UserInput.InputType.ESCAPE))
				this.Exit();

			HandleCentipedeSpawning();
			HandleScorpionSpawning();
			world.Update(gameTime, userInput);

			base.Update(gameTime);
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



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
			
			spriteBatch.Begin();
			spriteBatch.Draw(background, GameParameters.screenSize, Microsoft.Xna.Framework.Color.White);
			world.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
