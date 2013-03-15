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

            // Add the default font to the fonthash.
            fonthash.Add("Default", content.Load<SpriteFont>("Default"));
        }
    }
}
