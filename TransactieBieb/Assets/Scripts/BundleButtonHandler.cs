using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleButtonHandler : MonoBehaviour
{
    public Bundle connectedBundle;
    [SerializeField] private ParticleSystem particles;
    private bool isAlreadySelected;

    private ContentManager contentManager;
    private BundleMenuController bundleMenuController;

    public void Init()
    {
        particles.gameObject.SetActive(false);
        particles.Stop();

        if (contentManager == null) { contentManager = ContentManager.instance; }
        if (bundleMenuController == null) { bundleMenuController = BundleMenuController.instance; }

        isAlreadySelected = false;

        foreach (ActivitySelection selectedActivity in contentManager.selectedActivities)
        {
            if (selectedActivity.connectedBundleName == connectedBundle.name) { isAlreadySelected = true; break; }
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
            //Open Bundle Add menu -> edit mode
            bundleMenuController.gameObject.SetActive(true);
            bundleMenuController.Init(connectedBundle, true);
        }
        else
        {
            //Open bundle Add menu -> Add mode
            bundleMenuController.gameObject.SetActive(true);
            bundleMenuController.Init(connectedBundle, false);
        }
    }
}
