using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    public PileBase currentPile;
    public PileBase targetPile;
    public float cooldown;
    public bool wasTouchPile;

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            DetectObject(Input.mousePosition);
        }
#endif
        //android and ios
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            DetectObject(Input.touches[0].position);
        }
#endif
    }
    private IEnumerator HandleChangeStackScrew(PileBase pile, PileBase targetPile,
    Action onComplete)
    {
        if (pile.screwList.Count == 0)
            yield break;

        int top = pile.screwList.Count - 1;
        int bottom = targetPile.screwList.Count;

        // Color check
        if (targetPile.screwList.Count > 0 &&
            pile.screwList[top].id != targetPile.firstScrew.id)
            yield break;
        if (targetPile.screwPostList == null || bottom >= targetPile.screwPostList.Count)
        {
            yield break;
        }

        int movingId = pile.firstScrew.id;

        while (top >= 0 &&
               bottom < targetPile.screwPostList.Count &&
               pile.screwList[top].id == movingId)
        {
            // Store reference before modifying list
            ScrewBase movingScrew = pile.screwList[top];

            bool finished = false;

            // Step 1: Lift

            movingScrew.LocalMoveScrew(pile.firstPost, () => finished = true);
            yield return new WaitUntil(() => finished);

            finished = false;

            // Step 2: Move above target
            movingScrew.LocalMoveScrew(targetPile.firstPost, () => finished = true);
            yield return new WaitUntil(() => finished);

            finished = false;

            // Step 3: Drop to slot
            movingScrew.LocalMoveScrew(targetPile.screwPostList[bottom], () => finished = true);
            yield return new WaitUntil(() => finished);

            // Update lists after animation completes
            pile.screwList.RemoveAt(top);
            targetPile.screwList.Add(movingScrew);

            top--;
            bottom++;
        }
        onComplete?.Invoke();
    }

    private void DetectObject(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            PileBase pile = hit.collider.GetComponent<PileBase>();

            if (currentPile == null && pile != null)
            {
                currentPile = pile;
                pile.firstScrew.MoveScrew(currentPile.firstPost, delegate { currentPile.firstScrew.inFirstPos = true; });
            }
            else if (currentPile != null && pile != null && currentPile != pile)
            {
                // Case 1: target pile is empty
                if (pile.screwList.Count == 0)
                {
                    StartCoroutine(HandleChangeStackScrew(currentPile, pile, () =>
                    {
                        currentPile = null;
                    }));
                }
                // Case 2: same color
                else if (currentPile.firstScrew != null &&
                         pile.firstScrew != null &&
                         currentPile.firstScrew.id == pile.firstScrew.id)
                {
                    StartCoroutine(HandleChangeStackScrew(currentPile, pile, () =>
                    {
                        currentPile = null;
                    }));
                }
            }
            else if (currentPile != null && pile != null && currentPile.firstScrew.id != pile.firstScrew.id)
            {

                pile.firstScrew.MoveScrew(currentPile.firstScrewPos, delegate { currentPile = pile; });
            }
        }
    }
}
