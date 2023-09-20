using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using UnityEngine.SceneManagement;

public class ThanksManager : MonoBehaviour
{
    [SerializeField] private float autoToStartTime;
    private float toStartTimer;
    [SerializeField] private Disc toStartVisual;

    private StateManager stateManager;

    public void Init()
    {
        stateManager = StateManager.instance;
        toStartTimer = autoToStartTime;
    }

    private void Update()
    {
        if (toStartTimer > 0) { toStartVisual.AngRadiansEnd = Mathf.Deg2Rad * ((toStartTimer / autoToStartTime) * 360f); }
        else if (toStartTimer <= 0) { ToStart(); }

        toStartTimer -= Time.deltaTime;
    }

    public void ToStart()
    {
        SceneManager.LoadScene(0);
    }
}
