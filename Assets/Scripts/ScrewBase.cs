using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScrewBase : MonoBehaviour
{
    public List<Material> listColor;
    public bool isCompleted;
    public Animator animation;
    public int id;
    public int ScrewAmount;
    public Renderer renderer;
    public bool isMoving;
    public bool inFirstPos = false;
    [SerializeField] private Renderer _renderer;
 
    private  bool iscompleted;
    public bool IsCompleted
    {
        get
        {
            return iscompleted;
        }
        set
        {
            iscompleted = value;
           
        }
    }

    public void Init(int paramId)
    {
        _renderer.material = listColor[paramId];
        this.transform.localScale = new Vector3(0, 0, 0);
        id = paramId;
        IsCompleted = false;
    }

    public void MoveScrew(Transform targetPost, Action callback)
    {
        transform.DOMove(targetPost.position, 0.5f)
                 .OnComplete(() => callback?.Invoke());

        animation.Play("RotateLeft");
    }

    public void Scale(float time, Action callBack)
    {
   
        this.transform.DOScale(new Vector3(1, 1, 1), time).OnComplete(delegate { animation.Play("RotateLeft"); callBack?.Invoke(); });

    }

    public void LocalMoveScrew(Transform targetPost, Action callback)
{
    Vector3 targetPos = targetPost.position;

    transform.DOMove(targetPos, 0.3f)
        .OnComplete(() =>
        {
            transform.SetParent(targetPost);
            transform.localPosition = Vector3.zero;
            callback?.Invoke();
        });

    animation.Play("RotateLeft");
}
}
