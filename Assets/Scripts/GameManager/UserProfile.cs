using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public static bool FirstLoading
    {
        get
        {
            return PlayerPrefs.GetInt("", 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("", value ? 1 : 0);
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
    
 
    // public string ListSave
    // {
    //     get
    //     {
    //         return PlayerPrefs.GetString(GameData.LEVEL_SAVE);
    //     }
    //     set
    //     {
    //         PlayerPrefs.SetString(GameData.LEVEL_SAVE, value);
    //         PlayerPrefs.Save();
    //     }
    // }


}
