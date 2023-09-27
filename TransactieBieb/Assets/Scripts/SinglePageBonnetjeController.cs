using iText.StyledXmlParser.Node;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePageBonnetjeController : MonoBehaviour
{
    [SerializeField] private RectTransform entryParentTransform;
    [SerializeField] private Vector2 entryStartPosition;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private float spawnDistance;
    [SerializeField] private SinglePageContentManager contentManager;
    [SerializeField] private RectTransform scrollViewContent;
    [SerializeField] private float topMargin;
    [SerializeField] private float botMargin;

    private StateManager stateManager;

    private List<GameObject> spawnedEntries = new();

    [SerializeField] private Animator bonnetjeAnimator;

    public void Init()
    {
        stateManager = StateManager.instance;
        spawnedEntries.Clear();
    }

    public void UpdateContent()
    {
        foreach (GameObject item in spawnedEntries)
        {
            Destroy(item);
        }

        spawnedEntries.Clear();

        if (stateManager.isSelectingOrganisation)
        {
            for (int i = 0; i < contentManager.menuItemsO.Length; i++)
            {
                if (!contentManager.menuItemsO[i].selected) { continue; }
                GameObject entry = Instantiate(entryPrefab, scrollViewContent);
                RectTransform rt = entry.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(entryStartPosition.x, entryStartPosition.y - spawnedEntries.Count * spawnDistance);
                SinglePageBonnetjeItem itemController = entry.GetComponent<SinglePageBonnetjeItem>();
                itemController.displayIcon.sprite = contentManager.menuItemsO[i].icon;
                itemController.titleText.text = contentManager.menuItemsO[i].bonTitle;
                spawnedEntries.Add(entry);
            }
        }
        else
        {
            for (int i = 0; i < contentManager.menuItemsCP.Length; i++)
            {
                if (!contentManager.menuItemsCP[i].selected) { continue; }
                GameObject entry = Instantiate(entryPrefab, scrollViewContent);
                RectTransform rt = entry.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(entryStartPosition.x, entryStartPosition.y - spawnedEntries.Count * spawnDistance);
                SinglePageBonnetjeItem itemController = entry.GetComponent<SinglePageBonnetjeItem>();
                itemController.displayIcon.sprite = contentManager.menuItemsCP[i].icon;
                itemController.titleText.text = contentManager.menuItemsCP[i].bonTitle;
                spawnedEntries.Add(entry);
            }

        }
        
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, spawnedEntries.Count * spawnDistance + topMargin + botMargin);
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
