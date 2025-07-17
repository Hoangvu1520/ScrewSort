using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}
public class GameplayController : Singleton<GameplayController>
{
    public PlayerContain playerContain;
    public GameScene gameScene;
    public ReturnController returnController;
    public TutorialFunController tutLevel_1;
    public ExtralPlayController extralPlayController;
    public CameraScale cameraScale;

    protected override void OnAwake()
    {


        Init();

    }
    public void Init()
    {
        gameScene.Init();
        returnController.Init();
        playerContain.Init();
        tutLevel_1.Init();
    }
}
