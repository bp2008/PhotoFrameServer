using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoFrameServer
{
	public class Alert
	{
		public string cameraShortName;
		public double priority;
		public Alert(string cameraShortName, double priority = 1)
		{
			this.cameraShortName = cameraShortName;
			this.priority = priority;
		}
	}
}
