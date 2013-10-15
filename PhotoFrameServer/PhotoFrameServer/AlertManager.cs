using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoFrameServer
{
	public class AlertManager
	{
		List<Alert> alerts = new List<Alert>();
		public void SetAlertStatus(string camId, double priority, bool alertActive)
		{
			lock (alerts)
			{
				for (int i = 0; i < alerts.Count; i++)
					if (alerts[i].cameraShortName == camId)
					{
						alerts.RemoveAt(i);
						i--;
					}
				if (alertActive)
					alerts.Add(new Alert(camId, priority));
			}
		}
		public Alert GetTopActiveAlertOrNull()
		{
			Alert highestAlert = null;
			lock (alerts)
			{
				foreach (Alert alert in alerts)
					if (highestAlert == null || alert.priority > highestAlert.priority)
						highestAlert = alert;
			}
			return highestAlert;
		}
	}
}
