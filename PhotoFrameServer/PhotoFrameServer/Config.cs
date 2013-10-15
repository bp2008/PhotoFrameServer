using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoFrameServer
{
	public class Config : SerializableObjectBase
	{
		public int webPort = 8044;
		public int secondsBetweenPhotos = 15;
		public string photoDirectory = "C:\\Users\\USER_NAME\\Pictures\\";
		public string blueIrisAddress = "http://localhost/";
	}
}
