using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    static public ContentManager instance;

    public enum Activities
    {
        AanwezigZijnInHetGebouw,
        KluisjeGebruiken,
        InformatieVragen,
        AbonnementAfsluiten,
        MateriaalTerugbrengen,
        MateriaalInzien,
        MateriaalLenen,
        ZoekenInDeCatalogus,
        StroomGebruiken,
        WiFiGebruiken,
        DesktopGebruiken,
        Printen,
        EvenRondkijken,
        TentoonstellingOfActiviteitBezoeken,
        Spelen,
        IetsHalenBijHetCafe,
        WcGebruiken,
        WaterBijvullen,
        WerkenAanEenTafel,
        Vergaderen,
    }

    public List<ActivitySelection> selectedActivities = new List<ActivitySelection>();
    [Space(20)]
    [SerializeField] private Scrollbar itemMenuScrollbar;
    [SerializeField] private RectTransform itemMenuRectTransform;
    [SerializeField] private string[] categoryNames;
    [SerializeField] private float[] categoryScrollHeights;
    [SerializeField] private TextMeshProUGUI categoryTitleText;
    public CategoryButtonHandler[] categoryButtons;
    public BundleButtonHandler[] bundleButtons;
    public itemButtonContainer[] itemButtons;
    public Activity[] referenceActivities;
    public GameObject[] categoryParentObjects;
    [Space(20)]
    [SerializeField] private int startCatId;
    [Space(20)]
    public int selectedCatId;

    [SerializeField] private PrinterManager printerManager;

    [System.Serializable]
    public class itemButtonContainer
    {
        public string id;
        public ItemButtonHandler[] itemButtons;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        LoadCategory(startCatId);
    }

    public void LoadCategory(int CatID)
    {
        itemMenuRectTransform.sizeDelta = new Vector2(itemMenuRectTransform.sizeDelta.x, categoryScrollHeights[CatID]);
        itemMenuScrollbar.value = 1f;

        foreach (GameObject parent in categoryParentObjects)
        {
            parent.SetActive(false);
        }

        foreach(CategoryButtonHandler button in categoryButtons)
        {
            button.UnSelected();
        }

        categoryParentObjects[CatID].SetActive(true);
        categoryButtons[CatID].Selected();


        if (CatID == 0)
        {
            foreach(BundleButtonHandler button in bundleButtons)
            {
                button.Init();
            }
        }
        else if(CatID != 6)
        {
            foreach (ItemButtonHandler button in itemButtons[CatID-1].itemButtons)
            {
                button.Init();
            }
        }

        categoryTitleText.text = categoryNames[CatID];

        selectedCatId = CatID;
    }

    public void LookAtBundle(int BundleID)
    {
        itemMenuRectTransform.sizeDelta = new Vector2(itemMenuRectTransform.sizeDelta.x, categoryScrollHeights[0]);

        foreach (GameObject parent in categoryParentObjects)
        {
            parent.SetActive(false);
        }

        foreach (CategoryButtonHandler button in categoryButtons)
        {
            button.UnSelected();
        }

        categoryParentObjects[0].SetActive(true);
        categoryButtons[0].Selected();

        foreach (BundleButtonHandler button in bundleButtons)
        {
            button.Init();
        }

        categoryTitleText.text = categoryNames[0];

        selectedCatId = 0;

        if(BundleID == 0 || BundleID == 1) { itemMenuScrollbar.value = 1f; }
        else if(BundleID == 2 || BundleID == 3) { itemMenuScrollbar.value = 0.5f; }
        else if(BundleID == 4 || BundleID == 5) { itemMenuScrollbar.value = 0f; }     
    }

    public void Print()
    {
        List<string> allLines = new();

        for (int i = 0; i < selectedActivities.Count; i++)
        {
            for (int j = 0; j < selectedActivities[i].receiptLines.Length; j++)
            {
                allLines.Add(selectedActivities[i].receiptLines[j]);
            }
        }

        printerManager.PrintReceipt();
    }
}
