using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RemoveMenuController : MonoBehaviour
{
    static public RemoveMenuController instance;

    public Activity loadedActivity;
    [SerializeField] private TextMeshProUGUI activityTitle;
    [SerializeField] private Image activityVisual;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button AddButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private Button MiddleBackButton;
    [TextArea][SerializeField] private string stockQuestionText;
    [TextArea][SerializeField] private string cameraText;

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
        gameObject.SetActive(false);
    }

    public void Init(Activity ActivityToDisplay)
    {
        loadedActivity = ActivityToDisplay;
        activityVisual.sprite = ActivityToDisplay.visual;
        activityTitle.text = ActivityToDisplay.name;
        questionText.text = stockQuestionText.Replace("[activity]", ActivityToDisplay.name);
        MiddleBackButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
        AddButton.gameObject.SetActive(true);

        if (ActivityToDisplay.name == "Aanwezig Zijn in het Gebouw")
        {
            questionText.text = cameraText;
            MiddleBackButton.gameObject.SetActive(true);
            BackButton.gameObject.SetActive(false);
            AddButton.gameObject.SetActive(false);
        }
    }

    public void PressBack()
    {
        bonnetjeController.UpdateContent();

        contentManager.LoadCategory(contentManager.selectedCatId);

        this.gameObject.SetActive(false);
    }

    public void PressRemove()
    {
        contentManager.selectedActivities.RemoveAll(obj => obj.name == loadedActivity.name);

        bonnetjeController.UpdateContent();

        contentManager.LoadCategory(contentManager.selectedCatId);

        this.gameObject.SetActive(false);
    }
}
