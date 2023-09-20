using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BundleMenuController : MonoBehaviour
{
    public static BundleMenuController instance;

    [SerializeField] private Image bundleVisual;
    [SerializeField] private TextMeshProUGUI bundleText;
    [SerializeField] private TextMeshProUGUI bundleTitleText;
    [SerializeField] private Button terugKnop;
    [SerializeField] private Button verwijderKnop;
    [SerializeField] private Button klaarKnop;
    [TextArea][SerializeField] private string stockText;
    [Space(20)]
    public Bundle loadedBundle;
    public List<Activity> loadedActivity = new();
    public List<ActivitySelection> loadedActivitySelection = new();
    public List<BundleSimpleHandler> simplePrefabs = new();
    public List<BundleOptionsHandler> optionPrefabs = new();
    [Space(20)]
    [SerializeField] private GameObject bundleOptionsPrefab;
    [SerializeField] private GameObject bundleSimplePrefab;
    [SerializeField] private float startYPos;
    [SerializeField] private float YPosSteps;
    [SerializeField] private float XPos;

    private ContentManager contentManager;
    private BonnetjeController bonnetjeController;

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
        bonnetjeController = BonnetjeController.instance;
        gameObject.SetActive(false);
    }

    public void Init(Bundle bundleToLoad, bool isEdit)
    {
        for (int i = 0; i < simplePrefabs.Count; i++)
        {
            Destroy(simplePrefabs[i].gameObject);
        }

        for (int i = 0; i < optionPrefabs.Count; i++)
        {
            Destroy(optionPrefabs[i].gameObject);
        }

        loadedActivity.Clear();
        simplePrefabs.Clear();
        optionPrefabs.Clear();
        loadedActivitySelection.Clear();

        loadedActivity = new();
        simplePrefabs = new();
        optionPrefabs = new();
        loadedActivitySelection = new();

        if (contentManager == null) { contentManager = ContentManager.instance; }

        loadedBundle = bundleToLoad;

        foreach (ActivitySelection item in loadedBundle.activities)
        {
            loadedActivitySelection.Add(item);
        }

        foreach (ActivitySelection activity in loadedActivitySelection)
        {
            loadedActivity.Add(Array.Find(contentManager.referenceActivities, obj => obj.name == activity.name));
        }

        for (int i = 0; i < loadedActivity.Count; i++)
        {
            if (loadedActivity[i].options.Length != 0)
            {
                GameObject go = Instantiate(bundleOptionsPrefab, transform);
                RectTransform rt = go.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(XPos, startYPos - YPosSteps * i);
                BundleOptionsHandler boh = go.GetComponent<BundleOptionsHandler>();
                boh.Init(loadedActivity[i], loadedActivitySelection[i], isEdit);
                optionPrefabs.Add(boh);
            }
            else
            {
                GameObject go = Instantiate(bundleSimplePrefab, transform);
                RectTransform rt = go.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(XPos, startYPos - YPosSteps * i);
                BundleSimpleHandler bsh = go.GetComponent<BundleSimpleHandler>();
                bsh.Init(loadedActivity[i], loadedActivitySelection[i], isEdit);
                simplePrefabs.Add(bsh);
            }
        }

        bundleVisual.sprite = loadedBundle.visual;
        bundleText.text = loadedBundle.name;
        bundleTitleText.text = stockText.Replace("[bundel]", loadedBundle.name);
        verwijderKnop.gameObject.SetActive(isEdit);
    }

    public void PressBack()
    {
        bonnetjeController.UpdateContent();

        contentManager.LoadCategory(contentManager.selectedCatId);

        this.gameObject.SetActive(false);
    }

    public void PressRemove()
    {
        contentManager.selectedActivities.RemoveAll(obj => obj.connectedBundleName == loadedBundle.name);

        bonnetjeController.UpdateContent();

        contentManager.LoadCategory(contentManager.selectedCatId);

        this.gameObject.SetActive(false);
    }

    public void PressAdd()
    {
        contentManager.selectedActivities.RemoveAll(obj => obj.connectedBundleName == loadedBundle.name);
        for (int i = 0; i < simplePrefabs.Count; i++)
        {
            if (simplePrefabs[i].isActive) { contentManager.selectedActivities.Add(simplePrefabs[i].loadedActivitySelection); }
        }
        for (int i = 0; i < optionPrefabs.Count; i++)
        {
            if (optionPrefabs[i].isActive) { contentManager.selectedActivities.Add(optionPrefabs[i].loadedActivitySelection); }
        }

        bonnetjeController.UpdateContent();

        contentManager.LoadCategory(contentManager.selectedCatId);

        this.gameObject.SetActive(false);
    }
}
