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
		protected float originalHeight;


		public DamageableGameItem(PointF location) :
			base(location)
		{
			originalHeight = boundingBox.Height;
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			List<GameItemAction> actions = new List<GameItemAction>();

			if (GetAliveness() <= 0)
				Die(ref actions, info);

			return actions;
		}



		protected virtual void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			itemActions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
		}



		public void KillOff()
		{
			currentHealth = 0;
			boundingBox.Height = originalHeight * GetAliveness();
		}



		public virtual void Damage()
		{
			currentHealth--;
			boundingBox.Height = originalHeight * GetAliveness();
		}



		public float GetAliveness()
		{
			return Math.Max(0, currentHealth / (float)GameParameters.MAX_DAMAGEABLE_HEALTH);
		}



		protected override Microsoft.Xna.Framework.Rectangle GetTextureRectangle()
		{
			return new Microsoft.Xna.Framework.Rectangle(0, 0, (int)GetTexture().Bounds.Width, (int)(GetTexture().Bounds.Height * GetAliveness()));
		}
	}
}
