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
		private int highScore = 0;
		private PersistanceManager manager = new PersistanceManager("highScores.xml");
		private List<GameAlert> scoringAlerts = new List<GameAlert>();
		private int padding = 10;
		private bool attactMode = false;
		private Shooter player;


		public HeadsUpDisplay(ref Shooter player)
		{
			this.player = player;
			highScore = Convert.ToInt32(manager.Load());
		}



		public void IndicateLostLife()
		{
			//livesLeft--;
		}



		public void SetAttactMode(bool mode)
		{
			attactMode = mode;
		}



		public void IndicateAdditionalPoints(int pointValue, GameItem source)
		{
			if (player.GetCurrentScore() > highScore)
			{
				highScore = player.GetCurrentScore();
				manager.Save(highScore);
			}

			PointF itemLocation = source.GetLocation();
			scoringAlerts.Add(new GameAlert(new Vector2(itemLocation.X, itemLocation.Y), pointValue));
		}



		public void Update()
		{
			List<GameAlert> expiredAlerts = new List<GameAlert>();

			foreach (GameAlert alert in scoringAlerts)
				if (alert.HasExpired())
					expiredAlerts.Add(alert);

			foreach (GameAlert alert in expiredAlerts)
				scoringAlerts.Remove(alert);
		}



		public void Draw(SpriteBatch spriteBatch)
		{
			DrawScores(spriteBatch);
			DrawLife(spriteBatch);
			DrawAlerts(spriteBatch);

			if (attactMode)
				DrawAttractMode(spriteBatch);
		}



		private void DrawScores(SpriteBatch spriteBatch)
		{
			String scoreStr = "Score: " + player.GetCurrentScore();
			spriteBatch.DrawString(
				font,
				scoreStr,
				new Microsoft.Xna.Framework.Vector2(padding, padding),
				Microsoft.Xna.Framework.Color.Wheat
			);

			String highScoreStr = "Record: " + highScore;
			spriteBatch.DrawString(
				font,
				highScoreStr,
				new Microsoft.Xna.Framework.Vector2(GameParameters.TARGET_RESOLUTION.Width / 2 - font.MeasureString(highScoreStr).X / 3, padding),
				Microsoft.Xna.Framework.Color.Wheat
			);
		}



		private void DrawLife(SpriteBatch spriteBatch)
		{
			String livesLeftStr = "Lives: " + player.GetLivesLeft();
			spriteBatch.DrawString(
				font,
				livesLeftStr,
				new Microsoft.Xna.Framework.Vector2(GameParameters.TARGET_RESOLUTION.Width - font.MeasureString(livesLeftStr).X - padding, padding),
				Microsoft.Xna.Framework.Color.Wheat
			);
		}



		private void DrawAlerts(SpriteBatch spriteBatch)
		{
			foreach (GameAlert alert in scoringAlerts)
			{
				spriteBatch.DrawString(
					font, //may want to use a seperate and smaller font
					alert.value + "",
					alert.location,
					Microsoft.Xna.Framework.Color.Green
				);
			}
		}



		private void DrawAttractMode(SpriteBatch spriteBatch)
		{
			String attactText = "Check out this cool Gigapede game!";
			spriteBatch.DrawString(
				font,
				attactText,
				new Microsoft.Xna.Framework.Vector2(GameParameters.TARGET_RESOLUTION.Width - font.MeasureString(attactText).X - padding, padding + 100),
				Microsoft.Xna.Framework.Color.Wheat
			);
		}



		public class GameAlert
		{
			public Vector2 location;
			public int value;
			private DateTime spawnTime;


			public GameAlert(Vector2 location, int value)
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
