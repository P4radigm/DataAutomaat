using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonnetjeBundleStart : MonoBehaviour
{
    public ActivitySelection loadedActivitySelection;

    [SerializeField] private TextMeshProUGUI title;

    private ContentManager contentManager;
    private BonnetjeController bonnetjeController;

    public void LoadWithContent(ActivitySelection activitySelection)
    {
        if (contentManager == null) { contentManager = ContentManager.instance; }
        if (bonnetjeController == null) { bonnetjeController = BonnetjeController.instance; }

        loadedActivitySelection = activitySelection;

        title.text = $"{loadedActivitySelection.connectedBundleName} Bundel";
    }
}
