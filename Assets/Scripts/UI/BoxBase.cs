using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.ComponentModel;

public abstract class BoxBase : MonoBehaviour
{
    [SerializeField] protected RectTransform mainPanel;
    
    public bool isNotStack;
    public bool isPopUp;
    [SerializeField] protected bool isAnim = true;
    protected CanvasGroup canvasGroupPanel;
    protected CancelEventArgs popupCanvas;
    public bool isBoxSave;
    protected string idPopup;

    public UnityAction OnCloseBox;
    public UnityAction<int> OnChangeLayer; 
    protected UnityAction actionOpenSaveBox;

    protected string GetIDPopup()
    {
        return idPopup;
    }

    protected virtual void SetIDPopup()
    {
        idPopup = this.GetType().ToString();
    }

    public virtual T SetupBase<T>(bool isSaveBox = false, UnityAction actionOpenBoxSave = null) where T : BoxBase
    {
        return null;    
    }

    private void Awake()
    {
        
    }
}
