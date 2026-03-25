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

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            DetectObject(Input.mousePosition);
        }
        //android and ios
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            DetectObject(Input.touches[0].position);
        }
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

        int movingId = pile.screwList[top].id;

        while (top >= 0 &&
               bottom < targetPile.slotNum &&
               pile.screwList[top].id == movingId)
        {
            // Store reference before modifying list
            ScrewBase movingScrew = pile.screwList[top];

            bool finished = false;

            // Step 1: Lift
            if (!movingScrew.inFirstPos)
            {
                movingScrew.LocalMoveScrew(pile.firstPost, () => finished = true);
                yield return new WaitUntil(() => finished);

                finished = false;
            }
            else
            {
                yield return null;
            }


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
            else if (currentPile != null && pile != null
         && currentPile != pile
         && currentPile.firstScrew.id == pile.firstScrew.id)
            {

                // currentPile.firstScrew.MoveScrew(currentPile.firstScrewPos, delegate { });
                // currentPile = pile;
                StartCoroutine(HandleChangeStackScrew(currentPile, pile, () => { currentPile = null; }));
            }
            else if (currentPile != null && pile != null && currentPile.firstScrew.id != pile.firstScrew.id)
            {

                pile.firstScrew.MoveScrew(currentPile.firstScrewPos, delegate { currentPile = pile; });
            }
        }
    }
}
