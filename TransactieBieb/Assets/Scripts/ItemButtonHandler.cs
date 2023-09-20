using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[System.Serializable]
public class ItemButtonHandler : MonoBehaviour
{
    public ContentManager.Activities connectedActivity;
    [SerializeField] private ParticleSystem particles;
    private bool isAlreadySelected;

    private ContentManager contentManager;
    private AddMenuController addMenu;
    private RemoveMenuController removeMenu;
    private BonnetjeController bonnetjeController;

    public void Init()
    {
        particles.Stop();
        particles.gameObject.SetActive(false);

        if (contentManager == null) { contentManager = ContentManager.instance; }
        if (bonnetjeController == null) { bonnetjeController = BonnetjeController.instance; }
        if (addMenu == null) { addMenu = AddMenuController.instance; }
        if (removeMenu == null) { removeMenu = RemoveMenuController.instance; }

        isAlreadySelected = false;

        foreach(ActivitySelection selectedActivity in contentManager.selectedActivities)
        {
            if(selectedActivity.name == contentManager.referenceActivities[(int)connectedActivity].name) { isAlreadySelected = true; break; }
        }

        if (isAlreadySelected)
        {
            particles.gameObject.SetActive(true);
            particles.Play();
        }
    }

    public void Pressed()
    {
        if (isAlreadySelected)
        {
            if (contentManager.referenceActivities[(int)connectedActivity].options.Length != 0)
            {
                //Open option selection menu
                addMenu.gameObject.SetActive(true);
                addMenu.Init(contentManager.referenceActivities[(int)connectedActivity]);
            }
            else
            {
                //Open remove menu
                removeMenu.gameObject.SetActive(true);
                removeMenu.Init(contentManager.referenceActivities[(int)connectedActivity]);
            }
        }
        else
        {
            if (contentManager.referenceActivities[(int)connectedActivity].options.Length != 0)
            {
                //Open option selection menu
                addMenu.gameObject.SetActive(true);
                addMenu.Init(contentManager.referenceActivities[(int)connectedActivity]);
            }
            else
            {
                ActivitySelection selected = new();
                selected.name = contentManager.referenceActivities[(int)connectedActivity].name;
                selected.visual = contentManager.referenceActivities[(int)connectedActivity].visual;
                selected.receiptLines = contentManager.referenceActivities[(int)connectedActivity].stockReceiptLines;
                selected.selectedOptionName = "No";
                selected.connectedBundleName = "No";
                contentManager.selectedActivities.Add(selected);

                bonnetjeController.UpdateContent();
            }
        }

        Init();
    }
}
