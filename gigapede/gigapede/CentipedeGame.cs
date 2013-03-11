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

namespace gigapede
{
	public class CentipedeGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		UserInput userInput;
		World world;


		public CentipedeGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = 720;
			graphics.PreferredBackBufferHeight = 480;
			graphics.IsFullScreen = true;
		}



		protected override void Initialize()
		{
			userInput = new UserInput();
			world = new World(new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

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
			world.AddItem(new Shooter(new Point(200, 200)));
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
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			world.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
