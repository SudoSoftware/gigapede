using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace gigapede.GameItems
{
	class Mushroom: GameItem
	{
		private const int MAX_HEALTH = 4;
		private int currentHealth = MAX_HEALTH;

		public static Texture2D texture;
		private RectangleF backupBounds;


		public Mushroom(PointF location) :
			base(location)
		{
			backupBounds = boundingBox;
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (currentHealth <= 0)
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
			
			return actions;
		}



		public void Damage()
		{
			currentHealth--;
			boundingBox.Height = backupBounds.Height * GetAliveness();
		}



		public float GetAliveness()
		{
			return currentHealth / (float)MAX_HEALTH;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}



		protected override Microsoft.Xna.Framework.Rectangle GetTextureRectangle()
		{
			return new Microsoft.Xna.Framework.Rectangle(0, 0, (int)texture.Bounds.Width, (int)(texture.Bounds.Height * GetAliveness()));
		}
	}
}
