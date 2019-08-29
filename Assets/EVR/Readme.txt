Plan for this tool..... ~Matt 8/29/2019 @ EnhanceVR


Overall Goal:

To test sending simple data from the webtool in a json format and process that correctly to 
display or use that information in the unity experience. Equally, when the experience is over (in this
case when new data is modified in the UI elements) we can send the data to the webtool.

Fuctionality of this tool:

When the user presses "Get Data" Button, a url "endpoint" will be accessed from the webtool
that already has the appropriate json file ready. The "Headset" number field is suppose to be set at a 
number agreed upon by unity and the webtool as the headset ID (this way we can use multiple headsets
at the sametime). This ID number will be part of the url in a way to point to an "endpoint" just for
that headset. 

The json data will be turned into the appropriate C# class --> see MyClassData.cs in the EVR folder in Assets

This data will be loaded on the screen to see if Unity knows if everything is setup correctly.

Viceversa - the user can edit the data fields in the UI and click "Send Data" which will take that data and send it to 
the webtool at the correct endpoint as well.


Notes: 

-See links sent by matt via Slack for Json information and using Unity Webtool, Jsonutility, etc
-You should only really need 1-3 scripts to do all of this...

Ping Matt with any questions! :) Happy coding!

-Matt