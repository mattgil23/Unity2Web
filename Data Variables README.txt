Data Variables used in the Project:
 
Class: DataManager ->
"dataPath", type: string, purpose: Store path of headset.txt
"fileName", type: string, purpose: Store name of file for headset id
"HeadsetPath", type: string, purpose: Store JSON format of headset data
"gameDataProjectFilePath", type: string, purpose: Store JSON format of the user data related to experience.
"myFirstNameText", type: InputField, purpose: Store User first name
"myLastNameText", type: InputField, purpose: Store user's last name
"myHeadsetNumberText", type: InputField, purpose: Store headset number id used by that user
"myGradeText", type: InputField, purpose: Store grade received by user for that experience 
"myPassingInformationToggle", type: Boolean Toggle button, purpose: Store information if user passed the experience
"myCompletionInformationToggle", type: Boolean Toggle button, purpose: Store information if user completed the experience
"getDataButton", type: Button, purpose: Button used to receive data from server 
"sendDataButton", type: Button, purpose: Button used to send updated data to server
"json", type: string, purpose: Store MyClassData json format
"myData", type: MyClassData, purpose: Store object of MyClassData 
"myDataPost", type: MyClassData, purpose: Store object of MyClassData
"Hdata", type: ClassHeadset, purpose: Store object of ClassHeadset
"Headset", type: string, purpose: Store Headset ID
"myGetEndpoint", type: string, purpose: Store the endpoint to get data
"myPushEndpoint", type: string, purpose: Store the endpoint to send data
"devModeWin", type: boolean, purpose: Check if in development mode for Windows
"pathW", type: string, purpose: Path to the headset files for Windows
"devModeAnd", type: boolean, purpose: Check if in development mode for Android
"pathA", type: string, purpose: Path to the headset files for Android
"dateFormat", type: string, purpose: Store date when experience performed
"myTexture", type: Texture, purpose: Store image of user
"m_Path", type: string, purpose: Main Path when the application apk/exe is stored
"Url", type: string, purpose: URL to fetch image of user
"combine_path", type: string, purpose:Combine the main path and Image of user URL to finally store it on system
"path", type: string, purpose: Get path to desktop. Only for Windows mode
"myLog", type: string, purpose: Display log on screen for debugging
--------------------------------------------------------------------------------

Class: MyClassData ->
"DateLastAttempted", type: string, purpose: Store the date when the user last attempted the experience
"Grade", type: int, purpose: Grade obtained by the user in the experience
"PassorFail", type: boolean, purpose: Stores if the user passed or failed the experience
"CompletedBefore", type: boolean, purpose: Stores if the user completed the experience previously
"ExperienceNumber", type: string, purpose: Store experience number
"HeadSetNumber", type: string, purpose: Store headset number
"UserFirstName", type: string, purpose: Store user first name
"UserLastName", type: string, purpose: Store user last name
"UserNumber", type: string, purpose: Stores user ticket number
"ConfigId", type: string, purpose: Stores Config ID passed from server
--------------------------------------------------------------------------------