using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

[RequireComponent(typeof(ShapeRenderer))]
public class ShapesOpacityAnimation : MonoBehaviour
{
    private ShapeRenderer ownShape;
    [SerializeField] private bool loop;
    [SerializeField] private bool onStart;
    [SerializeField] private float onStartTargetOpacity;
    [SerializeField] private float animTime;
    public float AnimTime { get { return animTime; } set { animTime = value; } }
    [SerializeField] private AnimationCurve animCurve;
    private Coroutine animRoutine;

    private void Start()
    {
        ownShape = GetComponent<ShapeRenderer>();
        if (onStart) { AnimateOpacityTo(onStartTargetOpacity); }
    }

    public void AnimateOpacityTo(float targetOpacity)
    {
        if (ownShape == null) { ownShape = GetComponent<ShapeRenderer>(); }
        if (animRoutine != null) { StopCoroutine(animRoutine); }
        animRoutine = StartCoroutine(AnimateOpacityToIE(targetOpacity));
    }

    public IEnumerator AnimateOpacityToIE(float targetOpacity)
    {
        float timeValue = 0;
        float startOpacity = ownShape.Color.a;

        while (timeValue < 1)
        {
            timeValue += Time.deltaTime / animTime;
            float evaluatedTimeValue = animCurve.Evaluate(timeValue);
            float newOpacity = Mathf.Lerp(startOpacity, targetOpacity, evaluatedTimeValue);
            ownShape.Color = new Color(ownShape.Color.r, ownShape.Color.g, ownShape.Color.b, newOpacity);

            yield return null;
        }

        if (loop) { AnimateOpacityTo(targetOpacity); }

        yield return null;

        animRoutine = null;
    }
}
