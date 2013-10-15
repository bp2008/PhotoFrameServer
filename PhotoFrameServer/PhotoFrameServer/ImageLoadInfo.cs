using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PhotoFrameServer
{
	public class ImageLoadInfo
	{
		public FileInfo lastImage;
		public DateTime lastLoadTime;
		public ImageLoadInfo(FileInfo lastImage, DateTime lastLoadTime)
		{
			this.lastImage = lastImage;
			this.lastLoadTime = lastLoadTime;
		}
		public ImageLoadInfo GetNext()
		{
			return ImageManager.GetRandomImage();
		}
		public ImageLoadInfo GetPrevious()
		{
			return ImageManager.GetRandomImage();
		}
	}
}
