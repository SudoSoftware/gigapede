using System;

namespace gigapede
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
			using (CentipedeGame game = new CentipedeGame())
            {
                game.Run();
            }
        }
    }
#endif
}

