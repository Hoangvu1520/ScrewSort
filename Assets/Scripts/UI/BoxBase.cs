using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public abstract class BoxBase : MonoBehaviour
{
    //attributes
    [SerializeField] protected RectTransform mainPanel;
    [HideIf("isPopup", false)] public RectTransform contentPanel;
    public bool isNotStack;
    public bool isPopup;
    [SerializeField] protected bool isAnim = true;
    protected Canvas popupCanvas;
    protected CanvasGroup canvasGroupPanel;
    [HideInInspector] public bool isBoxSave;

    protected string iDPopup;


    //Call Back
    public UnityAction OnCloseBox;
    public UnityAction<int> OnChangeLayer;
    protected UnityAction actionOpenSaveBox;

    //method
    protected string GetIDPopup()
    {
        return iDPopup;
    }

    protected virtual void SetIDPopup()
    {
        iDPopup = this.GetType().ToString();
    }

    public virtual T SetupBase<T>(bool isSaveBox = false, UnityAction actionOpenBoxSave = null) where T : BoxBase
    {
        //InitBoxSave(isSaveBox, actionOpenBoxSave);
        return null;
    }

    private void Awake()
    {
        popupCanvas = this.GetComponent<Canvas>();
        if(popupCanvas != null && isPopup)
        {
            popupCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            popupCanvas.worldCamera = Camera.main;
            popupCanvas.sortingLayerID = SortingLayer.NameToID("Popup");
        }
        if (this.mainPanel != null)
        {
            //var tweenAnimation = this.mainPanel.GetComponent<DOTweenAnimation>();
            //if (tweenAnimation != null)
            //{
            //    tweenAnimation.isIndependentUpdate = true;
            //    isAnim = false;
            //}
        }

        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

    public void InitSaveBox(bool isSaveBox, UnityAction actionOpenSaveBox)
    {
        this.isBoxSave = isBoxSave;
        this.actionOpenSaveBox = actionOpenSaveBox;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void SaveBox()
    {
        //if (isBoxSave)
            //BoxController.Instance.AddBoxSave(GetIDPopup(), actionOpenSaveBox);
    }
    public virtual void RemoveSaveBox()
    {
        //BoxController.Instance.RemoveBoxSave(GetIDPopup());
    }
    public virtual bool IsActive()
    {
        return true;
    }

    protected virtual void DoClose()
    {
        //if (isAnim)
        //{
        //    if (mainPanel != null)
        //    {
        //        mainPanel.localScale = Vector3.one;
        //        mainPanel.DOScale(0, 0.5f).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() =>
        //        {

        //            this.gameObject.SetActive(false);
        //        });
        //    }
        //    else
        //    {

        //        this.gameObject.SetActive(false);
        //    }
        //}
        //else
        //{

        this.gameObject.SetActive(false);
        //}

        if (!isPopup)
        {
            if (canvasGroupPanel != null)
                canvasGroupPanel.blocksRaycasts = true;
        }
    }
    public virtual void Close()
    {
        if (!isNotStack)
            BoxController.Instance.Remove();
        DoClose();
    }
}
