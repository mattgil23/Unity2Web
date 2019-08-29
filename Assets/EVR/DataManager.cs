using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//This Script will be what controls the ability to push and get data given the input of the UI
//Some things already typed are just thoughts or starting points.... ~MG


public class DataManager : MonoBehaviour
{
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
        
        //1)first get the headset# and add that to the endpoint url
        //2)Then use like "UnityWebrequest" to "get" at the correct url (return log if not active) and
        //assign it to "myData" (the JsonUtility functions should help with this...)
        //3)Get data and then assign it correctly to vars to display
    }

    void SendData()
    {
        Debug.Log("Trying To Send the Data!");
 
        //1)Update data fields with new information
        //2)reference the headset# to add to endpoint url
        //3)mostlikely update myData with entered information and convert to .json with Jsonutility
        //4)from json form, send to endpoint and return if failed or sucessful

    }
}
