using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutotial_Level_1_Step_2 : TutorialBase
{
    public GameObject currentHand;
    public PileBase tutPile;
    public override bool IsCanShowTut()
    {
        //if (UseProfile.CurrentLevel != 1)
        //    return false;

        return base.IsCanShowTut();
    }
    public override bool IsCanEndTut()
    {

        Destroy(currentHand.gameObject);

        return true;
    }

    public override void StartTut()
    {
        if (GameplayController.Instance.playerContain.currentPile != GameplayController.Instance.playerContain.currentPileInGame[1])
        {
            //GameplayController.Instance.playerContain.currentPileInGame[1].HandleTutLevel1Step2();
            //currentHand = Instantiate(handTut, GameplayController.Instance.playerContain.currentPileInGame[1].transform);

        }
        else
        {
            //GameplayController.Instance.playerContain.currentPileInGame[0].HandleTutLevel1Step2();
            //currentHand = Instantiate(handTut, GameplayController.Instance.playerContain.currentPileInGame[0].transform);

        }
        tutPile = GameplayController.Instance.playerContain.currentPile;
    }

    protected override void SetNameTut()
    {
        nameTut = "TUTOTIAL_LEVEL_1_STEP_2";
    }
}
