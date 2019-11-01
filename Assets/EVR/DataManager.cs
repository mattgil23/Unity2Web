using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;
using System.Web;
using System.Net;

public class DataManager : MonoBehaviour
{

    [Header("DataPaths")]
    //public string Url = "file:///D://EVR//Projects//U2Web//Unity2Web//Assets//EVR//login.png";
    //public string dataPath = "C:\\Users\\EnhanceVR\\Desktop\\headset.txt";
    string dataPath;
    string fileName = "headset.txt";
    //public string Url = "file:///C://Users//candi//Desktop//COURSES//Sem 3//EVR//UnityToWeb//Assets//EVR//login.png";
    //public string dataPath = "C:\\Users\\candi\\Desktop\\COURSES\\Sem 3\\EVR\\headset.txt";
    private string HeadsetPath = "/EVR/Hdata.json";
    private string gameDataProjectFilePath = "/EVR/data.json";
    private string gameDataFileName = "/EVR/data.json";

    [Header("User Information")]
    public InputField myFirstNameText;
    public InputField myLastNameText;
    public InputField myHeadsetNumberText;
    public InputField myGradeText;
    public Toggle myPassingInformationToggle;
    public Toggle myCompletionInformationToggle;
    public Button getDataButton;
    public Button sendDataButton;
    private string json;

    [Header("Class Data")]
    public MyClassData myData;
    public MyClassData myDataPost;
    public ClassHeadset Hdata;
    private string Headset = "";
    private string myGetEndpoint = "";
    private string myPushEndpoint = "";

    //get current date
    System.DateTime date = System.DateTime.Now;
    string dateFormat;
    Texture2D myTexture;
    string m_Path;
    string Url;
    string combine_path;

    string path;

    string myLog;
    Queue myLogQueue = new Queue();

    // Start is called before the first frame update
    void Start()
    {
        //initialize json file to default null values
        myFirstNameText.text = "";
        myData.UserNumber = "";
        myData.ConfigId = "";
        myData.ExperienceNumber = "";
        myData.HeadSetNumber = "";
        myData.UserFirstName = "";
        myData.UserLastName = "";
        myData.PassorFail = false;
        myData.Grade = 0;
        myData.DateLastAttempted = "";
        myData.CompletedBefore = false;

        dateFormat =  date.Month + "/" + date.Day + "/" + date.Year;
        


        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //Debug.Log("Windows Platform");
            
            m_Path = Application.dataPath;
            Debug.Log(m_Path);
            //Output the Game data path to the console
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            dataPath = Path.GetFullPath(fileName);
            Debug.Log(dataPath);
            //Path.Combine("/First/Path/To", "Some/File/At/foo.txt");
            Url = "file:///" + m_Path + "//EVR//login.png";
            combine_path= m_Path + "//EVR//login.png";

            //Access the Headset Json to assign values
            GetID();

            //When Get or Post
            getDataButton.onClick.AddListener(GetData);
            sendDataButton.onClick.AddListener(SendData);
        }

