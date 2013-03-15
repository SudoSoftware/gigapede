using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Powerup : GameItem
	{
		public static Texture2D texture;
		private PowerupType type;
		private float verticalVelocity = GameParameters.POWERUP_INITIAL_UPWARD_THRUST;

		public enum PowerupType
		{
			ROCKET_BOOST, EXTRA_LIFE
		}


		public Powerup(PointF location, PowerupType type) :
			base(location)
		{
			this.type = type;
		}



		public override List<GameItemAction> Update(InfoForItem info)
		{
			boundingBox.Y += info.gameTime.ElapsedGameTime.Milliseconds * verticalVelocity;
			verticalVelocity += GameParameters.GRAVITY;

			List<GameItemAction> itemActions = new List<GameItemAction>();

			foreach (GameItem contact in info.contacts)
				if (contact.GetType() == typeof(Shooter))
					PowerupShooter((Shooter)contact, ref itemActions);

			return itemActions;
		}



		private void PowerupShooter(Shooter shooter, ref List<GameItemAction> itemActions)
		{
			if (!shooter.IsPoweredUp())
			{
				shooter.Powerup();
				itemActions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));
			}
		}



		public override bool IsMovable()
		{
			return true;
		}



		public override Texture2D GetTexture()
		{
			return texture;
		}
	}
}
