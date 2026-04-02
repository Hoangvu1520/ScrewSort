using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Runtime.Versioning;

[System.Serializable]

public class LevelData
{
    public int levelIndex;
    public List<int> screwID;
}
public enum PileType
{
    Slot_4 = 0,
    Slot_5 = 1,
    Slot_6 = 2
}

[System.Serializable]
public class PossitionType
{
    public int sumPile;
    public Transform parent;
    public float plusUp;
    public List<Transform> lisPostPile;
}

[System.Serializable]
public class PileConfig
{
    public List<int> idBirds = new List<int>();
    public PileData standData;
    public int side;
}

[System.Serializable]
public class PileData
{
    public PileType type;
    public int numSlot;
}

[System.Serializable]
public class LevelStandConfig
{

    public int id;
    public List<PileConfig> standConfig;
    // public List<ExtralPlayData> extralsConfig;
    public int min;
    public int second;



}

public class PlayerContain : MonoBehaviour
{
    public ScrewBase screwBase;
    public List<PileBase> currentPileList;
    public List<Transform> lsPostPileHolds;

    public List<PossitionType> lsPossitionType;
    public List<PileBase> lsPileHolds;
    public LevelData levelData;
    public LevelStandConfig levelStandConfig;
    public List<PileBase> currentPileInGame;
    PileType typeStandInLevel;
    public int sumScrewCurrent;
    public int sumScrewInit;
    public bool allScrewInitDone;
    bool checkWin;
    public PossitionType GetPossitionType(int param)
    {
        foreach (var item in lsPossitionType)
        {
            if (item.sumPile == param)
            {
                return item;
            }
        }
        return null;
    }

    public void Init()
    {
        sumScrewCurrent = 0;
        sumScrewInit = 0;
        SpawnLevel();
    }


    private void SpawnLevel()
    {
        string path = Application.dataPath + "/Scripts/LevelData/LevelData.json";
        string json = System.IO.File.ReadAllText(path);
        levelStandConfig = JsonUtility.FromJson<LevelStandConfig>(json);

        // levelStandConfig = JsonUtility.FromJson<LevelStandConfig>(lvJson.text);
        lsPostPileHolds = new List<Transform>();
        foreach (var item in GetPossitionType(levelStandConfig.standConfig.Count).lisPostPile)
        {
            if (item != null)
            {
                lsPostPileHolds.Add(item);
            }
        }

        if (levelStandConfig != null)
        {
            for (int i = 0; i < levelStandConfig.standConfig.Count ; i++)
            {
                int index = i;
                typeStandInLevel = levelStandConfig.standConfig[index].standData.type;
                int idStand = (int)levelStandConfig.standConfig[index].standData.type;
                int sidePosStand = levelStandConfig.standConfig[index].side;
                PileBase stand = Instantiate(lsPileHolds[Mathf.Clamp(idStand, 0, lsPileHolds.Count - 1)], lsPostPileHolds[i].transform);
                StartCoroutine(stand.Init(levelStandConfig.standConfig[index].idBirds));
                currentPileInGame.Add(stand);

            }
        }




        if (typeStandInLevel == PileType.Slot_6)
        {
            Camera.main.orthographicSize += 0.5f;
        }
    }
    public void CheckScewWasInit()
    {
        sumScrewInit += 1;
        if (sumScrewInit >= sumScrewCurrent)
        {
            allScrewInitDone = true;
            // GamePlayController.Instance.extralPlayController.SpawnExtrals(levelConfig.extralsConfig);
            // GamePlayController.Instance.tutLevel_1.StartTut();
            // GamePlayController.Instance.tutLevel_2.StartTut();
           
        
        }
        else
        {
            allScrewInitDone = false;
        }
          
    }

    public void HandleCheckWin()
    {
        checkWin = true;

        foreach (var item in currentPileInGame)
        {
            if (!item.GetScrewComplete)
            {
                checkWin = false;
            }

        }

        if (checkWin)
        {
            StartCoroutine(ShowPopupWin());

        }
    }
    private IEnumerator ShowPopupWin()
    {
        yield return new WaitForSeconds(1);

        WinBox.Setup().Show();

    }
}
