using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using UnityEngine.UI;
using TMPro;

public class SinglePageItemController : MonoBehaviour
{
    public bool selected = false;
    public string bonTitle;
    public string[] bonContent;
    public Sprite icon;

    [Header("Own References")]
    [SerializeField] private Rectangle displayOutline;
    [SerializeField] private Image displayIcon;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private ParticleSystem displayParticles;
    [SerializeField] private float selectedAlpha = 1;
    [SerializeField] private float deselectedAlpha = 0.6f;

    private void Start()
    {
        Set(false);
    }

    public void Toggle()
    {
        if (selected)
        {
            selected = false;
            displayOutline.Dashed = true;
            displayOutline.Color = new Color(displayOutline.Color.r, displayOutline.Color.g, displayOutline.Color.b, deselectedAlpha);
            displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, deselectedAlpha);
            displayIcon.color = new Color(displayIcon.color.r, displayIcon.color.g, displayIcon.color.b, deselectedAlpha);
            displayParticles.Stop();
            displayParticles.Clear();
        }
        else
        {
            selected = true;
            displayOutline.Dashed = false;
            displayOutline.Color = new Color(displayOutline.Color.r, displayOutline.Color.g, displayOutline.Color.b, selectedAlpha);
            displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, selectedAlpha);
            displayIcon.color = new Color(displayIcon.color.r, displayIcon.color.g, displayIcon.color.b, selectedAlpha);
            displayParticles.Play();
        }
    }

    public void Set(bool setSelected)
    {
        if (setSelected)
        {
            selected = true;
            displayOutline.Dashed = false;
            displayOutline.Color = new Color(displayOutline.Color.r, displayOutline.Color.g, displayOutline.Color.b, selectedAlpha);
            displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, selectedAlpha);
            displayIcon.color = new Color(displayIcon.color.r, displayIcon.color.g, displayIcon.color.b, selectedAlpha);
            displayParticles.Play();
        }
        else
        {
            selected = false;
            displayOutline.Dashed = true;
            displayOutline.Color = new Color(displayOutline.Color.r, displayOutline.Color.g, displayOutline.Color.b, deselectedAlpha);
            displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, deselectedAlpha);
            displayIcon.color = new Color(displayIcon.color.r, displayIcon.color.g, displayIcon.color.b, deselectedAlpha);
            displayParticles.Stop();
            displayParticles.Clear();
        }
    }
}
