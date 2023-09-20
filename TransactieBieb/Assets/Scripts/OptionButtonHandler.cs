using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using iText.Commons.Actions.Contexts;
using static UnityEngine.ParticleSystem;

public class OptionButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionTitle;
    [SerializeField] private Image optionVisual;
    public ParticleSystem particles;
    public Option loadedOption;
    public bool isSelected;

    private AddMenuController addMenuController;
    private ContentManager contentManager;

    public void Init(Option OptionToLoad)
    {
        addMenuController = AddMenuController.instance;
        contentManager = ContentManager.instance;

        particles.Stop();
        particles.gameObject.SetActive(false);

        optionTitle.text = OptionToLoad.name;
        optionVisual.sprite = OptionToLoad.visual;

        isSelected = false;

        //Check whether option is active or not in bonnetje
        foreach (ActivitySelection activity in contentManager.selectedActivities)
        {
            if (activity.selectedOptionName == loadedOption.name && activity.name == addMenuController.loadedActivity.name) { isSelected = true; Debug.Log($"Found option {activity.selectedOptionName} loaded"); }         
        }

        if (isSelected) { particles.gameObject.SetActive(true); particles.Play(); }

        loadedOption = OptionToLoad;
    }

    public void Pressed()
    {
        if (!addMenuController.loadedActivity.multiplePossible)
        {
            for (int i = 0; i < addMenuController.optionSelectionButtons.Length; i++)
            {
                addMenuController.optionSelectionButtons[i].isSelected = false;
                addMenuController.optionSelectionButtons[i].particles.Stop();
                addMenuController.optionSelectionButtons[i].particles.gameObject.SetActive(false);
            }
        }

        isSelected = !isSelected;

        if (isSelected) { particles.gameObject.SetActive(true); particles.Play(); }
        else { particles.Stop(); particles.gameObject.SetActive(false); }

        addMenuController.hasInteracted = true;
    }
}
