using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public UserProfile useProfile;

    public StartLoading startLoading;

    protected void Awake()
    {
        Instance = this;
        Init();

        DontDestroyOnLoad(this);

        //GameController.Instance.useProfile.IsRemoveAds = true;
    }

    public void Init()
    {
        Application.targetFrameRate = 60;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            SetUp();
        }
        else
        {
            //NotiBox.Setup(SetUp).Show();
        }
    }

    public void SetUp()
    {

        //admobAds.Init();
        //musicManager.Init();
        //iapController.Init();
        //MMVibrationManager.SetHapticsActive(useProfile.OnVibration);
        startLoading.Init();
    }

    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName.ToString(), Color.black, 2f);
    }
}

public enum SceneType
{
    StartLoading = 0,
    MainHome = 1,
    GamePlay = 2
}
