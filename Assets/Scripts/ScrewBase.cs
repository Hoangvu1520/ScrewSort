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

    public void MoveScrew(Transform targetPost, Action callback)
    {
        transform.DOMove(targetPost.position, 0.5f)
                 .OnComplete(() => callback?.Invoke());

        animation.Play("RotateLeft");
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
