using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;
using System.Web;
using System.Net;

//This Script will be what controls the ability to push and get data given the input of the UI
//Some things already typed are just thoughts or starting points.... ~MG


public class DataManager : MonoBehaviour
{
    private string gameDataFileName = "/EVR/data.json";
    public Text myFirstNameText = null;
    public string myFirstnameString;
    public Text myLastNameText;
    public string myLastNameString;
    public Toggle myPassingInformationToggle;
    public bool myPassingInformationBool = false;
    public Toggle myCompletionInformationToggle;
    public bool myCompletionInformationBool = false;
    public Text myHeadsetNumberText;
    public string myHeadsetNumberString;
    public int myHeadsetNumberInt;
    public Text myGradeText;
    public string myGradeString;
    public int myGradeInt;
    public Button getDataButton;
    public Button sendDataButton;
    public string json; 
    public MyClassData myData;
    public MyClassData myDataPost;
    public string ticket;
    private string gameDataProjectFilePath = "/EVR/data.json";
    public string myGetEndpoint = "probablyneedtoedit";
    public string myPushEndpoint = "probablyneedtochangethis";
    // Start is called before the first frame update
    void Start()
    {
        getDataButton.onClick.AddListener(GetData);
        sendDataButton.onClick.AddListener(SendData);
        myFirstNameText.text = "";
    }
    //I think you need to make an "Event" function for when a button is pressed to perform a function from it...

    //you should just have to fill in the the following event functions....
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

        //StartCoroutine(GetRequest("http://evr-demo.herokuapp.com/test?HeadSetNumber=1"));

        //StartCoroutine(GetRequest("http://evr-demo.herokuapp.com/images/login.png"));
        //StartCoroutine(GetText());

        //save image from url to local disk
       // WebClient webClient = new WebClient();
      // webClient.DownloadFile("http://evr-demo.herokuapp.com/images/login.png", "C:\\Users\\candi\\Desktop\\COURSES\\Sem 4\\EVR\\UnityToWeb\\Assets\\EVR\\login.png");

    }

    //Save image as a texture.....error in casting, need to fix
    //IEnumerator GetText()
    //{
    //    using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("http://evr-demo.herokuapp.com/images/login.png"))
    //    {
    //        yield return uwr.SendWebRequest();

    //        if (uwr.isNetworkError || uwr.isHttpError)
    //        {
    //            Debug.Log(uwr.error);
    //        }
    //        else
    //        {
    //            // Get downloaded asset bundle
    //            var texture = DownloadHandlerTexture.GetContent(uwr);
    //        }
    //    }
    //}

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

                //image
                //Texture myTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;


                //Debug.Log(webRequest.downloadHandler.text.ToString());

                string dataAsJson = webRequest.downloadHandler.text.ToString();

                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                myData = JsonUtility.FromJson<MyClassData>(dataAsJson);
                //JsonUtility.FromJsonOverwrite( dataAsJson , this);

                //1. print the results received from GET
                ticket = myData.ConfigID;
                Debug.Log("Ticket: " + myData.ConfigID + " \nExperience Number: " + myData.ExperienceNumber + " \nHead Set Number: " + myData.HeadSetNumber + " \nUser First Name: " + myData.UserFirstName + " \nUser Last Name: " + myData.UserLastName + " \nPassed? " + myData.PassorFail + " \nGrade: " + myData.Grade + " \nPrevious Date: " + myData.PreviousDate + " \nCurrent Date: " + myData.CurrentDate + " \nIs it Completed Before: " + myData.CompletedBefore);

                //*********************Updating the Text fields in UI*******************************************
                myFirstnameString = myData.UserFirstName;
                myFirstNameText.text = myFirstnameString;
                
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
        //1)Update data fields with new information
        //2)reference the headset# to add to endpoint url
        //3)mostlikely update myData with entered information and convert to .json with Jsonutility
        //4)from json form, send to endpoint and return if failed or sucessful

    }


    IEnumerator Upload()
    {
        //A. Save the results to data.json *********************Updating the Text fields in UI*******************************************
        //myData = JsonUtility.FromJson<MyClassData>(myFirstNameText.text);
        //String dataAsJson1 = JsonUtility.ToJson(myData);
        //Debug.Log("Ticket: " + myData.ConfigID + " \nExperience Number: " + myData.ExperienceNumber + " \nHead Set Number: " + myData.HeadSetNumber + " \nUser First Name: " + myData.UserFirstName + " \nUser Last Name: " + myData.UserLastName + " \nPassed? " + myData.PassorFail + " \nGrade: " + myData.Grade + " \nPrevious Date: " + myData.PreviousDate + " \nCurrent Date: " + myData.CurrentDate + " \nIs it Completed Before: " + myData.CompletedBefore);

         myData.UserFirstName = myFirstNameText.text;
        //Debug.Log("Ticket: " + myData.ConfigID + " \nExperience Number: " + myData.ExperienceNumber + " \nHead Set Number: " + myData.HeadSetNumber + " \nUser First Name: " + myData.UserFirstName + " \nUser Last Name: " + myData.UserLastName + " \nPassed? " + myData.PassorFail + " \nGrade: " + myData.Grade + " \nPrevious Date: " + myData.PreviousDate + " \nCurrent Date: " + myData.CurrentDate + " \nIs it Completed Before: " + myData.CompletedBefore);

        //string filePath1 = Application.dataPath + gameDataProjectFilePath;
        //File.WriteAllText(filePath1, dataAsJson1);


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
