using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[System.Serializable]

public class LevelData
{
    public int levelIndex;
    public List<int> screwID;
}
public enum PileType
{
    
}


public class PlayerContain : MonoBehaviour
{
    public ScrewBase scewBase;
    public List<PileBase> currentPileList;
    public List<Transform> listPileHoldPos;
    public List<PileBase> pileType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
