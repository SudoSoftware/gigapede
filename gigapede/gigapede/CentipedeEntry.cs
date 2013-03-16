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

			int width = graphics.PreferredBackBufferWidth;
			int height = graphics.PreferredBackBufferHeight;

			manager.RM.FontHash["head_font"] = Content.Load<SpriteFont>("MenuHead");
			MenuStyle style = new MenuStyle(
				new Vector2(((float)2.7 / 8) * width, ((float)2.7 / 6) * height),
				new Vector2(((float)3.0 / 8) * width, ((float)3.1 / 6) * height),
				new Vector2(0, ((float)1.0 / 20) * graphics.PreferredBackBufferHeight),
				(SpriteFont)manager.RM.FontHash["head_font"], (SpriteFont)manager.RM.FontHash["Default"],
				Color.Orange, Color.Orange, Color.OrangeRed, menutrack
			);

			MenuScreen main_menu = new MenuScreen(manager, new ExitScreen(manager, null), "Main Menu", style);
			main_menu.AddItem(new AddScreenButton("Start Game", manager, typeof(CentipedeGame),
				new Object[] { manager, main_menu }));
			main_menu.AddItem(new AddScreenButton("Go to submenu", manager, typeof(MenuScreen),
				new Object[] { manager, main_menu, "Sub Menu", style }));
			main_menu.AddItem(new AddScreenButton("Credits", manager, typeof(CreditsScreen),
				new Object[] { manager, main_menu, style.head_pos}));
			main_menu.AddItem(new MenuQuitButton("Quit", main_menu));

			// Load Background
			Texture2D background = Content.Load<Texture2D>("lcars");

			manager.AddScreen(new BackgroundScreen(manager, background));
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
