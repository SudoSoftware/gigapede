using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
			
			Centipede.texture = this.Content.Load<Texture2D>("images/klingon bird of prey");
			Mushroom.normalTexture = this.Content.Load<Texture2D>("images/asteroid1");
			Mushroom.poisonedTexture = this.Content.Load<Texture2D>("images/asteroid2");
			Rocket.texture = this.Content.Load<Texture2D>("images/phaser");
			Scorpion.texture = this.Content.Load<Texture2D>("images/borg cube1");
			Shooter.texture = this.Content.Load<Texture2D>("images/enterprise");

			background = this.Content.Load<Texture2D>("images/starfield");

			AddWorldContent();
		}



		protected override void UnloadContent()
		{
			this.Content.Unload();
		}



		public void AddWorldContent()
		{
			AddMushrooms();
			world.AddItem(new Centipede(new PointF(0, 0)));
			world.AddItem(new Shooter(new PointF(
				(world.getBounds().Width + GameParameters.DEFAULT_ITEM_WIDTH) / 2,
				world.getBounds().Height - GameParameters.DEFAULT_ITEM_HEIGHT
			)));
		}
		


		private void AddMushrooms()
		{
			int startY = GameParameters.EMPTY_HEADER_ROWS * GameParameters.DEFAULT_ITEM_HEIGHT;
			float endY = world.getBounds().Height - GameParameters.EMPTY_FOOTER_ROWS * GameParameters.DEFAULT_ITEM_HEIGHT;
			float endX = world.getBounds().Width - GameParameters.DEFAULT_ITEM_WIDTH;

			for (int x = 0; x < endX; x += GameParameters.DEFAULT_ITEM_WIDTH)
				for (int y = startY; y < endY; y += GameParameters.DEFAULT_ITEM_HEIGHT)
					if (prng.nextRange(0, 10) <= 1) //if there isn't a mushroom directly northwest, 10% chance of adding
						world.AddItem(new Mushroom(new PointF(x, y)));
		}

		int centipedeCount = 10;

		protected override void Update(GameTime gameTime)
		{
			userInput.Update();

			if (userInput.justPressed(UserInput.InputType.ESCAPE))
				this.Exit();

			world.Update(gameTime, userInput);
			if (centipedeCount > 0 && !world.TypeAt(new PointF(0, 0), 1f, typeof(Centipede)))
			{
				world.AddItem(new Centipede(new PointF(0, 0)));
				centipedeCount--;
			}

			base.Update(gameTime);
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
