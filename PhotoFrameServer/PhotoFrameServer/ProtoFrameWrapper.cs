using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoFrameServer
{
	public static class PhotoFrameWrapper
	{
		public static Config cfg;
		static PhotoWebServer pws;
		public static bool isStopping = false;
		static PhotoFrameWrapper()
		{
			System.Net.ServicePointManager.Expect100Continue = false;
			System.Net.ServicePointManager.DefaultConnectionLimit = 640;

			cfg = new Config();
			cfg.Load(Globals.ConfigFilePath);
			cfg.blueIrisAddress = cfg.blueIrisAddress.TrimEnd('/') + '/';
			cfg.Save(Globals.ConfigFilePath);

			SimpleHttp.SimpleHttpLogger.RegisterLogger(Logger.httpLogger);

			pws = new PhotoWebServer(cfg);
		}
		public static void Start()
		{
			isStopping = false;
			pws.Start();
		}
		public static void Stop()
		{
			isStopping = true;
			pws.Stop();
		}
	}
}
