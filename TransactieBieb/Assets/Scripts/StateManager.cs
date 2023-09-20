using iText.Commons.Actions.Contexts;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class StateManager : MonoBehaviour
{
    [System.Serializable]
    public class BezoekerData
    {
        public int bezoekerID;
    }

    public BezoekerData currentBezoekerData;
    [SerializeField] private TextMeshProUGUI bezoekerNoOne;
    [SerializeField] private TextMeshProUGUI bezoekerNoTwo;

    public static StateManager instance;

    [SerializeField] private ExperienceState startState;
    public ExperienceState loadedSegment;

    [SerializeField] private GameObject[] stateParents;
    [SerializeField] private float transitionTime;

    [SerializeField] private ContentManager contentmanager;
    [SerializeField] private ThanksManager thanksManager;
    [SerializeField] private PrinterManager printerManager;

    private Coroutine transitionRoutine;
    private TransitionManager transitionManager;

    public enum ExperienceState
    {
        Start,
        Selection,
        Thanks
    }

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
        transitionManager = TransitionManager.instance;
        LoadExperienceSegment((int)startState);

        currentBezoekerData = LoadBezoekerData();

    }

    public void LoadExperienceSegment(int newSegment)
    {
        if (transitionRoutine != null) { StopCoroutine(transitionRoutine); transitionRoutine = null; }
        StartCoroutine(IEtransition(newSegment));
    }

    private IEnumerator IEtransition(int newSegment)
    {
        switch ((ExperienceState)newSegment)
        {
            case ExperienceState.Start:
                //transitionManager.Transition(transitionTime);
                break;
            case ExperienceState.Selection:
                currentBezoekerData.bezoekerID++;
                bezoekerNoOne.text = $"N  {currentBezoekerData.bezoekerID.ToString().PadLeft(6, '0')}";
                bezoekerNoTwo.text = $"N  {currentBezoekerData.bezoekerID.ToString().PadLeft(6, '0')}";
                SaveBezoekerData(currentBezoekerData);
                transitionManager.Transition(transitionTime);
                break;
            case ExperienceState.Thanks:
                transitionManager.Transition(transitionTime);
                break;
            default:
                transitionManager.Transition(transitionTime);
                break;
        }
        yield return new WaitForSeconds(transitionTime / 2);
        DisableAllSegments();
        stateParents[newSegment].SetActive(true);
        switch ((ExperienceState)newSegment)
        {
            case ExperienceState.Start:
                break;
            case ExperienceState.Selection:
                contentmanager.Init();
                break;
            case ExperienceState.Thanks:
                thanksManager.Init();
                contentmanager.Print();
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(transitionTime / 2);

        loadedSegment = (ExperienceState)newSegment;

        transitionRoutine = null;
    }

    private void DisableAllSegments()
    {
        foreach (GameObject segment in stateParents)
        {
            segment.SetActive(false);
        }
    }

    public void PressStart()
    {
        LoadExperienceSegment((int)ExperienceState.Selection);
    }

    public void PressPay()
    {
        LoadExperienceSegment((int)ExperienceState.Thanks);
    }

    public void PressReset()
    {
        SceneManager.LoadScene(0);
    }

    public BezoekerData LoadBezoekerData()
    {
        string filePath = Application.persistentDataPath + "/bezoekerID.json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<BezoekerData>(jsonData);
        }
        else
        {
            BezoekerData newData = new BezoekerData();
            newData.bezoekerID = 1;
            SaveBezoekerData(newData);
            return newData;
        }
    }

    public void SaveBezoekerData(BezoekerData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/bezoekerID.json";
        File.WriteAllText(filePath, jsonData);
    }

}
