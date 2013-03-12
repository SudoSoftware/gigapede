using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gigapede
{
    class MenuStyle
    {
        public Vector2 head_pos;
        public Vector2 menu_start;

        public SpriteFont head_font;
        public SpriteFont font;

        public Color head_color;
        public Color menu_color;

        public Vector2 menu_inc;

        public MenuStyle(Vector2 head_pos, Vector2 menu_start, SpriteFont head_font, SpriteFont font,
            Color head_color, Color menu_color, Vector2 menu_inc)
        {
            this.head_font = head_font;
            this.font = font;

            this.menu_start = menu_start;
            this.head_pos = head_pos;

            this.head_color = head_color;
            this.menu_color = menu_color;

            this.menu_inc = menu_inc;
        }
    }
}
