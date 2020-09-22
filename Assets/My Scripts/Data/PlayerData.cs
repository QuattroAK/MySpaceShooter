using PlayerDatas;
using UnityEngine;
using System;
using UnityEditor;

public static class PlayerData
{
    private static SaveData saveData;
    private static SaveManager saveManager;

    #region Properties

    public static PlayerProgressData PlayerProgressData
    {
        get
        {
            return saveData.PlayerProgressData;
        }
    }

    public static PlayerResourcesData PlayerResourcesData
    {
        get
        {
            return saveData.PlayerResourcesData;
        }
    }

    public static PlayerStatisticData PlayerStatisticData
    {
        get
        {
            return saveData.PlayerStatisticData;
        }
    }

    #endregion

    public static void Init()
    {
        saveManager = new SaveManager();
        saveData = saveManager.LoadSaves();
    }

    public static void SaveSaves()
    {
        saveManager.SaveSaves(saveData);
    }
}

public class SaveManager
{
    private const string savesKey = "Saves";

    public SaveData LoadSaves()
    {
        if (PlayerPrefs.HasKey(savesKey))
        {
            string jsonSaves = PlayerPrefs.GetString(savesKey);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonSaves);
            return saveData;
        }
        else
        {
            SaveData saveData = new SaveData();
            return saveData;
        }
    }

    public void SaveSaves(SaveData saveData)
    {
        string jsonSaves = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(savesKey, jsonSaves);
    }

#if UNITY_EDITOR
    [MenuItem("PlayerData/Clear Save")]
    public static void ClearSaves()
    {
        PlayerPrefs.DeleteKey(savesKey);
    }
#endif
}

[Serializable]
public class SaveData
{
    public PlayerProgressData PlayerProgressData;
    public PlayerResourcesData PlayerResourcesData;
    public PlayerStatisticData PlayerStatisticData;

    public SaveData()
    {
        PlayerProgressData = new PlayerProgressData();
        PlayerResourcesData = new PlayerResourcesData();
        PlayerStatisticData = new PlayerStatisticData();
    }
}