using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutotial_Level_1_Step_1 : TutorialBase
{
    public GameObject currentHand;
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
        currentHand = Instantiate(handTut, GameplayController.Instance.playerContain.currentPileInGame[0].transform);

    }

    protected override void SetNameTut()
    {
        nameTut = "TUTOTIAL_LEVEL_1_STEP_1";
    }
}
