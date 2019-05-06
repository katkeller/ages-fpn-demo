using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightPickUp : InventoryObject
{
    [SerializeField]
    private Text tutorialText;

    [SerializeField]
    private string tutorialTextContent;

    [SerializeField]
    private Light spotLight, pointLight;

    [SerializeField]
    private float tutorialTextDelay;

    [SerializeField]
    private GameObject fPFlashlight;

    protected override void Start()
    {
        base.Start();
        tutorialText.text = "";
        fPFlashlight.SetActive(false);
    }

    public override void InteractWith()
    {
        base.InteractWith();
        fPFlashlight.SetActive(true);
        spotLight.intensity = 0.0f;
        pointLight.intensity = 0.0f;
        tutorialText.text = tutorialTextContent;
        StartCoroutine(WaitForTutorialText());
    }

    IEnumerator WaitForTutorialText()
    {
        yield return new WaitForSeconds(tutorialTextDelay);
        tutorialText.text = "";
    }
}
