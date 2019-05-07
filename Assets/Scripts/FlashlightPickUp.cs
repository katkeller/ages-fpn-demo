using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightPickUp : MonoBehaviour
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

    private void Start()
    {
        tutorialText.text = "";
        fPFlashlight.SetActive(false);
    }

    private void OnFlashlightHasBeenPickedUp()
    {
        fPFlashlight.SetActive(true);
        spotLight.intensity = 0.0f;
        pointLight.intensity = 0.0f;
        tutorialText.text = tutorialTextContent;
        StartCoroutine(WaitForTutorialText());
    }

    private void OnEnable()
    {
        InventoryObject.FlashlightHasBeenPickedUp += OnFlashlightHasBeenPickedUp;
    }

    private void OnDisable()
    {
        InventoryObject.FlashlightHasBeenPickedUp -= OnFlashlightHasBeenPickedUp;
    }

    IEnumerator WaitForTutorialText()
    {
        yield return new WaitForSeconds(tutorialTextDelay);
        tutorialText.text = "";
    }
}
