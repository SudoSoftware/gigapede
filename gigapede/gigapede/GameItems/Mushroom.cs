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
		private const int MAX_HEALTH = 5;
		internal int currentHealth = MAX_HEALTH;
		public static Texture2D texture;
		//private RectangleF backupBounds;

		public Mushroom(PointF location) :
			base(location)
		{
			//backupBounds = boundingBox;
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			//boundingBox.Height = backupBounds.Height * getAliveness();

			List<GameItemAction> actions = new List<GameItemAction>();
			
			if (currentHealth <= 0)
				actions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
			
			return actions;
		}



		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				GetTexture(),
				new Microsoft.Xna.Framework.Rectangle((int)boundingBox.X, (int)boundingBox.Y, (int)boundingBox.Width, (int)(boundingBox.Height * getAliveness())),
				new Microsoft.Xna.Framework.Rectangle(0, 0, (int)texture.Bounds.Width, (int)(texture.Bounds.Height * getAliveness())),
				Microsoft.Xna.Framework.Color.White
			);
		}



		public float getAliveness()
		{
			return currentHealth / (float)MAX_HEALTH;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
