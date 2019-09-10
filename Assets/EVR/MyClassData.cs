using UnityEngine;
using UnityEditor;
using System.Collections;
using Unity.Collections;

[System.Serializable]
public class MyClassData 
{
    //This is just going to be a simple class that holds the data that will be sent back and forth...
    //[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public string UserID = "1";
    public string ConfigID = "100000";
    public string ExperienceNumber = "2";
    public string HeadSetNumber = "2";
    public string UserFirstName = "Matt";
    public string UserLastName = "Gill";
    public bool PassorFail = false;
    public int Grade = 90;
    public string PreviousDate = "09/05/2019";
    public string CurrentDate = "09/05/2019";
    public bool CompletedBefore = true;


    //Optionally if you have time other data that could be added is a float that could store the time it took to complete the experience or task....
}
