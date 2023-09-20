using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonnetjeSimpleEntry : MonoBehaviour
{
    public ActivitySelection loadedActivitySelection;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image[] outlineImages;
    [SerializeField] private TextMeshProUGUI buttonText;

    private ContentManager contentManager;
    private BonnetjeController bonnetjeController;

    public void LoadWithContent(ActivitySelection activitySelection)
    {
        if (contentManager == null) { contentManager = ContentManager.instance; }
        if (bonnetjeController == null) { bonnetjeController = BonnetjeController.instance; }

        loadedActivitySelection = activitySelection;

        title.text = loadedActivitySelection.name;

        if(loadedActivitySelection.name == "Aanwezig Zijn in het Gebouw")
        {
            //Disable remove button and gray out
            removeButton.interactable = false;
            foreach (Image image in outlineImages)
            {
                image.color = new Color(0.4f, 0.4f, 0.4f, 1f);
            }
            buttonText.color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }
    }

    public void PressRemove()
    {
        contentManager.selectedActivities.RemoveAll(obj => obj.name == loadedActivitySelection.name && obj.connectedBundleName == loadedActivitySelection.connectedBundleName && obj.selectedOptionName == loadedActivitySelection.selectedOptionName);
        bonnetjeController.UpdateContent();
    }
}
