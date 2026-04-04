using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PileBase : SerializedMonoBehaviour
{
    //attributes
    [SerializeField] public List<ScrewBase> screwList;
    public List<Transform> screwPostList;
    public PileData pileData;
    private bool isComPleted;
    public int id;
    public Transform firstPost;
    public int slotNum; //maximum number of screw
    public AudioClip audioClip;
    public bool canTouch;
    public ScrewBase[] screwOnArray
    {
        get
        {
            return screwList.ToArray();
        }
    }
    public bool GetScrewComplete
    {
        get
        {
            for (int i = 0; i < screwList.Count; i++)
            {
                if (screwList[i].IsCompleted == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public bool IsCompleted
    {
        get
        {
            return isComPleted;
        }
        set
        {
            isComPleted = value;
            if (isComPleted)
            {
                // foreach (var item in lsVfxStar)
                // {
                //     item.gameObject.SetActive(true);
                // }
            }
            else
            {
                // foreach (var item in lsVfxStar)
                // {
                //     item.gameObject.SetActive(false);
                // }
            }
        }
    }
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
    public bool GetPileSuccess
    {
        get
        {
            
            for (int i = 0; i < screwList.Count; i++)
            {
                if (screwList[i].id != firstScrew.id)
                {
                    return false;
                }
            }

            if (screwList.Count >= pileData.numSlot)
            {
                for (int i = 0; i < screwList.Count; i++)
                {
                    screwList[i].IsCompleted = true;
                }
                //vfxConfetti.Play();
                //audio.PlayOneShot(audioClipConfeti);
                //MMVibrationManager.Haptic(HapticTypes.Selection, false, true, this);
                return true;
            }

            return false;
        }
    }


    public IEnumerator Init(List<int> idScrew)
    {
        screwList = new List<ScrewBase>();
        yield return new WaitForSeconds(1);

        for (int i = 0; i < idScrew.Count; i++)
        {
            int index = i;
            var ScrewPrefab = GamePlayController.Instance.playerContain.screwBase;
            var Screw = Instantiate(ScrewPrefab, firstPost).GetComponent<ScrewBase>();
            Screw.Init(idScrew[i]);
            Screw.gameObject.name = idScrew[i].ToString();
            screwList.Add(Screw);
            GamePlayController.Instance.playerContain.sumScrewCurrent += 1;
        }

        for (int i = 0; i < screwList.Count; i++)
        {
            int index = i;
            float time = 0;
            switch (index)
            {
                case 0:
                    time = 0.3f;
                    break;
                case 1:
                    time = 0.4f;
                    break;
                case 2:
                    time = 0.6f;
                    break;
                case 3:
                    time = 0.8f;
                    break;
                case 4:
                    time = 1f;
                    break;
                case 5:
                    time = 1.2f;
                    break;
            }
            // if (index > 0)
            // {
            //     time = (float)(1 / (float)index);

            // }
            // else
            // {
            //     time = (float)(1 / 0.8f);
            // }
            screwList[index].Scale(time, delegate
            {
                screwList[index].transform.parent = screwPostList[index];
                screwList[index].LocalMoveScrew(screwList[index].transform.parent, delegate { GamePlayController.Instance.playerContain.CheckScewWasInit(); });

            });
        }
        
        IsCompleted = false;

    }


    public void PopFirstScrewSameColor()
    {
        int lastIndex = screwList.Count - 1;
    }
    

}
