using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateExtral
{
    Appear = 0,
    Life = 1,
    Success = 2,
    Fail = 3
}
public abstract class ExtralPlayBase : MonoBehaviour
{
    public int id;
    public ExtralPlayData data;
    public StateExtral state;
    protected IEnumerator m_ExtralSuccess;
    [SerializeField] protected GameObject body;
    public Sprite iconItem;
    public bool isReady;

    public virtual void Init(ExtralPlayData data, int id)
    {
        this.data = data;
        this.id = id;
        state = StateExtral.Appear;

        if (data.scewParent != null)
            data.scewParent.extrals.Add(this);

        if (data.pileParent != null)
            data.pileParent.extrals.Add(this);
    }
    public void CheckExtral(ScrewBase screw, PileBase pile, bool move)
    {
        if (state != StateExtral.Life)
            return;

        CheckExtralHandle(screw, pile, move);
        if (state == StateExtral.Success)
        {
            // GamePlayController.Instance.gameScene.extralBtn.interactable = false;
        }

    }
    public virtual void ApearExtral()
    {
        PlayAnimAppear();
    }
    protected abstract void CheckExtralHandle(ScrewBase bird, PileBase stand, bool isFlyOut);
    public void ReturnExtral()
    {
        if (state != StateExtral.Life)
            return;

        ReturnExtralHandle();
    }
    protected abstract void ReturnExtralHandle();

    public abstract void ReAppearExtral(int idBird, int idStand);

    public virtual void EndExtral(bool isDone)
    {
        state = isDone ? StateExtral.Success : StateExtral.Fail;



    }

    public virtual void DestroyExtrals()
    {
        state = StateExtral.Success;
        //PlayAnimEnd();
        // GamePlayController.Instance.gameScene.extralBtn.interactable = false;
    }

    protected virtual void OnUpdate()
    {

    }

    private void Update()
    {
        if (state != StateExtral.Life)
            return;

        OnUpdate();
    }

    protected virtual void PlayAnimAppear()
    {
        state = StateExtral.Life;
    }

    //public virtual void PlayAnimEnd()
    //{
    //    GameplayController.Instance.extralPlayController.CheckAllExtralSuccess();
    //}

    protected virtual void OnDestroy()
    {
        if (m_ExtralSuccess != null)
            StopCoroutine(m_ExtralSuccess);

        StopAllCoroutines();
    }
    public abstract void ResetExtral();
}
