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

		UserInput userInput;
		World world;

		public const int WIDTH = 720;
		public const int HEIGHT = 480;


		public CentipedeGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = WIDTH;
			graphics.PreferredBackBufferHeight = HEIGHT;
			graphics.IsFullScreen = true;
		}



		protected override void Initialize()
		{
			userInput = new UserInput();
			world = new World(new RectangleF(0, 0, WIDTH, HEIGHT));

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
			world.AddItem(new Shooter(new PointF((WIDTH + GameItem.DEFAULT_WIDTH) / 2, HEIGHT - GameItem.DEFAULT_HEIGHT)));
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
