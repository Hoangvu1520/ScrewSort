using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class WinBox : BoxBase
{
    public Button nextButton;
    public Image aura;
    public Image emojiIcon;
    public List<Sprite> spritesList;
    public List<string> contentList;
    public Text tvContent;
    public List<GameObject> onOffIcon;
    #region instance

    private static WinBox instance;
    public static WinBox Setup(bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<WinBox>(PathPrefabs.WIN_BOX));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion

    private void Init()
    {
        nextButton.onClick.AddListener(HandleNext);
    }

    public void InitState()
    {
        emojiIcon.sprite = spritesList[UnityEngine.Random.Range(0, spritesList.Count)];
        nextButton.gameObject.SetActive(false);

        //GameController.Instance.musicManager.PlayWinSound();
        Invoke("ShowBtnNext", 0.5f);
        //if (UserProfile.CurrentLevel < RemoteConfigController.GetIntConfig(FirebaseConfig.LEVEL_START_SHOW_INITSTIALL, 3))
        //{
        //    return;
        //}
        //if (GameController.Instance.useProfile.IsRemoveAds)
        //{
        //    return;
        //}
        //if (GameController.Instance.admobAds.IsMRecReady)
        //{
        //    foreach (var item in lsIconOnOffMerce)
        //    {
        //        item.gameObject.SetActive(false);
        //    }
        //    GameController.Instance.admobAds.HandleShowMerec();

        //}
    }

    public void ShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    public void HandleShowNextButton()
    {
        nextButton.gameObject.SetActive(true);
    }

    public void HandleNext()
    {
        //GameController.Instance.admobAds.ShowInterstitial(false, actionIniterClose: () => { Next(); }, actionWatchLog: "replay");

        void Next()
        {
            //    GameController.Instance.AnalyticsController.WinLevel(UseProfile.CurrentLevel);
            //    GameController.Instance.admobAds.HandleHideMerec();
            //    MMVibrationManager.Haptic(HapticTypes.Selection, false, true, this);
            //    GameController.Instance.musicManager.PlayClickSound();
                //GamePlayController.Instance.tutLevel_1.EndTut();
            if (UserProfile.CurrentLevel >= 100)
        {
            SceneManager.LoadScene("GamePlay");
            return;
        }
        UserProfile.CurrentLevel += 1;
            SceneManager.LoadScene("GamePlay");

        }


}

    
}
