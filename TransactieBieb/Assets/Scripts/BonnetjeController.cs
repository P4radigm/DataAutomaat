using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonnetjeController : MonoBehaviour
{
    static public BonnetjeController instance;

    [SerializeField] private TextMeshProUGUI itemCounter;

    [SerializeField] private Animator bonnetjeAnimator;

    [SerializeField] private RectTransform bonnetjeRectTransform;

    [SerializeField] private GameObject bundleStartPrefab;
    [SerializeField] private GameObject bundleEndPrefab;
    [SerializeField] private GameObject activityOptionPrefab;
    [SerializeField] private GameObject activitySimplePrefab;
    [SerializeField] private RectTransform scrollViewContent;
    [Space(20)]
    [SerializeField] private float usualDistance;
    [SerializeField] private float bundleStartDistance;
    [SerializeField] private float bundleEndDistance;
    [SerializeField] private float bundleEndMinusDistance;
    [SerializeField] private float YPosStart;
    [SerializeField] private float topMargin;
    [SerializeField] private float botMargin;
    private float YPosCounter;
    public List<GameObject> instantiatedObjects = new();

    private ContentManager contentManager;

    private bool bundleActive = false;
    private string prevBundleName = "Start";

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
        contentManager = ContentManager.instance;
    }

    public void UpdateContent()
    {
        foreach (GameObject item in instantiatedObjects)
        {
            Destroy(item);
        }

        instantiatedObjects = new();

        itemCounter.text = contentManager.selectedActivities.Count.ToString();

        YPosCounter = YPosStart;

        bundleActive = false;

        for (int i = 0; i < contentManager.selectedActivities.Count; i++)
        {
            if (contentManager.selectedActivities[i].connectedBundleName != "No")
            {
                if(contentManager.selectedActivities[i].connectedBundleName != prevBundleName && prevBundleName != "Start" && prevBundleName != "No")
                {
                    YPosCounter += bundleEndMinusDistance;
                    //Place bundle end
                    GameObject go = Instantiate(bundleEndPrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeBundleEnd bbe = go.GetComponent<BonnetjeBundleEnd>();
                    bbe.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= bundleEndDistance;

                    bundleActive = false;
                }

                if (!bundleActive)
                {
                    GameObject go = Instantiate(bundleStartPrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeBundleStart bbs = go.GetComponent<BonnetjeBundleStart>();
                    bbs.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= bundleStartDistance;
                }

                //Place entry
                if(contentManager.selectedActivities[i].selectedOptionName == "No")
                {
                    GameObject go = Instantiate(activitySimplePrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeSimpleEntry bse = go.GetComponent<BonnetjeSimpleEntry>();
                    bse.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= usualDistance;
                }
                else
                {
                    GameObject go = Instantiate(activityOptionPrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeOptionEntry boe = go.GetComponent<BonnetjeOptionEntry>();
                    boe.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= usualDistance;
                }

                bundleActive = true;
                prevBundleName = contentManager.selectedActivities[i].connectedBundleName;
            }
            else
            {
                if (bundleActive)
                {
                    YPosCounter += bundleEndMinusDistance;

                    //Place bundle end
                    GameObject go = Instantiate(bundleEndPrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeBundleEnd bbe = go.GetComponent<BonnetjeBundleEnd>();
                    bbe.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= bundleEndDistance;

                    bundleActive = false;
                }

                //Place entry
                if (contentManager.selectedActivities[i].selectedOptionName == "No")
                {
                    GameObject go = Instantiate(activitySimplePrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeSimpleEntry bse = go.GetComponent<BonnetjeSimpleEntry>();
                    bse.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= usualDistance;
                }
                else
                {
                    GameObject go = Instantiate(activityOptionPrefab, scrollViewContent);
                    RectTransform rt = go.GetComponent<RectTransform>();
                    rt.anchoredPosition = new Vector2(0, YPosCounter);
                    BonnetjeOptionEntry boe = go.GetComponent<BonnetjeOptionEntry>();
                    boe.LoadWithContent(contentManager.selectedActivities[i]);
                    instantiatedObjects.Add(go);

                    YPosCounter -= usualDistance;
                }
            }
        }

        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, Mathf.Abs(YPosCounter) + botMargin);
    }

    public void Open()
    {
        UpdateContent();
        bonnetjeAnimator.SetTrigger("ToBekijken");
    }

    public void Done()
    {
        bonnetjeAnimator.SetTrigger("ToKlaar");
    }

    public void Back()
    {
        bonnetjeAnimator.SetTrigger("ToBekijken");
    }

    public void Close()
    {
        bonnetjeAnimator.SetTrigger("ToClosed");
    }

    public void Sure()
    {

    }
}
