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

			//SoundEffect menutheme = Content.Load<SoundEffect>("menutheme");

			Soundtrack menutrack = new Soundtrack();
			//menutrack.AddAudio(menutheme);

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
		    		Content.Load<SpriteFont>(GameParameters.DEFAULT_TITLE_FONT),
                    (SpriteFont)manager.RM.FontHash["Default"],
				    GameParameters.DEFAULT_TITLE_COLOR,
                    GameParameters.DEFAULT_MENU_COLOR,
                    GameParameters.DEFAULT_SELECTED_ITEM_COLOR,
                    menutrack
			    );

			MainMenuScreen main_menu =
                new MainMenuScreen(manager, new ExitScreen(manager, null), style);
			

			manager.AddScreen(
                new BackgroundScreen(
                    manager,
                    Content.Load<Texture2D>(GameParameters.DEFAULT_MENU_BACKGROUND)
                )
            );

			manager.AddScreen(new IntroScreen(manager, main_menu, style.head_pos));


			MenuScreen sub_menu = new MenuScreen(manager, main_menu, "Sub Menu", style);
			sub_menu.AddItem(new MenuQuitButton("Go Back", sub_menu));

			base.Initialize();
		}

		
		protected override void LoadContent()
		{
			//load game textures
			CentipedeGame.background = this.Content.Load<Texture2D>("textures/starfield");
			Centipede.texture = this.Content.Load<Texture2D>("textures/klingon bird of prey");
			Flea.texture = this.Content.Load<Texture2D>("textures/romulan warbird");
			Mushroom.normalTexture = this.Content.Load<Texture2D>("textures/asteroid1");
			Mushroom.poisonedTexture = this.Content.Load<Texture2D>("textures/asteroid2");
			Powerup.texture = this.Content.Load<Texture2D>("textures/US coin");
			Rocket.primaryTexture = this.Content.Load<Texture2D>("textures/phaser");
			Rocket.secondaryTexture = this.Content.Load<Texture2D>("textures/photon torpedo");
			Scorpion.texture = this.Content.Load<Texture2D>("textures/borg cube1");
			Shooter.texture = this.Content.Load<Texture2D>("textures/enterprise");
			Spider.texture = this.Content.Load<Texture2D>("textures/ferangi vessel");

			//load all fonts
			HeadsUpDisplay.font = Content.Load<SpriteFont>("temporaryFont");
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