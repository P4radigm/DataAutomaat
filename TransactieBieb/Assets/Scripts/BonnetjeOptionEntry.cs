using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonnetjeOptionEntry : MonoBehaviour
{
    public ActivitySelection loadedActivitySelection;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI option;

    private ContentManager contentManager;
    private BonnetjeController bonnetjeController;

    public void LoadWithContent(ActivitySelection activitySelection)
    {
        if (contentManager == null) { contentManager = ContentManager.instance; }
        if (bonnetjeController == null) { bonnetjeController = BonnetjeController.instance; }

        loadedActivitySelection = activitySelection;

        title.text = loadedActivitySelection.name;
        option.text = loadedActivitySelection.selectedOptionName;
    }

    public void PressRemove()
    {
        contentManager.selectedActivities.RemoveAll(obj => obj.name == loadedActivitySelection.name && obj.connectedBundleName == loadedActivitySelection.connectedBundleName && obj.selectedOptionName == loadedActivitySelection.selectedOptionName);
        bonnetjeController.UpdateContent();
    }
}
