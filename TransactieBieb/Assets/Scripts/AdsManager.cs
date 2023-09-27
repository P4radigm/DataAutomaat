using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class AdsManager : MonoBehaviour
{
    public RectTransform parentAdTransform;
    [SerializeField] private float[] targetXPositions;
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private AnimationCurve nextHighlightCurve;
    [SerializeField] private AnimationCurve prevHighlightCurve;
    [SerializeField] private float moveTimeStandard;
    [SerializeField] private float moveTimeAddedPerAdToMoveThrough;
    [SerializeField] private float displayTime;
    [SerializeField] private Rectangle[] highlightRects;
    private float moveTimer;
    private int currentlyShownAdID;
    private bool isAnimating = false;
    private Coroutine moveRoutine;

    private void Start()
    {
        foreach (Rectangle rect in highlightRects)
        {
            rect.Color = new Color(0, 0, 0, 0);
        }

        SnapToAd(Random.Range(0, 3));
    }

    private void Update()
    {
        if (moveTimer <= 0)
        {
            AnimateToAd((currentlyShownAdID + 1) % 3);
            moveTimer = displayTime;
        }
        else if (isAnimating)
        {
            //Do nothing
            moveTimer = displayTime;
        }

        moveTimer -= Time.deltaTime;
    }

    public void AnimateToAd(int AdID)
    {
        if(moveRoutine != null) { StopCoroutine(moveRoutine); }
        moveRoutine = StartCoroutine(AnimateToAdIE(AdID));
        isAnimating = true;
    }

    private IEnumerator AnimateToAdIE(int AdID)
    {
        float timeValue = 0;
        Vector2 startLocation = parentAdTransform.anchoredPosition;
        Vector2 targetLocation = new Vector2(targetXPositions[AdID], parentAdTransform.anchoredPosition.y);

        float startOpacityPrev = highlightRects[currentlyShownAdID].Color.a;
        float startOpacityNext = highlightRects[AdID].Color.a;

        for (int i = 0; i < highlightRects.Length; i++)
        {
            if (i != AdID && i != currentlyShownAdID)
            {
                highlightRects[AdID].Color = new Color(0, 0, 0, 0);
            }
        }

        while (timeValue < 1)
        {
            timeValue += Time.deltaTime / (moveTimeStandard + Mathf.Abs(currentlyShownAdID-AdID)*moveTimeAddedPerAdToMoveThrough);
            float evaluatedTimeValueMove = moveCurve.Evaluate(timeValue);
            float evaluatedTimeValuePrevOpacity = prevHighlightCurve.Evaluate(timeValue);
            float evaluatedTimeValueNextOpacity = nextHighlightCurve.Evaluate(timeValue);
            
            //AnimateMove
            Vector2 newLocation = Vector2.Lerp(startLocation, targetLocation, evaluatedTimeValueMove);
            parentAdTransform.anchoredPosition = newLocation;

            //AnimateHighlightRectangles
            float newOpacityPrev = Mathf.Lerp(startOpacityPrev, 0, evaluatedTimeValuePrevOpacity);
            float newOpacityNext = Mathf.Lerp(startOpacityNext, 1, evaluatedTimeValueNextOpacity);

            highlightRects[currentlyShownAdID].Color = new Color(0, 0, 0, newOpacityPrev);
            highlightRects[AdID].Color = new Color(0, 0, 0, newOpacityNext);

            yield return null;
        }

        yield return null;

        moveTimer = displayTime;
        currentlyShownAdID = AdID;
        isAnimating = false;

        moveRoutine = null;
    }

    public void SnapToAd(int AdID)
    {
        foreach (Rectangle rect in highlightRects)
        {
            rect.Color = new Color(0, 0, 0, 0);
        }

        highlightRects[AdID].Color = new Color(0, 0, 0, 1);

        if (moveRoutine != null) { StopCoroutine(moveRoutine); }

        parentAdTransform.anchoredPosition = new Vector2(targetXPositions[AdID], parentAdTransform.anchoredPosition.y);

        moveTimer = displayTime;
        currentlyShownAdID = AdID;
        isAnimating = false;
    }
}
