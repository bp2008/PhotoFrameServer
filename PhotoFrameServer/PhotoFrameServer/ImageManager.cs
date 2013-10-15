using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace PhotoFrameServer
{
	public class ImageManager
	{
		private AlertManager alertManager;

		int millisecondsBetweenPhotoUpdates = Math.Max(0, PhotoFrameWrapper.cfg.secondsBetweenPhotos * 1000);
		/// <summary>
		/// Keeps track of the last image sent to each client, and the time it was sent.
		/// </summary>
		private ConcurrentDictionary<string, ImageLoadInfo> lastLoadedImages = new ConcurrentDictionary<string, ImageLoadInfo>();
		/// <summary>
		/// Keeps track of the time remaining until the next image from the photo frame should be sent.  Only used during alerts, so the photo frame state is preserved while the alert is active.
		/// </summary>
		private ConcurrentDictionary<string, TimeSpan> nextPhotoFrameImageDelay = new ConcurrentDictionary<string, TimeSpan>();

		public ImageManager(AlertManager alertManager)
		{
			this.alertManager = alertManager;
		}

		/// <summary>
		/// Returns an image when it is deemed necessary to send it to the client.
		/// </summary>
		/// <returns></returns>
		public byte[] GetImageAuto(SimpleHttp.HttpProcessor p)
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			while (!PhotoFrameWrapper.isStopping)
			{
				Alert topAlert = alertManager.GetTopActiveAlertOrNull();
				if (topAlert != null)
				{
					// We are going to send an alert image.  But first, make sure the photo frame state is preserved.
					if (!nextPhotoFrameImageDelay.ContainsKey(p.RemoteIPAddress))
					{
						// No state has been preserved.  Preserve it!
						ImageLoadInfo ili;
						TimeSpan span = TimeSpan.Zero;
						if (lastLoadedImages.TryGetValue(p.RemoteIPAddress, out ili))
						{
							TimeSpan timeToWait = TimeSpan.FromMilliseconds(millisecondsBetweenPhotoUpdates);
							TimeSpan timeWaitedAlready = DateTime.Now - ili.lastLoadTime;
							TimeSpan timeRemainingToWait = timeToWait - timeWaitedAlready;
							if (timeRemainingToWait < TimeSpan.Zero)
								timeRemainingToWait = TimeSpan.Zero;
							span = timeRemainingToWait;
						}
						nextPhotoFrameImageDelay.AddOrUpdate(p.RemoteIPAddress, span, (s, ts) => { return span; });
					}

					byte[] imgData = SimpleProxy.GetData(PhotoFrameWrapper.cfg.blueIrisAddress + "image/" + topAlert.cameraShortName);
					return imgData;
				}
				else
				{
					// Determine if it is time to move to another photo.
					ImageLoadInfo ili;
					if (lastLoadedImages.TryGetValue(p.RemoteIPAddress, out ili))
					{
						// Restore state if necessary
						TimeSpan span;
						if (nextPhotoFrameImageDelay.TryGetValue(p.RemoteIPAddress, out span))
						{
							// Restore the state
							nextPhotoFrameImageDelay.TryRemove(p.RemoteIPAddress, out span);

							TimeSpan timeToWait = TimeSpan.FromMilliseconds(millisecondsBetweenPhotoUpdates);
							TimeSpan timeWaitedAlready = span;
							TimeSpan timeRemainingToWait = timeToWait - timeWaitedAlready;
							ili.lastLoadTime = DateTime.Now - timeRemainingToWait;
							lastLoadedImages.AddOrUpdate(p.RemoteIPAddress, ili, (o1, o2) => { return ili; });

							// A motion alert just ended, so send the photo frame image to the client again.
							byte[] restoreImageData = File.ReadAllBytes(ili.lastImage.FullName);
							return restoreImageData;
						}
						if (ili.lastLoadTime.AddMilliseconds(millisecondsBetweenPhotoUpdates) > DateTime.Now)
						{
							// Not yet time.
							Thread.Sleep(100);
							continue;
						}
						else
						{
							// There was a previous image and it is time to move on.
							ili = ili.GetNext();
						}
					}
					else
					{
						// No previous image
						ili = GetRandomImage();
					}
					if (ili == null)
						return new byte[0];

					ili.lastLoadTime = DateTime.Now;
					lastLoadedImages.AddOrUpdate(p.RemoteIPAddress, ili, (o1, o2) => { return ili; });
					// If we get here, then we need to send a photo frame image to the client.
					byte[] imgData = File.ReadAllBytes(ili.lastImage.FullName);
					return imgData;
				}
			}
			return new byte[0];
		}

		public static ImageLoadInfo GetRandomImage()
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			DirectoryInfo di = new DirectoryInfo(PhotoFrameWrapper.cfg.photoDirectory);
			FileInfo[] files = di.GetFiles("*.jpg", SearchOption.AllDirectories);
			watch.Stop();
			Console.WriteLine("Directory scan took " + watch.Elapsed.ToString());

			if (files.Length < 1)
				return null;

			Random rand = new Random();
			int index = rand.Next(0, files.Length);
			return new ImageLoadInfo(files[index], DateTime.Now);
		}
	}
}
