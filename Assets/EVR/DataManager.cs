using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//This Script will be what controls the ability to push and get data given the input of the UI
//Some things already typed are just thoughts or starting points.... ~MG


public class DataManager : MonoBehaviour
{
    private string gameDataFileName = "/EVR/data.json";
    public Text myFirstNameText;
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
    public Button getDataButton;
    public Button sendDataButton;

    public MyClassData myData;

    public string myGetEndpoint = "probablyneedtoedit";
    public string myPushEndpoint = "probablyneedtochangethis";
    // Start is called before the first frame update
    void Start()
    {
        getDataButton.onClick.AddListener(GetData);
        sendDataButton.onClick.AddListener(SendData);

        //probably should 
    }
    //I think you need to make an "Event" function for when a button is pressed to perform a function from it...

    //you should just have to fill in the the following event functions....
    void GetData()
    {
        Debug.Log("Trying To Get the Data!");
        StartCoroutine(GetRequest("http://evr-demo.herokuapp.com/test"));
        //1)first get the headset# and add that to the endpoint url
        //2)Then use like "UnityWebrequest" to "get" at the correct url (return log if not active) and
        //assign it to "myData" (the JsonUtility functions should help with this...)
        //3)Get data and then assign it correctly to vars to display
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
                //Debug.Log(webRequest.downloadHandler.text.ToString());

                string dataAsJson = webRequest.downloadHandler.text.ToString();
                // Pass the json to JsonUtility, and tell it to create a GameData object from it
                myData = JsonUtility.FromJson<MyClassData>(dataAsJson);

                // Retrieve the allRoundData property of loadedData
                //allRoundData = loadedData.allRoundData;

                //Debug.Log(pages[3] + ":\nReceived: " + webRequest.downloadHandler.text.ToString());
                Debug.Log("Ticket: " + myData.Ticket + " \nExperience Number: " + myData.ExperienceNumber + " Head Set Number: " + myData.HeadSetNumber + " User First Name: " + myData.UserFirstName + " User Last Name: " + myData.UserLastName + " Passed? " + myData.PassorFail + " Grade: " + myData.Grade + " Previous Date: " + myData.PreviousDate + " Current Date: " + myData.CurrentDate + " Is it Completed Before: " + myData.CompletedBefore);
                Debug.Log("Ticket: " + myData.Ticket + " \nExperience Number: " + myData.ExperienceNumber + " \nHead Set Number: " + myData.HeadSetNumber + " \nUser First Name: " + myData.UserFirstName + " \nUser Last Name: " + myData.UserLastName + " \nPassed? " + myData.PassorFail + " \nGrade: " + myData.Grade + " \nPrevious Date: " + myData.PreviousDate + " \nCurrent Date: " + myData.CurrentDate + " \nIs it Completed Before: " + myData.CompletedBefore);

            }
        }

    }


    void SendData()
    {
        Debug.Log("Trying To Send the Data!");
        //StartCoroutine(Upload());
        //1)Update data fields with new information
        //2)reference the headset# to add to endpoint url
        //3)mostlikely update myData with entered information and convert to .json with Jsonutility
        //4)from json form, send to endpoint and return if failed or sucessful

    }


    //IEnumerator Upload()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("myField", "myData");

    //    using (UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Form upload complete!");
    //        }
    //    }
    //}


}
