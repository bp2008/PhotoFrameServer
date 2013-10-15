using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace PhotoFrameServer
{
	public partial class PhotoFrameService : ServiceBase
	{
		public PhotoFrameService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			PhotoFrameWrapper.Start();
		}

		protected override void OnStop()
		{
			PhotoFrameWrapper.Stop();
		}
	}
}
