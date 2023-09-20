using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance = null;

    [SerializeField] private AnimationCurve inCurve;
    [SerializeField] private AnimationCurve outCurve;

    [SerializeField] private Image panel;

    private Coroutine transitionRoutine = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        panel.color = new Color(1, 1, 1, 0);
    }

    public void Transition(float time)
    {
        if(transitionRoutine != null) { StopCoroutine(transitionRoutine); transitionRoutine = null; }
        transitionRoutine = StartCoroutine(IEtransition(time, panel));

    }

    private IEnumerator IEtransition(float time, Image panel)
    {
        float timeValueIn = 0;
        float timeValueOut = 0;
        panel.color = new Color(1, 1, 1, 0);

        while (timeValueIn < 1)
        {
            timeValueIn += Time.deltaTime / (time / 2);
            float evaluatedTimeValue = inCurve.Evaluate(timeValueIn);
            float newAlpha = Mathf.Lerp(0, 1, evaluatedTimeValue);

            panel.color = new Color(1, 1, 1, newAlpha);

            yield return null;
        }

        while (timeValueOut < 1)
        {
            timeValueOut += Time.deltaTime / (time / 2);
            float evaluatedTimeValue = outCurve.Evaluate(timeValueOut);
            float newAlpha = Mathf.Lerp(1, 0, evaluatedTimeValue);

            panel.color = new Color(1, 1, 1, newAlpha);

            yield return null;
        }

        transitionRoutine = null;
    }
}
