using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHelper : MonoBehaviour
{
    public const string LOADING_COMPLETE = "LOADING_COMPLETE";
    public const string ONOFF_SOUND = "ONOFF_SOUND";
    public const string ONOFF_MUSIC = "ONOFF_MUSIC";
    public const string ONOFF_VIBRATION = "ONOFF_VIBRATION";
    public const string PATH_CONFIG_LEVEL = "LevelTest/LevelData{0}";
    public const string CURRENT_LEVEL = "CURRENT_LEVEL";
}

public class SceneName
{
    public const string LOADING_SCENE = "LoadingScene";
    public const string HOME_SCENE = "HomeScene";
    public const string GAME_PLAY = "GamePlay";
}

public class PathPrefabs
{
    public const string WIN_BOX = "UI/Popups/WinBox";
    public const string SETTING_BOX = "UI/Popups/SettingBox";
}
