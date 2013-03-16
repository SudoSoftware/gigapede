using System;

namespace gigapede
{
	#if WINDOWS || XBOX

	static class Program
	{
		static void Main(string[] args)
		{
			using (CentipedeEntry game = new CentipedeEntry())
			{
				game.Run();
			}
		}
	}

	#endif
}

