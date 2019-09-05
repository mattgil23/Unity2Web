using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class GameDataEditor : EditorWindow
{

    public MyClassData gameData;

    private string gameDataProjectFilePath = "/EVR/data.json";

    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    void OnGUI()
    {
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
    }

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<MyClassData>(dataAsJson);
        }
        else
        {
            gameData = new MyClassData();
        }
    }

    private void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }
}