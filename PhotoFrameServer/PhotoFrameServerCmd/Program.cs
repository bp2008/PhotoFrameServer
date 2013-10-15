using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoFrameServer;

namespace PhotoFrameServerCmd
{
	class Program
	{
		static void Main(string[] args)
		{
			PhotoFrameWrapper.Start();
			Console.WriteLine("Photo Frame Server started on port " + PhotoFrameWrapper.cfg.webPort);
			Console.WriteLine("Press Enter to stop the server");
			Console.ReadLine();
			Console.WriteLine("Shutting down the Photo Frame Server");
			PhotoFrameWrapper.Stop();
			Console.WriteLine("Waiting for threads to stop");
		}
	}
}
