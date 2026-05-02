using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class UserProfile : MonoBehaviour
{
    public static bool FirstLoading
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LOADING_COMPLETE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LOADING_COMPLETE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, value);
            PlayerPrefs.Save();
        }
    }
    


}
