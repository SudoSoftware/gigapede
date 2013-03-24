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

namespace gigapede
{
    class MenuScreen : Screen
    {
        protected String head_text;

        protected MenuStyle style;

        protected int selected_index;

        protected List<MenuItem> menu_items;

        public MenuScreen(ScreenManager manager, Screen exit_screen, String head_text, MenuStyle style)
            : base(manager, exit_screen)
        {
            this.head_text = head_text;
            this.style = style;

            selected_index = 0;

            menu_items = new List<MenuItem>();
        }

        public void ChangeStyle(MenuStyle style)
        {
            this.style = style;
        }

        public void AddItem(MenuItem new_item, int position = -1)
        {
            if (position < 0)
                position = menu_items.Count;

            menu_items.Insert(position, new_item);
        }

        public void KillItem(MenuItem dead_item)
        {
            menu_items.Remove(dead_item);
        }

        public void SetItemActive(MenuItem item, bool active)
        {
            if (menu_items.Contains(item))
                item.SetActive(active);
        }

        public override void HandleInput(GameTime time, UserInput input)
        {
            base.HandleInput(time, input);

            // Based on input, move the highlighted item across the screen.
            // Pass input to the selected item.

            if (input.justPressed(UserInput.InputType.DOWN) &&
                selected_index +1 < menu_items.Count)
                selected_index++;

            if (input.justPressed(UserInput.InputType.UP) &&
                selected_index > 0)
                selected_index--;

            menu_items[selected_index].HandleInput(time, input);
        }

        public override void Draw()
        {
            SpriteBatch sb = manager.RM.SpriteB;
            String title_font = style.title_font;
            Color head_color = style.head_color;
            Vector2 position = new Vector2(style.head_pos.X, style.head_pos.Y);

            sb.Begin();
            sb.DrawString(
                (SpriteFont)manager.RM.FontHash["TitleFont"],
                head_text,
                position,
                head_color
            );
            sb.End();

            position = new Vector2(style.menu_start.X, style.menu_start.Y);

            foreach (MenuItem x in menu_items)
            {
                bool selected = false;
                if (menu_items[selected_index].Equals(x))
                    selected = true;

                x.Draw(manager, style, position, selected);
                position.X += style.menu_inc.X;
                position.Y += style.menu_inc.Y;

                if (position.Y >= ((float)5 / 6) *
                    GameParameters.TARGET_RESOLUTION.Height)
                {
                    position.Y = style.menu_start.Y;
                    position.X += ((float)100 / 512) *
                        GameParameters.TARGET_RESOLUTION.Width;
                }
            }
        }

    }
}
