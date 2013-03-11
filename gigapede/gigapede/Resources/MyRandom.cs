using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace gigapede
{
	/// <summary>
	/// Expands upon some of the features the .NET Random class does:
	/// 
	/// NextRange : Generate a random number within some range
	/// NextGaussian : Generate a normally distributed random number
	/// 
	/// </summary>
	class MyRandom : Random
	{
		/// <summary>
		/// Generates a random number in the range of [Min,Max]
		/// </summary>
		public float nextRange(float Min, float Max)
		{
			return MathHelper.Lerp(Min, Max, (float)this.NextDouble());
		}

		/// <summary>
		/// Generate a random vector about a unit circle
		/// </summary>
		public Vector2 nextCircleVector()
		{
			float Angle = (float)(this.NextDouble() * 2.0 * Math.PI);
			float x = (float)Math.Cos(Angle);
			float y = (float)Math.Sin(Angle);

			return new Vector2(x, y);
		}

		/// <summary>
		/// Generate a normally distributed random number.  Derived from a Wiki reference on
		/// how to do this.
		/// </summary>
		public double nextGaussian(double Mean, double StdDev)
		{
			if (this.m_usePrevious)
			{
				this.m_usePrevious = false;
				return Mean + m_y2 * StdDev;
			}
			this.m_usePrevious = true;

			double x1 = 0.0;
			double x2 = 0.0;
			double y1 = 0.0;
			double z = 0.0;

			do
			{
				x1 = 2.0 * this.NextDouble() - 1.0;
				x2 = 2.0 * this.NextDouble() - 1.0;
				z = (x1 * x1) + (x2 * x2);
			}
			while (z >= 1.0);

			z = Math.Sqrt((-2.0 * Math.Log(z)) / z);
			y1 = x1 * z;
			m_y2 = x2 * z;

			return Mean + y1 * StdDev;
		}

		/// <summary>
		/// Keep this around to optimize gaussian calculation performance.
		/// </summary>
		private double m_y2;
		private bool m_usePrevious { get; set; }
	}
}
