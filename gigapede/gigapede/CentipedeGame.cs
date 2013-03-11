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

namespace gigapede
{
	public class CentipedeGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		MyRandom prng = new MyRandom();
		UserInput userInput;
		World world;

		public const int GRID_SIZE = 30; //size of N-by-N grid
		public const int EMPTY_HEADER_ROWS = 2; //free space for centipede
		public const int EMPTY_FOOTER_ROWS = 5; //free space for shooter movement

		public static readonly Size TARGET_RESOLUTION = new Size(1024, 768);


		public CentipedeGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			
			graphics.PreferredBackBufferWidth = TARGET_RESOLUTION.Width;
			graphics.PreferredBackBufferHeight = TARGET_RESOLUTION.Height;
			graphics.IsFullScreen = true;
		}



		protected override void Initialize()
		{
			userInput = new UserInput();
			world = new World(new RectangleF(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

			GameItem.defaultWidth = (int)(TARGET_RESOLUTION.Width / GRID_SIZE);
			GameItem.defaultHeight = (int)GameItem.defaultWidth;

			Shooter.minY = world.getBounds().Height - GameItem.defaultWidth * EMPTY_FOOTER_ROWS;

			base.Initialize();
		}



		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Centipede.texture = this.Content.Load<Texture2D>("popcorn");
			Mushroom.texture = this.Content.Load<Texture2D>("mushroom");
			Rocket.texture = this.Content.Load<Texture2D>("rocket");
			Shooter.texture = this.Content.Load<Texture2D>("shooter");

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
				(world.getBounds().Width + GameItem.defaultWidth) / 2,
				world.getBounds().Height - GameItem.defaultHeight
			)));
		}
		


		private void AddMushrooms()
		{
			int startY = EMPTY_HEADER_ROWS * GameItem.defaultHeight;
			float endY = world.getBounds().Height - EMPTY_FOOTER_ROWS * GameItem.defaultHeight;
			float endX = world.getBounds().Width - GameItem.defaultWidth;

			for (int x = 0; x < endX; x += GameItem.defaultWidth)
				for (int y = startY; y < endY; y += GameItem.defaultHeight)
					if (!(x == 0 && y == 0) && prng.nextRange(0, 10) <= 1) //do not add a mushroom at location of Centipede, else 10% chance of adding
						world.AddItem(new Mushroom(new PointF(x, y)));
		}



		protected override void Update(GameTime gameTime)
		{
			userInput.Update();

			if (userInput.justPressed(UserInput.InputType.ESCAPE))
				this.Exit();

			world.Update(gameTime, userInput);

			base.Update(gameTime);
		}



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

			

			spriteBatch.Begin();
			world.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
