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

namespace gigapede
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        ScreenManager manager;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            manager = new ScreenManager(graphics, Content, spriteBatch);

            //this.Window.AllowUserResizing = true;
            //this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            //SoundEffect menutheme = Content.Load<SoundEffect>("menutheme");

            Soundtrack menutrack = new Soundtrack();
            //menutrack.AddAudio(menutheme);

            manager.RM.FontHash["head_font"] = Content.Load<SpriteFont>("MenuHead");
            MenuStyle style = new MenuStyle(new Vector2(270, 220), new Vector2(300, 260), new Vector2(0, 20),
                (SpriteFont)manager.RM.FontHash["head_font"], (SpriteFont)manager.RM.FontHash["Default"],
                Color.Orange, Color.Orange, Color.OrangeRed, menutrack);

            Screen screen = new Screen(manager, null);
            MenuScreen main_menu = new MenuScreen(manager, screen, "Main Menu", style);
            main_menu.AddItem(new MenuItem("This is a dummy item."));
            main_menu.AddItem(new MenuItem("Another dummy item."));

            // Load Background
            Texture2D background = Content.Load<Texture2D>("lcars");

            manager.AddScreen(new BackgroundScreen(manager, background));
            manager.AddScreen(new IntroScreen(manager, main_menu));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            manager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            manager.Draw();

            base.Draw(gameTime);
        }


        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            // Make changes to handle the new window size.            
        }
    }
}
