using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PhotoFrameServer
{
	public static class Globals
	{
		private static string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
		/// <summary>
		/// Gets the full path to the current executable file.
		/// </summary>
		public static string ExecutablePath
		{
			get { return executablePath; }
		}
		private static string applicationRoot = new FileInfo(executablePath).Directory.FullName;
		/// <summary>
		/// Gets the full path to the root directory where the current executable is located.  Does not have trailing '\\'.
		/// </summary>
		public static string ApplicationRoot
		{
			get { return applicationRoot; }
		}
		private static string applicationDirectoryBase = applicationRoot + "\\";
		/// <summary>
		/// Gets the full path to the root directory where the current executable is located.  Includes trailing '\\'.
		/// </summary>
		public static string ApplicationDirectoryBase
		{
			get { return applicationDirectoryBase; }
		}
		private static string configFilePath = applicationDirectoryBase + "Config.cfg";
		/// <summary>
		/// Gets the full path to the config file.
		/// </summary>
		public static string ConfigFilePath
		{
			get { return configFilePath; }
		}
		public static string Version = "1.0";
	}
}
