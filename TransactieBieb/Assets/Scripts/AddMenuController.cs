using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using iText.Commons.Actions.Contexts;

public class AddMenuController : MonoBehaviour
{
    static public AddMenuController instance;

    public Activity loadedActivity;
    public OptionButtonHandler[] optionSelectionButtons;
    [SerializeField] private TextMeshProUGUI activityTitle;
    [SerializeField] private Image activityVisual;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI doneButtonText;
    [HideInInspector] public bool hasInteracted = false;

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
        hasInteracted = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!hasInteracted) { doneButtonText.text = "TERUG"; }
        else
        {
            bool hasOption = false;
            for (int i = 0; i < loadedActivity.options.Length; i++)
            {
                if (optionSelectionButtons[i].isSelected) { hasOption = true; break; }
            }

            doneButtonText.text = hasOption ? "KLAAR" : "VERWIJDER";
        }
    }

    public void Init(Activity ActivityToDisplay)
    {
        hasInteracted = false;

        loadedActivity = ActivityToDisplay;

        if (ActivityToDisplay.options.Length == 2)
        {
            optionSelectionButtons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-150, -175);
            optionSelectionButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -175);
            optionSelectionButtons[2].gameObject.SetActive(false);

            optionSelectionButtons[0].Init(ActivityToDisplay.options[0]);
            optionSelectionButtons[1].Init(ActivityToDisplay.options[1]);
        }
        else if (ActivityToDisplay.options.Length == 3)
        {
            optionSelectionButtons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -175);
            optionSelectionButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -175);
            optionSelectionButtons[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(300, -175);

            optionSelectionButtons[0].Init(ActivityToDisplay.options[0]);
            optionSelectionButtons[1].Init(ActivityToDisplay.options[1]);
            optionSelectionButtons[2].Init(ActivityToDisplay.options[2]);
        }

        activityVisual.sprite = ActivityToDisplay.visual;
        activityTitle.text = ActivityToDisplay.name;

        if (ActivityToDisplay.multiplePossible) { questionText.text = "Selecteer je optie(s)"; }
        else { questionText.text = "Selecteer je optie"; }

    }

    public void PressedDoneButton()
    {
       contentManager.selectedActivities.RemoveAll(obj => obj.name == loadedActivity.name);
        for (int i = 0; i < loadedActivity.options.Length; i++)
        {
            if (optionSelectionButtons[i].isSelected)
            {
                ActivitySelection selected = new();
                selected.name = loadedActivity.name;
                selected.visual = loadedActivity.visual;
                selected.receiptLines = loadedActivity.options[i].receiptLines;
                selected.selectedOptionName = loadedActivity.options[i].name;
                selected.connectedBundleName = "No";
                contentManager.selectedActivities.Add(selected);
            }
        }

        bonnetjeController.UpdateContent();

        contentManager.LoadCategory(contentManager.selectedCatId);

        this.gameObject.SetActive(false);
    }
}
