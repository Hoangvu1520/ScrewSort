using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameScene : SceneBase
{
    //attribute
    public Button btnNextLevel;
    public Button btnRemoveAds;
    public Button btnSetting;
    public Button btnReturn;
    public Button btnReload;
    public Button btnAddPile;
    public Button btnBack;
    public TMP_Text tvLevel;
    public Text tvBtnReturn;
    public Text tvBtnAddPile;
    public Text tvBtnRemoveBoom;
    public Text tvBtnRemoveCage;

    public Image iconPlusReturn;
    public Image iconPlusAddPile;
    public Image iconPlusRemoveBoom;
    public Image iconPlusRemoveCage;

    public Light light;
    public Button btnRemoveBoom;
    public Button btnRemoveCage;
    public InputField input;

    public GameObject testPanel;
    public bool isOff = false;
    public List<Image> lsImage;
    public List<Text> lsTv;

    //methods
    public void Init()
    {
        btnReload.onClick.AddListener(OnClickRetry);
        btnSetting.onClick.AddListener(delegate { 
            //GameController.Instance.musicManager.PlayClickSound(); 
            SettingBox.Setup().Show(); });
        tvLevel.text = "Level " + "\n" + UserProfile.CurrentLevel;
    }
    public void HandleBtnRemoveAds()
    {
        //GameController.Instance.musicManager.PlayClickSound();
        //GameController.Instance.iapController.BuyProduct(TypePackIAP.NoAdsPack);
    }

    //this one is watching ads to skip level
    public void HandleNextLevelButton()
    {
        //GameController.Instance.admobAds.ShowVideoReward(
        //      actionReward: () =>
        //      {

        //          Winbox.Setup().Show();
        //      },
        //      actionNotLoadedVideo: () =>
        //      {
        //          GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
        //           (
        //              btnNextLevel.transform,
        //           btnNextLevel.transform.position,
        //           "No video at the moment!",
        //           Color.white,
        //           isSpawnItemPlayer: true
        //           );
        //      },
        //      actionClose: null,
        //      //ActionWatchVideo.Skip_level,
        //      UserProfile.CurrentLevel.ToString());

        //GameController.Instance.musicManager.PlayClickSound();
    }

    public void OnClickPassLevel()
    {
        var level = Int32.Parse(input.text);
        UserProfile.CurrentLevel = level;
        SceneManager.LoadScene("Gameplay");
    }

    public void OnOffUI()
    {
        if (!isOff)
        {
            isOff = true;
            foreach (var item in lsImage)
            {
                item.color = new Color32(0, 0, 0, 0);
            }
            foreach (var item in lsTv)
            {
                item.color = new Color32(0, 0, 0, 0);
            }

        }
        else
        {
            isOff = false;
            foreach (var item in lsImage)
            {
                item.color = new Color32(255, 255, 255, 255);
            }
            foreach (var item in lsTv)
            {
                item.color = Color.black;
            }

        }
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {

    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("Gameplay");
    }
}

public class HomeExtral
{
    public ExtralPlayType type;
    public int quantity;
    public Sprite icon;
    public UnityAction actionExtralDone;
    public HomeExtral(ExtralPlayType type, int quantity, Sprite icon, UnityAction actionExtralDone)
    {
        this.type = type;
        this.quantity = quantity;
        this.icon = icon;
        this.actionExtralDone = actionExtralDone;
    }
}
