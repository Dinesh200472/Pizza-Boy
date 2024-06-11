using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static readonly string savePath = Application.persistentDataPath + "/saveData.json";

    public static void SaveIntArray(int[] data)
    {
        SaveData saveData = new SaveData();
        saveData.integerArray = data;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    public static int[] LoadIntArray()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            return saveData.integerArray;
        }
        else
        {
            Debug.LogWarning("Save file not found");
            return null;
        }
    }
}
