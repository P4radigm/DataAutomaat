using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using UnityEngine.SceneManagement;

public class InactivityManager : MonoBehaviour
{
    [SerializeField] private float inactivityTriggerTime;
    [SerializeField] private float autoToStartTime;
    private float inactivityTimer;
    private float toStartTimer;
    //private float manualRestartTimer;
    //[SerializeField] private float manualRestartTime;
    [SerializeField] private GameObject inactivityCanvas;
    private bool inactivityCanvasActive = false;
    [SerializeField] private Disc toStartVisual;
    private StateManager stateManager;


    private void Start()
    {
        stateManager = StateManager.instance;
        inactivityTimer = inactivityTriggerTime;
        toStartTimer = autoToStartTime;
    }

    private void Update()
    {
        if (CheckActivity()) { inactivityTimer = inactivityTriggerTime; }

        if(inactivityTimer > 0) {  }
        else if(inactivityTimer <= 0) { inactivityCanvas.SetActive(true); inactivityCanvasActive = true; }

        if (inactivityCanvasActive)
        {
            if(toStartTimer > 0) { toStartVisual.AngRadiansEnd = Mathf.Deg2Rad * ((toStartTimer / autoToStartTime) * 360f); }
            else if(toStartTimer <= 0) { ToStart(); }

            toStartTimer -= Time.deltaTime;
        }
        else
        {
            toStartTimer = autoToStartTime;
        }

        if(stateManager.loadedSegment == StateManager.ExperienceState.Start || stateManager.loadedSegment == StateManager.ExperienceState.Thanks)
        {
            inactivityTimer = inactivityTriggerTime;
            toStartTimer = autoToStartTime;
        }
        else
        {
            inactivityTimer -= Time.deltaTime;
        }
    }

    public void ToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        inactivityTimer = inactivityTriggerTime;
        toStartTimer = autoToStartTime;
        inactivityCanvas.SetActive(false);
        inactivityCanvasActive = false;
    }

    private bool CheckActivity()
    {
        bool returnBool = false;

        // Mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0f || mouseY != 0f)
        {
            returnBool = true;
        }

        // Mouse buttons
        if (Input.GetMouseButtonDown(0))
        {
            returnBool = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            returnBool = true;
        }

        if(Input.touchCount > 0)
        {
            returnBool = true;
        }

        // Scroll wheel
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0f)
        {
            returnBool = true;
        }

        // Keyboard input
        if (Input.anyKeyDown)
        {
            returnBool = true;
        }

        return returnBool;
    }
}
