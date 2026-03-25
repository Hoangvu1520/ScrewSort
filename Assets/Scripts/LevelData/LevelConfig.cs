using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;

[CreateAssetMenu(menuName = "Game/Level Config")]
[System.Serializable]
public class LevelConfigData
{
    public List<StandConfig> standConfig;
}

[System.Serializable]
public class StandConfig
{
    public List<int> idBirds;
    public StandData standData;
    public int side;
}

[System.Serializable]
public class StandData
{
    public int type;
    public int numSlot;
}
public class LevelConfig : ScriptableObject
{
    [Title("Level Number")]
    public int levelNumber;
    [Title("Editable Data")]
    [ShowInInspector]
    public LevelConfigData data = new LevelConfigData();

    [Title("JSON")]
    [TextArea(15, 30)]
    public string json;

    [Button(ButtonSizes.Large)]
    public void LoadFromJson()
    {
        data = JsonUtility.FromJson<LevelConfigData>(json);
    }

    [Button(ButtonSizes.Large)]
    public void SaveToJson()
    {
        json = JsonUtility.ToJson(data, true);
    }
    [Button(ButtonSizes.Large)]
    public void SaveJsonToFile()
    {
        string folderPath = Application.dataPath + "/Scripts/LevelData";
        string path = folderPath + $"/LevelData{levelNumber}.json";
        File.WriteAllText(path, json);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif
    }
}
