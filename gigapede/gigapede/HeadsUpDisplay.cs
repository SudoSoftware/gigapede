using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigapede.Resources;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using gigapede.GameItems;
using System.Drawing;

namespace gigapede
{
	class HeadsUpDisplay
	{
		public static SpriteFont font;
		private int livesLeft = GameParameters.MAX_LIVES;
		private int currentScore = 0;
		private int highScore = 0;
		private PersistanceManager manager = new PersistanceManager("highScores.xml");
		private List<ScoreAlert> scoringAlerts = new List<ScoreAlert>();


		public HeadsUpDisplay()
		{
			highScore = Convert.ToInt32(manager.Load());
		}



		public void IndicateLostLife()
		{
			livesLeft--;
		}



		public void AddToScore(int pointValue, GameItem source)
		{
			currentScore += pointValue;

			if (currentScore > highScore)
			{
				highScore = currentScore;
				manager.Save(highScore);
			}

			PointF itemLocation = source.GetLocation();
			scoringAlerts.Add(new ScoreAlert(new Vector2(itemLocation.X, itemLocation.Y), pointValue));
		}



		public void Update()
		{
			List<ScoreAlert> expiredAlerts = new List<ScoreAlert>();

			foreach (ScoreAlert alert in scoringAlerts)
				if (alert.HasExpired())
					expiredAlerts.Add(alert);

			foreach (ScoreAlert alert in expiredAlerts)
				scoringAlerts.Remove(alert);
		}



		public void Draw(SpriteBatch spriteBatch)
		{
			DrawScores(spriteBatch);
			DrawAlerts(spriteBatch);
		}



		private void DrawScores(SpriteBatch spriteBatch)
		{
			int y = 10;

			spriteBatch.DrawString(
				font,
				currentScore + "",
				new Microsoft.Xna.Framework.Vector2(30, y),
				Microsoft.Xna.Framework.Color.Wheat);

			spriteBatch.DrawString(
				font,
				highScore + "",
				new Microsoft.Xna.Framework.Vector2(GameParameters.TARGET_RESOLUTION.Width / 2, y),
				Microsoft.Xna.Framework.Color.Wheat);
		}



		private void DrawAlerts(SpriteBatch spriteBatch)
		{
			foreach (ScoreAlert alert in scoringAlerts)
			{
				spriteBatch.DrawString(
				font,
				alert.value + "",
				alert.location,
				Microsoft.Xna.Framework.Color.Wheat);
			}
		}



		public class ScoreAlert
		{
			public Vector2 location;
			public int value;
			private DateTime spawnTime;


			public ScoreAlert(Vector2 location, int value)
			{
				this.location = location;
				this.value = value;
				this.spawnTime = DateTime.Now;
			}



			public bool HasExpired()
			{
				TimeSpan timeDiff = DateTime.Now.Subtract(spawnTime);
				return timeDiff.Seconds * 1000 + timeDiff.Milliseconds > GameParameters.ALERT_MILLISECONDS;
			}
		}
	}
}
