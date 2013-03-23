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
using gigapede.Resources;
using gigapede.GameItems;

namespace gigapede
{
	public class CentipedeEntry : Microsoft.Xna.Framework.Game
	{
		ScreenManager manager;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public CentipedeEntry()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferHeight = GameParameters.TARGET_RESOLUTION.Height;
			graphics.PreferredBackBufferWidth = GameParameters.TARGET_RESOLUTION.Width;
			graphics.IsFullScreen = true;

			Content.RootDirectory = "Content";
		}

		
		protected override void Initialize()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			manager = new ScreenManager(this, graphics, Content, spriteBatch);


            // Set up default key bindings.
            UserInput.LeftKey = Keys.Left;
            UserInput.RightKey = Keys.Right;
            UserInput.UpKey = Keys.Up;
            UserInput.DownKey = Keys.Down;
            UserInput.EscKey = Keys.Escape;
            UserInput.FireKey = Keys.Space;


            // Soundtrack should repreat.
            MediaPlayer.IsRepeating = true;
            
            Vector2 SCREEN_PARAMETERS =
                new Vector2(
                    GameParameters.TARGET_RESOLUTION.Width,
                    GameParameters.TARGET_RESOLUTION.Height
                );

			MenuStyle style =
                new MenuStyle(
                    GameParameters.DEFAULT_TITLE_FACTOR * SCREEN_PARAMETERS,
    				GameParameters.DEFAULT_MENU_FACTOR * SCREEN_PARAMETERS,
                    GameParameters.DEFAULT_MENU_ITEM_DISPLACEMENT * SCREEN_PARAMETERS,
		    		"TitleFont",
                    "MenuFont",
				    GameParameters.DEFAULT_TITLE_COLOR,
                    GameParameters.DEFAULT_MENU_COLOR,
                    GameParameters.DEFAULT_SELECTED_ITEM_COLOR
			    );

			MainMenuScreen main_menu =
                new MainMenuScreen(manager, new ExitScreen(manager, null), style);
			

			manager.AddScreen(new BackgroundScreen(manager));

            IntroScreen intro_screen = new IntroScreen(manager, main_menu, style.head_pos);
			manager.AddScreen(intro_screen);
            manager.FocusScreen(intro_screen);

			base.Initialize();
		}

		
		protected override void LoadContent()
		{
            manager.RM.LoadResources("default/");
		}

		
		protected override void UnloadContent()
		{
			this.Content.Unload();
		}

		
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			this.Exit();

			manager.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			manager.Draw();
			base.Draw(gameTime);
		}
	}
}
