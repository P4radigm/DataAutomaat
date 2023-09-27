using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SinglePageContentManager : MonoBehaviour
{
    [SerializeField] private StateManager stateManager;
    [SerializeField] private AdsManager adsManager;
    public SinglePageItemController[] menuItemsCP;
    public SinglePageItemController[] menuItemsO;
    [SerializeField] private GameObject[] staticItemsCP;
    [SerializeField] private GameObject[] staticItemsO;
    [SerializeField] private RectTransform parentAdCP;
    [SerializeField] private RectTransform parentAdO;
    [SerializeField] private TextMeshProUGUI itemCounter;


    public void Init()
    {
        stateManager = StateManager.instance;

        foreach (GameObject go in staticItemsCP)
        {
            go.SetActive(!stateManager.isSelectingOrganisation);
        }

        foreach (GameObject go in staticItemsO)
        {
            go.SetActive(stateManager.isSelectingOrganisation);
        }

        foreach(SinglePageItemController controller in menuItemsCP)
        {
            controller.Set(false);
        }

        foreach (SinglePageItemController controller in menuItemsO)
        {
            controller.Set(false);
        }

        if (stateManager.isSelectingOrganisation)
        {
            adsManager.parentAdTransform = parentAdO;
            parentAdO.gameObject.SetActive(true);
            parentAdCP.gameObject.SetActive(false);
        }
        else
        {
            adsManager.parentAdTransform = parentAdCP;
            parentAdO.gameObject.SetActive(false);
            parentAdCP.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        int counter = 0;

        foreach (SinglePageItemController controller in menuItemsCP)
        {
            if (controller.selected)
            {
                counter++;
            }
        }

        foreach (SinglePageItemController controller in menuItemsO)
        {
            if (controller.selected)
            {
                counter++;
            }
        }

        itemCounter.text = counter.ToString();
    }
}
