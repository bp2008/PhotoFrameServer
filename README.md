# PhotoFrameServer

A Windows Service that hosts a simple digital photo frame web page that integrates with the Blue Iris NVR to show camera alerts when motion is detected.

To use the application, run `PhotoFrameServerCmd.exe` and close it once.  This creates the file `Config.cfg`.  Open this in your favorite text editor and modify the settings as necessary.  Then, run `PhotoFrameServerCmd.exe` again or install and run `PhotoFrameServer.exe` which is a background service.

Then, connect your web browser to `http://your-pc-ip-address:8044/`

To show a camera from Blue Iris when motion is detected, open the properties of the camera you want alerts from in Blue Iris.  Go to the **Alerts** tab and configure **Request from a web service** as shown:

![Blue Iris Screenshot](http://i.imgur.com/mEowkIo.png)

Modify the address and port as needed.  If you configure multiple cameras, change the priority in the `When triggered POST text` on each camera to something different.  The highest priority will be shown in the event that more than one camera alert is active simultaneously.

**This is an alpha-quality server** - **Things may not work perfectly**

