using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
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
    /*
     * This enum holds indices for the various fonts accessable from ResourceManager.
     * */
    
    public enum FontIndexer
    {
        Default = 0
    }

    class ResourceManager
    {
        // Graphics Device Manager
        GraphicsDeviceManager graphics;

        // Content Pipeline
        ContentManager content;

        // Sprite Batch
        SpriteBatch spriteb;

        // Background Texture
        Texture2D background;

        // Accessors for content, spriteb and graphics.
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }

        public ContentManager Content
        {
            get { return content; }
        }

        public SpriteBatch SpriteB
        {
            get { return spriteb; }
        }

        public Texture2D Background
        {
            get { return background; }
            set { this.background = value; }
        }


        // Hashtable of SpriteFonts.
        Hashtable fonthash;

        // Accessor for fonthash
        public Hashtable FontHash
        {
            get { return fonthash; }
        }

        public ResourceManager(GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteb)
        {
            // Initialize the ContentManager and the SpriteBatch.
            this.graphics = graphics;
            this.content = content;
            this.spriteb = spriteb;


            //Load up fonts and misc resources here.
            
            // Initialize FontHash
            fonthash = new Hashtable();
        }

        public void LoadResources(String prefix)
        {
                //load game textures
                CentipedeGame.background = this.Content.Load<Texture2D>(prefix + "textures/starfield");
                Centipede.texture = this.Content.Load<Texture2D>(prefix + "textures/klingon bird of prey");
                Flea.texture = this.Content.Load<Texture2D>(prefix + "textures/Centipede");
                Mushroom.normalTexture = this.Content.Load<Texture2D>(prefix + "textures/Mushroom");
                Mushroom.poisonedTexture = this.Content.Load<Texture2D>(prefix + "textures/PMushroom");
                Powerup.texture = this.Content.Load<Texture2D>(prefix + "textures/US coin");
                Rocket.primaryTexture = this.Content.Load<Texture2D>(prefix + "textures/phaser");
                Rocket.secondaryTexture = this.Content.Load<Texture2D>(prefix + "textures/photon torpedo");
                Scorpion.texture = this.Content.Load<Texture2D>(prefix + "textures/Scorpion");
                Shooter.texture = this.Content.Load<Texture2D>(prefix + "textures/Shooter");
                Spider.texture = this.Content.Load<Texture2D>(prefix + "textures/ferangi vessel");

                //load all game fonts
                HeadsUpDisplay.font = Content.Load<SpriteFont>(prefix + "temporaryFont");

                // Load up Soundtrack
                MainMenuScreen.menu_theme = Content.Load<Song>(prefix + GameParameters.DEFAULT_MENU_SONG);
                CentipedeGame.game_theme = Content.Load<Song>(prefix + GameParameters.DEFAULT_GAME_SONG);

                // Load up all menu resources.
                Background = Content.Load<Texture2D>(prefix + "lcars"); // prefix + GameParameters.DEFAULT_MENU_BACKGROUND);
                FontHash.Add("TitleFont", Content.Load<SpriteFont>(prefix + GameParameters.DEFAULT_TITLE_FONT));
                FontHash.Add("MenuFont", Content.Load<SpriteFont>(prefix + GameParameters.DEFAULT_MENU_FONT));
                FontHash.Add("LcarsFont", Content.Load<SpriteFont>(prefix + GameParameters.DEFAULT_LCARS_FONT));
        }
    }
}
