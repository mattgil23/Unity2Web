using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class GameDataEditor : EditorWindow
{
    public MyClassData gameData;
    public ClassHeadset HeadsetData;
    private string gameDataProjectFilePath = "/EVR/data.json";
    private string HeadsetPath = "/EVR/Hdata.json";
    [MenuItem("Window/Game Data Editor")]
    //[MenuItem("Window/Headset Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    void OnGUI()
    {
        //MyClass Data
        if (gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }
        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        //Headset Data
        if (HeadsetData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("HeadsetData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }
    }

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        string HfilePath = Application.dataPath + HeadsetPath;

        //MyClass Data
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<MyClassData>(dataAsJson);
        }
        else
        {
            gameData = new MyClassData();
        }

        //HeadSet Data
        if (File.Exists(HfilePath))
        {
            string HdataAsJson = File.ReadAllText(HfilePath);
            HeadsetData = JsonUtility.FromJson<ClassHeadset>(HdataAsJson);
        }
        else
        {
            HeadsetData = new ClassHeadset();
        }
    }

    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

        //Headset data
        string HdataAsJson = JsonUtility.ToJson(HeadsetData);
        string HfilePath = Application.dataPath + HeadsetPath;
        File.WriteAllText(HfilePath, HdataAsJson);
    }
}