        if ( Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //Debug.Log("Windows Platform");

            m_Path = Application.dataPath;
            Debug.Log(m_Path);
            //Output the Game data path to the console
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            dataPath = Path.GetFullPath(fileName);
            Debug.Log(dataPath);
            //Path.Combine("/First/Path/To", "Some/File/At/foo.txt");
            Url = "file:///" + m_Path + "/login.png";
            combine_path = m_Path + "/login.png";

            //Access the Headset Json to assign values
            GetID();

            //When Get or Post
            getDataButton.onClick.AddListener(GetData);
            sendDataButton.onClick.AddListener(SendData);
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Android Platform");
            m_Path = Application.persistentDataPath;
            //Output the Game data path to the console
            Debug.Log("Path : " + m_Path);
            dataPath = m_Path +  Path.GetFullPath(fileName);
            Debug.Log(dataPath);
            Url = "file:///" + m_Path + "/login.png";
            combine_path = m_Path + "/login.png";
            //Access the Headset Json to assign values
            GetID_Android();

            //When Get or Post
            getDataButton.onClick.AddListener(GetData_Android);
            sendDataButton.onClick.AddListener(SendData_Android);
        }

    }

    void CopyFile( string file, string file_noext)
    {
        string fileName = "/EVR/" + file;
        if (!File.Exists(fileName))
        {
            TextAsset resourceFile = Resources.Load(file_noext) as TextAsset;
            FileStream f = new FileStream(fileName, FileMode.Create);
            foreach (byte b in resourceFile.bytes)
            {
                f.WriteByte(b);
            }
            f.Close();
        }
    }

    //void OnEnable()
    //{
    //    Application.logMessageReceived += HandleLog;
    //}

    //void OnDisable()
    //{
    //    Application.logMessageReceived -= HandleLog;
    //}
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    void OnGUI()
    {
        GUILayout.Label(myLog);
    }


    //Get the Headset Id and Endpoints and save to JSON
    void GetID()
    {
        StreamReader inp_stm = new StreamReader(dataPath);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            //Debug.Log(inp_ln);
            Hdata = JsonUtility.FromJson<ClassHeadset>(inp_ln);
            inp_ln = JsonUtility.ToJson(Hdata);
            //CopyFile("Hdata.json", "Hdata");
            //var HeadsetPath = Resources.Load<TextAsset>("EVR/Hdata");
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                HeadsetPath = "/Hdata.json";
            }
            string filePath = Application.dataPath + HeadsetPath;
            Debug.Log(filePath);
            File.WriteAllText(filePath, inp_ln);
        }
        inp_stm.Close();

        Headset = Hdata.HeadsetID;
        myGetEndpoint = Hdata.GetEndPoint;
        myPushEndpoint = Hdata.PostEndPoint;

    }

    void GetData()
    {
        Debug.Log("Trying To Get the Data!");
        
        //Appending headset information to the URL for GET request
        
        var uriBuilder = new UriBuilder(myGetEndpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["HeadSetNumber"] = Headset;
        uriBuilder.Query = query.ToString();
        myGetEndpoint = uriBuilder.ToString();
        StartCoroutine(GetRequest(myGetEndpoint));

        //save image from url to local disk
       
    }

    IEnumerator GetRequest(string uri)
    {
        WebClient webClient = new WebClient();
        //webClient.DownloadFile("http://evr-demo.herokuapp.com/images/login.png", "C:\\Users\\candi\\Desktop\\COURSES\\Sem 3\\EVR\\UnityToWeb\\Assets\\EVR\\login.png");
        webClient.DownloadFile("http://evr-demo.herokuapp.com/images/login.png", combine_path);
        Debug.Log("Profile pic image" + combine_path);
        WWW www = new WWW(Url);
        while (!www.isDone)
            yield return null;
        GameObject image = GameObject.Find("RawImage");
        image.GetComponent<RawImage>().texture = www.texture;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            Debug.Log("GET END PT" + uri);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }

            else
            {
                string dataAsJson = webRequest.downloadHandler.text.ToString();

                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                myData = JsonUtility.FromJson<MyClassData>(dataAsJson);

                //1. print the results received from GET
                Debug.Log("Ticket: " + myData.ConfigId + " \nExperience Number: " + myData.ExperienceNumber + " \nHead Set Number: " + myData.HeadSetNumber + " \nUser First Name: " + myData.UserFirstName + " \nUser Last Name: " + myData.UserLastName + " \nPassed? " + myData.PassorFail + " \nGrade: " + myData.Grade + " \nPrevious Date: " + myData.DateLastAttempted + " \nIs it Completed Before: " + myData.CompletedBefore);

                //Updating the Text fields in UI
                myFirstNameText.text = myData.UserFirstName;
                myLastNameText.text = myData.UserLastName;
                myGradeText.text = myData.Grade.ToString();
                myHeadsetNumberText.text = myData.HeadSetNumber;
                if (myData.PassorFail == true)
                    myPassingInformationToggle.isOn = true;
                else
                    myPassingInformationToggle.isOn = false;
                if (myData.CompletedBefore == true)
                    myCompletionInformationToggle.isOn = true;
                else
                    myCompletionInformationToggle.isOn = false;

                //2. Save the results to data.json
                dataAsJson = JsonUtility.ToJson(myData);
                string filePath;
                if (Application.platform == RuntimePlatform.Android)
                {
                    //CopyFile("data.json", "data");
                    filePath = m_Path + Path.GetFullPath("data.json");
                }
                else if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    gameDataProjectFilePath = "/data.json";
                    filePath = Application.dataPath + gameDataProjectFilePath;
                }
                else
                {
                    filePath = Application.dataPath + gameDataProjectFilePath;
                }
                //CopyFile("data.json", "data");
                File.WriteAllText(filePath, dataAsJson);
            }
        }
    }

    void SendData()
    {
        Debug.Log("Trying To Send the Data!");
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        //A. Save the results to data.json 
        myData.UserFirstName = myFirstNameText.text;
        myData.UserLastName = myLastNameText.text;
        myData.Grade = int.Parse(myGradeText.text);
        myData.HeadSetNumber = myHeadsetNumberText.text;
        if (myPassingInformationToggle.isOn == true)
            myData.PassorFail = true;
        else
            myData.PassorFail = false;
        if (myCompletionInformationToggle.isOn == true)
            myData.CompletedBefore = true;
        else
            myData.CompletedBefore = false;
        String dataAsJson1 = JsonUtility.ToJson(myData);
        string filePath1;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath1 = m_Path + Path.GetFullPath("data.json");
        }
        else
        {
            filePath1 = Application.dataPath + gameDataProjectFilePath;
        }

        File.WriteAllText(filePath1, dataAsJson1);

        //1. generate result from the data.json file.
        string filePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = m_Path + Path.GetFullPath("data.json");

        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            gameDataProjectFilePath = "/data.json";
            filePath = Application.dataPath + gameDataProjectFilePath;
        }
        else
        {
            filePath = Application.dataPath + gameDataProjectFilePath;
        }
        string dataAsJson = File.ReadAllText(filePath);
        myDataPost = JsonUtility.FromJson<MyClassData>(dataAsJson);
        json = JsonUtility.ToJson(myDataPost);

        //2. generate json from class
        //myData = new MyClassData();
        //json = JsonUtility.ToJson(myData);

        //3. generate data from received result after GET
        //json = JsonUtility.ToJson(myData);

        //creating a from with key value pair.
        WWWForm form = new WWWForm();
        //form.AddField(ticket, json);
        form.AddField("data", json);
        using (UnityWebRequest www = UnityWebRequest.Post(myPushEndpoint, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.isDone.ToString());
    
            }
        }
    }

    //==================================ANDROID==================================

    //Get the Headset Id and Endpoints and save to JSON
    void GetID_Android()
    {
        StreamReader inp_stm = new StreamReader(dataPath);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            //Debug.Log(inp_ln);
            Hdata = JsonUtility.FromJson<ClassHeadset>(inp_ln);
            inp_ln = JsonUtility.ToJson(Hdata);
            //CopyFile("Hdata.json", "Hdata");
            //var HeadsetPath = Resources.Load<TextAsset>("EVR/Hdata");
            string filePath = filePath = m_Path + Path.GetFullPath("Hdata.json");
            File.WriteAllText(filePath, inp_ln);
        }
        inp_stm.Close();

        Headset = Hdata.HeadsetID;
        myGetEndpoint = Hdata.GetEndPoint;
        myPushEndpoint = Hdata.PostEndPoint;

    }

    void GetData_Android()
    {
        Debug.Log("Trying To Get the Data!");
        Debug.Log("Earlier:" + myGetEndpoint);
        //Appending headset information to the URL for GET request

        var uriBuilder = new UriBuilder(myGetEndpoint);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["HeadSetNumber"] = Headset;
        uriBuilder.Query = query.ToString();
        myGetEndpoint = uriBuilder.ToString();
        Debug.Log("Later:" + myGetEndpoint);

        StartCoroutine(GetRequest(myGetEndpoint));
        Debug.Log("FINISH GET");
        //save image from url to local disk

    }

    void SendData_Android()
    {
        Debug.Log("Trying To Send the Data!");
        StartCoroutine(Upload());
    }

  
}
