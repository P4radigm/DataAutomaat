using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CategoryButtonHandler : MonoBehaviour
{
    public int categoryId;
    private ContentManager contentManager;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject backgroundShape;
    [SerializeField] private ParticleSystem particles;

    public void Pressed()
    {
        if(contentManager == null) { contentManager = ContentManager.instance; }

        if(contentManager.selectedCatId != categoryId) { contentManager.LoadCategory(categoryId); }
    }

    public void Selected()
    {
        backgroundShape.SetActive(true);
        particles.Play();
        titleText.color = new Color(0, 0, 0, 1);
    }

    public void UnSelected()
    {
        backgroundShape.SetActive(false);
        particles.Stop();
        titleText.color = new Color(0, 0, 0, 0.5f);
    }
}
