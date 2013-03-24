using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using gigapede.Resources;

namespace gigapede.GameItems
{
	class Flea : DamageableGameItem
	{
		public static Texture2D texture;
		private MyRandom prng = new MyRandom();


		public Flea(PointF location) :
			base(location)
		{ }



		public override List<GameItemAction> Update(InfoForItem info)
		{
			float speed = (GetAliveness() == 1) ? GameParameters.FLEA_SPEED : GameParameters.FLEA_SPEED * 3; //if damaged, speed is "lighting fast"
			float theta = info.gameTime.ElapsedGameTime.Milliseconds * speed;

			List<GameItemAction> itemActions = new List<GameItemAction>();

			PossiblySpawnMushroom(boundingBox.Y, boundingBox.Y + theta, ref itemActions, info.world);
			boundingBox.Y += theta;

			if (!info.world.IsLegalLocation(boundingBox)) //if flea out of bounds, remove from world
				itemActions.Add(new GameItemAction(GameItemAction.Action.REMOVE_ITEM, this));

			return itemActions;
		}



		//if chance agrees that a mushroom should be spawned, only spawn it in the grid pattern 
		private void PossiblySpawnMushroom(float currentY, float nextY, ref List<GameItemAction> itemActions, World world)
		{
			int rowIn = (int)currentY / (int)GameParameters.DEFAULT_ITEM_HEIGHT;
			int rowWillBeIn = (int)nextY / (int)GameParameters.DEFAULT_ITEM_HEIGHT;
			
			if (rowIn != rowWillBeIn && prng.nextRange(0, 3) <= 1)
			{
				PointF mushroomLoc = new PointF(boundingBox.X, rowWillBeIn * GameParameters.DEFAULT_ITEM_HEIGHT);
			
				if (world.ItemAt(mushroomLoc) == null)
					itemActions.Add(new GameItemAction(GameItemAction.Action.ADD_ITEM, new Mushroom(mushroomLoc)));
			}
		}



		public static bool SpawnIsAppropriate(World world)
		{
			int count = 0;
			List<GameItem> allMushrooms = world.GetAllItemsOfType(typeof(Mushroom));
			foreach (GameItem mushroom in allMushrooms)
				if (mushroom.GetLocation().Y >= Shooter.minY)
					count++;

			return count < 5;
		}



		public override void Damage()
		{
			currentHealth -= 2;
			boundingBox.Height = originalHeight * GetAliveness();
		}



		protected override void Die(ref List<GameItemAction> itemActions, InfoForItem info)
		{
			((Shooter)info.world.GetItemOfType(typeof(Shooter))).AddToScore(GameParameters.FLEA_POINTS, this, info.world.getHUD());
			base.Die(ref itemActions, info);
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
