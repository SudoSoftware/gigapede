using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	abstract class DamageableGameItem : GameItem
	{
		protected int currentHealth = GameParameters.MAX_DAMAGEABLE_HEALTH;
		protected RectangleF backupBounds;


		public DamageableGameItem(PointF location) :
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
			return currentHealth / (float)GameParameters.MAX_DAMAGEABLE_HEALTH;
		}



		protected override Microsoft.Xna.Framework.Rectangle GetTextureRectangle()
		{
			return new Microsoft.Xna.Framework.Rectangle(0, 0, (int)GetTexture().Bounds.Width, (int)(GetTexture().Bounds.Height * GetAliveness()));
		}
	}
}
