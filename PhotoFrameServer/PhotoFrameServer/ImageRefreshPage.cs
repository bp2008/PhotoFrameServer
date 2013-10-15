using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleHttp;

namespace PhotoFrameServer
{
	public class ImageRefreshPage
	{
		public static string GetHtml(HttpProcessor httpProcessor)
		{
			return @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
	<meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
	<meta http-equiv=""cache-control"" content=""max-age=0"" />
	<meta http-equiv=""cache-control"" content=""no-cache"" />
	<meta http-equiv=""expires"" content=""0"" />
	<meta http-equiv=""expires"" content=""Tue, 01 Jan 1980 1:00:00 GMT"" />
	<meta http-equiv=""pragma"" content=""no-cache"" />
	<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
	<title>Photo Frame and Blue Iris Motion Monitor</title>
	<style type=""text/css"">
		body
		{
			background-color: black;
			margin: 0;
			color: Gray;
		}
	</style>
	<script src=""jQuery.js"" type=""text/javascript""></script>
	<script src=""photoframe.js"" type=""text/javascript""></script>
</head>
<body>
	<div id=""rootDiv"">
	</div>
</body>
</html>
";
		}
	}
}
