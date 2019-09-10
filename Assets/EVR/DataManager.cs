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
    //get current date
    System.DateTime date = System.DateTime.Now;
    string dateFormat;
    private string gameDataFileName = "/EVR/data.json";
    public InputField myFirstNameText;
    public InputField myLastNameText;
    public InputField myHeadsetNumberText;
    public InputField myGradeText;
    public Toggle myPassingInformationToggle;
    public Toggle myCompletionInformationToggle;
    public Button getDataButton;
    public Button sendDataButton;
    public string json; 
    public MyClassData myData;
    public MyClassData myDataPost;
    private string gameDataProjectFilePath = "/EVR/data.json";
    public string myGetEndpoint = "probablyneedtoedit";
    public string myPushEndpoint = "probablyneedtochangethis";
    // Start is called before the first frame update
    void Start()
    {
        //initialize json file to default null values
        myFirstNameText.text = "";
        myData.UserID = "";
        myData.ConfigID = "";
        myData.ExperienceNumber = "";
        myData.HeadSetNumber = "";
        myData.UserFirstName = "";
        myData.UserLastName = "";
        myData.PassorFail = false;
        myData.Grade = 0;
        myData.PreviousDate = "";
        myData.CurrentDate = "";
        myData.CompletedBefore = false;

        dateFormat =  date.Month + "/" + date.Day + "/" + date.Year;
        getDataButton.onClick.AddListener(GetData);
        sendDataButton.onClick.AddListener(SendData);
    }
  
    void GetData()
    {
        Debug.Log("Trying To Get the Data!");
        
        //Appending headset information to the URL for GET request
        string longurl = "http://evr-demo.herokuapp.com/test?";
        var uriBuilder = new UriBuilder(longurl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["HeadSetNumber"] = "1";
        uriBuilder.Query = query.ToString();
        longurl = uriBuilder.ToString();
        StartCoroutine(GetRequest(longurl));

        //save image from url to local disk
        //WebClient webClient = new WebClient();
        //webClient.DownloadFile("http://evr-demo.herokuapp.com/images/login.png", "C:\\Users\\candi\\Desktop\\COURSES\\Sem 4\\EVR\\UnityToWeb\\Assets\\EVR\\login.png");
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
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
                Debug.Log("Ticket: " + myData.ConfigID + " \nExperience Number: " + myData.ExperienceNumber + " \nHead Set Number: " + myData.HeadSetNumber + " \nUser First Name: " + myData.UserFirstName + " \nUser Last Name: " + myData.UserLastName + " \nPassed? " + myData.PassorFail + " \nGrade: " + myData.Grade + " \nPrevious Date: " + myData.PreviousDate + " \nCurrent Date: " + myData.CurrentDate + " \nIs it Completed Before: " + myData.CompletedBefore);

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
                string filePath = Application.dataPath + gameDataProjectFilePath;
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
        myData.CurrentDate = dateFormat;
        String dataAsJson1 = JsonUtility.ToJson(myData);
        string filePath1 = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath1, dataAsJson1);

        //1. generate result from the data.json file.
        string filePath = Application.dataPath + gameDataProjectFilePath;
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
        using (UnityWebRequest www = UnityWebRequest.Post("http://evr-demo.herokuapp.com/test", form))
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
}
