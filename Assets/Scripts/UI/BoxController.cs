using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxController : MonoBehaviour
{
    public static BoxController Instance;

    public const int BASE_INDEX_LAYER = 20;

    [SerializeField] private SceneBase currentScene;

    private Stack<BoxBase> boxStack = new Stack<BoxBase>();
    private Dictionary<string, UnityAction> lstActionSaveBox = new Dictionary<string, UnityAction>();

    public bool isLoadingShow;
    public bool isLockEscape;
    public UnityAction actionStackEmty;
    public UnityAction actionOnClosedOneBox;

    protected void Awake()
    {
        Instance = this;
        this.gameObject.name = "BoxController";
        isLoadingShow = true;
    }

    private IEnumerator GetBoxAsyncHandle(BoxBase instance, Action<BoxBase> OnLoadedFirst, Action<BoxBase> OnLoaded, string resourcePath)
    {
        // Context.Waiting.ShowWaiting();
        if (instance == null)
        {
            ResourceRequest request = Resources.LoadAsync<BoxBase>(resourcePath);
            yield return request;
            instance = Instantiate(request.asset as BoxBase);
            if (OnLoadedFirst != null)
                OnLoadedFirst(instance);
        }

        if (OnLoaded != null)
            OnLoaded(instance);
        //  Context.Waiting.HideWaiting();
    }

    public void AddNewBackObj(BoxBase obj)
    {
        boxStack.Push(obj);
        SettingOderLayerPopup();
    }

    private void SettingOderLayerPopup()
    {
        if (boxStack == null && boxStack.Count <= 0)
            return;
        BoxBase[] lst_backObjs = boxStack.ToArray();
        int lenght = lst_backObjs.Length;
        int index = 0;
        for (int i = lenght - 1; i >= 0; i--)
        {
            //lst_backObjs[i].ChangeLayerHandle(ref index);
        }
    }

    public void Remove()
    {

        if (boxStack.Count == 0)
            return;
        BoxBase obj = boxStack.Pop();
        if (boxStack.Count == 0)
            OnStackEmpty();

        SettingOderLayerPopup();
    }

    /// <summary>
    /// ?ang có Popup hi?n
    /// </summary>
    /// <returns></returns>
    public bool IsShowingPopup()
    {
        BoxBase[] lst_backObjs = boxStack.ToArray();
        int lenght = lst_backObjs.Length;
        for (int i = lenght - 1; i >= 0; i--)
        {
            if (lst_backObjs[i].isPopup)
            {
                return true;
            }
        }

        return false;
    }


    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //ProcessBackBtn();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugStack();
        }
    }


    public void DebugStack()
    {
        BoxBase[] lst_backObjs = boxStack.ToArray();
        int lenght = lst_backObjs.Length;
        for (int i = lenght - 1; i >= 0; i--)
        {
            Debug.Log(" =============== " + lst_backObjs[i].gameObject.name + " ===============");
        }
    }

    private void OnStackEmpty()
    {
        if (actionStackEmty != null)
            actionStackEmty();
    }

    public virtual void ProcessBackBtn()
    {
        if (isLoadingShow)
            return;

        if (isLockEscape)
            return;

        if (boxStack.Count != 0)
        {
            //boxStack.Peek().Close();
        }
        else
        {
            if (!OpenBoxSave())
                OnPressEscapeStackEmpty();
        }
    }

    protected virtual void OnPressEscapeStackEmpty()
    {
        if (currentScene == null)
            currentScene = GameObject.FindObjectOfType<SceneBase>();

        if (currentScene != null)
            currentScene.OnEscapeWhenStackBoxEmpty();

    }

    protected void OnLevelWasLoaded(int level)
    {
        if (currentScene == null)
            currentScene = GameObject.FindObjectOfType<SceneBase>();
        if (boxStack != null)
            boxStack.Clear();

        OpenBoxSave();
    }

    /// <summary>
    /// Check không có Popup hay panel nào ???c b?t
    /// </summary>
    /// <returns></returns>
    public bool IsEmptyStackBox()
    {
        return boxStack.Count > 0 ? false : true;
    }

    #region Box Save
    private bool OpenBoxSave()
    {
        bool isOpened = false;
        foreach (var save in lstActionSaveBox)
        {
            if (save.Value != null)
            {
                isOpened = true;
                save.Value();
            }
        }

        return isOpened;
    }

    public void AddBoxSave(string idPopup, UnityAction actionOpen)
    {
        if (lstActionSaveBox.ContainsKey(idPopup))
            lstActionSaveBox.Add(idPopup, actionOpen);
    }

    public void RemoveBoxSave(string idPopup)
    {
        if (lstActionSaveBox.ContainsKey(idPopup))
            lstActionSaveBox.Remove(idPopup);
    }
    #endregion
}
