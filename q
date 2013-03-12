[33mcommit 97e82622cfb763847feb4b33ff61c8b8f2e6049c[m
Author: pcm2718 <parker.michaelson@gmail.com>
Date:   Tue Mar 12 01:12:33 2013 -0600

    Removed swap files.

[1mdiff --git a/gigapede/gigapede/MenuItem.cs~ b/gigapede/gigapede/MenuItem.cs~[m
[1mdeleted file mode 100644[m
[1mindex 0cc4a02..0000000[m
[1m--- a/gigapede/gigapede/MenuItem.cs~[m
[1m+++ /dev/null[m
[36m@@ -1,35 +0,0 @@[m
[31m-using System;[m
[31m-using System.Collections.Generic;[m
[31m-using System.linq;[m
[31m-using Microsoft.Xna.Framework;[m
[31m-using Microsoft.Xna.Framework.Audio;[m
[31m-using Microsoft.Xna.Framework.Content;[m
[31m-using Microsoft.Xna.Framework.GamerServices;[m
[31m-using Microsoft.Xna.Framework.Graphics;[m
[31m-using Microsoft.Xna.Framework.Input;[m
[31m-using Microsoft.Xna.Framework.Media;[m
[31m-[m
[31m-namespace gigapede[m
[31m-{[m
[31m-    class MenuItem[m
[31m-    {[m
[31m-	private String display_text;[m
[31m-[m
[31m-        public MenuItem (String init_text)[m
[31m-	{[m
[31m-	    display_text = init_text;[m
[31m-	}[m
[31m-[m
[31m-	public HandleInput (GameTime time, InputBundle input)[m
[31m-	{[m
[31m-	}[m
[31m-[m
[31m-	public Draw (ResourceManager manager, MenuStyle style, Vector2 postion)[m
[31m-	{[m
[31m-	    SpriteBatch sb = manager.RM.SpriteB;[m
[31m-	    FontTexture font = style.font;[m
[31m-[m
[31m-	    sb.DrawText(DisplayText, font, position);[m
[31m-	}[m
[31m-    }[m
[31m-}[m
[1mdiff --git a/gigapede/gigapede/MenuScreen.cs~ b/gigapede/gigapede/MenuScreen.cs~[m
[1mdeleted file mode 100644[m
[1mindex bda7d4f..0000000[m
[1m--- a/gigapede/gigapede/MenuScreen.cs~[m
[1m+++ /dev/null[m
[36m@@ -1,78 +0,0 @@[m
[31m-using System;[m
[31m-using System.Collections.Generic;[m
[31m-using System.linq;[m
[31m-using Microsoft.Xna.Framework;[m
[31m-using Microsoft.Xna.Framework.Audio;[m
[31m-using Microsoft.Xna.Framework.Content;[m
[31m-using Microsoft.Xna.Framework.GamerServices;[m
[31m-using Microsoft.Xna.Framework.Graphics;[m
[31m-using Microsoft.Xna.Framework.Input;[m
[31m-using Microsoft.Xna.Framework.Media;[m
[31m-[m
[31m-namespace gigapede[m
[31m-{[m
[31m-    class MenuScreen : Screen[m
[31m-    {[m
[31m-	private String head_text;[m
[31m-[m
[31m-        private MenuStyle style;[m
[31m-[m
[31m-	private List<MenuItem> menu_items;[m
[31m-[m
[31m-	public MenuScreen (String head_text, MenuStyle style)[m
[31m-	{[m
[31m-	    this.head_text = head_text;[m
[31m-	    this.style = style;[m
[31m-	}[m
[31m-[m
[31m-	public ChangeStyle (MenuStyle style)[m
[31m-	{[m
[31m-	    this.style = style;[m
[31m-	}[m
[31m-[m
[31m-	public AddItem (MenuItem new_item, int position = menu_items.size)[m
[31m-	{[m
[31m-	    menu_items.Insert(new_item, position);[m
[31m-	}[m
[31m-[m
[31m-	public KillItem (MenuItem dead_item)[m
[31m-	{[m
[31m-	    menu_items.Remove(dead_item)[m
[31m-	}[m
[31m-[m
[31m-	public SetItemActive (MenuItem item, bool active)[m
[31m-	{[m
[31m-	    if (menu_items.Contains(item))[m
[31m-                item.SetActive(active);[m
[31m-	}[m
[31m-[m
[31m-	public override HandleInput (GameTime time, InputBundle input)[m
[31m-	{[m
[31m-	    // Based on input, move the highlighted item across the screen.[m
[31m-	    // If the input is not used here, pass it to the item.[m
[31m-	    [m
[31m-	    for (MenuItem x in menu_items)[m
[31m-	        x.HandleInput(time, input)[m
[31m-	}[m
[31m-[m
[31m-	public override Draw ()[m
[31m-	{[m
[31m-	    SpriteBatch sb = manager.RM.SpriteB;[m
[31m-	    FontTexture head_font = style.head_font;[m
[31m-[m
[31m-[m
[31m-	    Vector2 postion = new Vector2(style.headpos.X, style.headpos.Y);[m
[31m-	    sb.DrawText(head_text, head_font, position);[m
[31m-[m
[31m-	    postion = new Vector2(style.startpos.X, style.startpos.Y);[m
[31m-[m
[31m-	    for (MenuItem x in menu_items)[m
[31m-	    {[m
[31m-	        x.Draw(manager, style, position);[m
[31m-		position.X += style.startpos.X;[m
[31m-		position.Y += style.startpos.Y;[m
[31m-	    }[m
[31m-	}[m
[31m-[m
[31m-    }[m
[31m-}[m
[1mdiff --git a/gigapede/gigapede/Screen.cs~ b/gigapede/gigapede/Screen.cs~[m
[1mdeleted file mode 100644[m
[1mindex 8123742..0000000[m
[1m--- a/gigapede/gigapede/Screen.cs~[m
[1m+++ /dev/null[m
[36m@@ -1,56 +0,0 @@[m
[31m-using System;[m
[31m-using System.Collections.Generic;[m
[31m-using System.linq;[m
[31m-using Microsoft.Xna.Framework;[m
[31m-using Microsoft.Xna.Framework.Audio;[m
[31m-using Microsoft.Xna.Framework.Content;[m
[31m-using Microsoft.Xna.Framework.GamerServices;[m
[31m-using Microsoft.Xna.Framework.Graphics;[m
[31m-using Microsoft.Xna.Framework.Input;[m
[31m-using Microsoft.Xna.Framework.Media;[m
[31m-[m
[31m-namespace gigapede[m
[31m-{[m
[31m-[m
[31m-    class Screen[m
[31m-    {[m
[31m-	public bool hidden_p;[m
[31m-[m
[31m-        private ScreenManager manager;[m
[31m-        private Screen exit_screen;[m
[31m-[m
[31m-[m
[31m-        public Screen (ScreenManager manager, Screen exit_screen)[m
[31m-        {[m
[31m-	    this.manager = manager[m
[31m-            this.exit_screen = exit_screen[m
[31m-        }[m
[31m-[m
[31m-	public virtual void ExitScreen ()[m
[31m-	{[m
[31m-	    manager.KillScreen(this);[m
[31m-	    manager.AddScreen(exit_screen);[m
[31m-	    manager.FocusScreen(exit_screen);[m
[31m-	}[m
[31m-[m
[31m-        public virtual void Update (GameTime time)[m
[31m-        {[m
[31m-[m
[31m-        }[m
[31m-[m
[31m-        public virtual void HandleInput (GameTime time, InputBundle input)[m
[31m-        {[m
[31m-[m
[31m-        }[m
[31m-[m
[31m-        public virtual void Draw ()[m
[31m-        {[m
[31m-            GraphicsDevice gd = manager.RM.Graphics;[m
[31m-            SpriteBatch sb = manager.RM.SpriteB;[m
[31m-[m
[31m-	    gd.Clear(Color.CornflowerBlue);[m
[31m-            sb.DrawText("This is a screen.", new Vector2(100, 100), Color.White)[m
[31m-        }[m
[31m-    }[m
[31m-[m
[31m-}[m
[1mdiff --git a/gigapede/gigapede/ScreenManager.cs~ b/gigapede/gigapede/ScreenManager.cs~[m
[1mdeleted file mode 100644[m
[1mindex b60b27a..0000000[m
[1m--- a/gigapede/gigapede/ScreenManager.cs~[m
[1m+++ /dev/null[m
[36m@@ -1,70 +0,0 @@[m
[31m-﻿using System;[m
[31m-using System.Collections.Generic;[m
[31m-using System.Linq;[m
[31m-using System.Text;[m
[31m-using Microsoft.Xna.Framework;[m
[31m-using Microsoft.Xna.Framework.Audio;[m
[31m-using Microsoft.Xna.Framework.Content;[m
[31m-using Microsoft.Xna.Framework.GamerServices;[m
[31m-using Microsoft.Xna.Framework.Graphics;[m
[31m-using Microsoft.Xna.Framework.Input;[m
[31m-using Microsoft.Xna.Framework.Media;[m
[31m-[m
[31m-namespace gigapede[m
[31m-{[m
[31m-    /*[m
[31m-     * This class is a top level class managing screen display and resource management.[m
[31m-     * */[m
[31m-    class ScreenManager[m
[31m-    {[m
[31m-	// Screen with input focus.[m
[31m-	Screen focus;[m
[31m-[m
[31m-	// Queue of Screens.[m
[31m-	List<Screen> screenqueue;[m
[31m-[m
[31m-        // The resource manager.[m
[31m-        ResourceManager rm;[m
[31m-[m
[31m-        // The resource manager's accessor.[m
[31m-        public ResourceManager RM[m
[31m-        {[m
[31m-            get { return rm; }[m
[31m-        }[m
[31m-[m
[31m-        // Constructor[m
[31m-        public ScreenManager(GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteb)[m
[31m-        {[m
[31m-            rm = new ResourceManager(graphics, content, spriteb);[m
[31m-        }[m
[31m-[m
[31m-	public AddScreen (Screen new_screen)[m
[31m-	{[m
[31m-	    screenqueue.AddScreen(new_screen)[m
[31m-	}[m
[31m-[m
[31m-	public KillScreen (Screen dead_screen)[m
[31m-	{[m
[31m-	    screenqueue.Remove(dead_screen);[m
[31m-	}[m
[31m-[m
[31m-	public FocusScreen (Screen focus_screen)[m
[31m-	{[m
[31m-	    focus = focus_screen[m
[31m-	}[m
[31m-[m
[31m-	public 