﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.Resources;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace gigapede
{
	class HeadsUpDisplay
	{
		public static SpriteFont font;
		private int livesLeft = GameParameters.MAX_LIVES;
		private int currentScore = 0;
		private int highScore = 0;
		private PersistanceManager manager = new PersistanceManager("highScores.xml");


		public HeadsUpDisplay()
		{
			highScore = Convert.ToInt32(manager.Load());
		}


		public void IndicateLostLife()
		{
			livesLeft--;
		}



		public void AddToScore(int additionalPoints)
		{
			currentScore += additionalPoints;

			if (currentScore > highScore)
			{
				highScore = currentScore;
				manager.Save(highScore);
			}
		}



		public void Draw(SpriteBatch spriteBatch)
		{
			int y = 10;

			spriteBatch.DrawString(
				font,
				currentScore + "",
				new Microsoft.Xna.Framework.Vector2(30, y),
				Color.Wheat);

			spriteBatch.DrawString(
				font,
				highScore + "",
				new Microsoft.Xna.Framework.Vector2(GameParameters.TARGET_RESOLUTION.Width / 2, y),
				Color.Wheat);
		}
	}
}
