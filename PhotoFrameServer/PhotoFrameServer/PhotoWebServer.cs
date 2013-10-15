using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleHttp;
using System.IO;
using System.Net;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PhotoFrameServer
{
	public class PhotoWebServer : HttpServer
	{
		public Config cfg;
		AlertManager alertManager;
		ImageManager imageManager;
		public PhotoWebServer(Config cfg)
			: base(cfg.webPort)
		{
			this.cfg = cfg;

			alertManager = new AlertManager();
			imageManager = new ImageManager(alertManager);
		}
		public override void handleGETRequest(HttpProcessor p)
		{
			try
			{
				string requestedPage = p.request_url.AbsolutePath.TrimStart('/');
				
				if (requestedPage == "image")
				{
					byte[] imgData = imageManager.GetImageAuto(p);
					Console.WriteLine("Sending image at " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
					p.writeSuccess("image/jpeg", imgData.Length);
					p.outputStream.Flush();
					p.rawOutputStream.Write(imgData, 0, imgData.Length);
					p.rawOutputStream.Flush();
				}
				else if (requestedPage == "")
				{
					p.writeSuccess();
					p.outputStream.Write(ImageRefreshPage.GetHtml(p));
				}
				else if (requestedPage == "jQuery.js")
				{
					p.writeSuccess();
					p.outputStream.Write(Javascript.JQuery());
				}
				else if(requestedPage == "photoframe.js")
				{
					p.writeSuccess();
					p.outputStream.Write(Javascript.PhotoFrame());
				}
			}
			catch (Exception ex)
			{
				if (!p.isOrdinaryDisconnectException(ex))
					Logger.Debug(ex);
			}
		}

		public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
		{
			try
			{
				string requestedPage = p.request_url.AbsolutePath.TrimStart('/');

				string camId = p.GetPostParam("cam");
				double priority = p.GetPostDoubleParam("priority", 1);
				if (requestedPage == "alert")
				{
					alertManager.SetAlertStatus(camId, priority, true);
					Console.WriteLine(camId + " " + priority + " active");
					p.writeSuccess();
				}
				else if (requestedPage == "reset")
				{
					alertManager.SetAlertStatus(camId, priority, false);
					Console.WriteLine(camId + " " + priority + " reset");
					p.writeSuccess();
				}
			}
			catch (Exception ex)
			{
				if (!p.isOrdinaryDisconnectException(ex))
					Logger.Debug(ex);
			}
		}

		public override void stopServer()
		{
		}
	}
}
