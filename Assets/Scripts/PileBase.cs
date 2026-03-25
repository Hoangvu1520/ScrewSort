using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileBase : SerializedMonoBehaviour
{
    //attributes
    [SerializeField] public List<ScrewBase> screwList;
    public List<Transform> screwPostList;
    public bool isCompleted;
    public int id;
    public Transform firstPost;
    public int slotNum; //maximum number of screw
    public ScrewBase firstScrew
    {
        get
        {
            if (screwList.Count == 0)
            {
                return null;
            }
            return screwList[screwList.Count - 1];
        }
    }

    public Transform firstScrewPos
    {
        get
        {
            if (screwList.Count == 0)
                return null;

            return screwPostList[screwList.Count - 1];
        }
    }


    public void HandleScrewID()
    {

    }

    public void PopFirstScrewSameColor()
    {
        int lastIndex = screwList.Count - 1;
    }


}
