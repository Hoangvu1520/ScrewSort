using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}
public class GamePlayController : Singleton<GamePlayController>
{
    public PlayerContain playerContain;
    public GameScene gameScene;
    public ReturnController returnController;

    protected override void OnAwake()
    {
        Init();
    }
    void Init()
    {
        if (playerContain == null)
        {
            Debug.LogError("GamePlayController: playerContain reference is missing.");
            return;
        }

        playerContain.Init();
    }
}
