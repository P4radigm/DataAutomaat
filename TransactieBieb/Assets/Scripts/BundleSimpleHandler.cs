using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Shapes;

public class BundleSimpleHandler : MonoBehaviour
{
    public Activity loadedActivity;
    public ActivitySelection loadedActivitySelection;
    public bool isActive;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image[] minusImages;
    [SerializeField] private Image[] plusImages;
    [SerializeField] private Rectangle minusOutline;
    [SerializeField] private Rectangle plusOutline;

    private ContentManager contentManager;

    public void Init(Activity activityToLoad, ActivitySelection activitySelectionToLoad, bool isEdit)
    {
        if(contentManager == null) { contentManager = ContentManager.instance; }
        loadedActivitySelection = activitySelectionToLoad;
        loadedActivity = activityToLoad;
        title.text = activityToLoad.name;
        ToggleFalse();

        bool selectedBefore = false;
        for (int i = 0; i < contentManager.selectedActivities.Count; i++)
        {
            if (contentManager.selectedActivities[i].name == loadedActivity.name && contentManager.selectedActivities[i].connectedBundleName == loadedActivitySelection.connectedBundleName)
            {
                selectedBefore = true;
            }
        }

        loadedActivitySelection.receiptLines = loadedActivity.stockReceiptLines;

        if (selectedBefore) { ToggleTrue(); }

        if (!isEdit) { ToggleTrue(); }
    }

    public void ToggleFalse()
    {
        title.color = new Color(0, 0, 0, 0.4f);

        for (int i = 0; i < minusImages.Length; i++)
        {
            minusImages[i].color = new Color(0, 0, 0, 0.4f);
        }
        minusOutline.Color = new Color(0, 0, 0, 0.4f);
        removeButton.interactable = false;

        for (int i = 0; i < plusImages.Length; i++)
        {
            plusImages[i].color = new Color(0, 0, 0, 1f);
        }
        plusOutline.Color = new Color(0, 0, 0, 1f);
        addButton.interactable = true;

        isActive = false;
    }

    public void ToggleTrue()
    {
        title.color = new Color(0, 0, 0, 1f);

        for (int i = 0; i < minusImages.Length; i++)
        {
            minusImages[i].color = new Color(0, 0, 0, 1f);
        }
        minusOutline.Color = new Color(0, 0, 0, 1f);
        removeButton.interactable = true;

        for (int i = 0; i < plusImages.Length; i++)
        {
            plusImages[i].color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }
        plusOutline.Color = new Color(0, 0, 0, 0.4f);
        addButton.interactable = false;

        isActive = true;
    }
}
