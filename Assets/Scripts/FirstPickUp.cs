using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPickUp : MonoBehaviour
{
    [SerializeField]
    private Text tutorialText;

    [SerializeField]
    private string tutorialTextContent;

    [SerializeField]
    private float tutorialTextDelay;

    private void OnFirstNoteHasBeenPickedUp()
    {
        tutorialText.text = tutorialTextContent;
        StartCoroutine(WaitForTutorialText());
    }

    private void OnEnable()
    {
        InventoryObject.FirstNoteHasBeenPickedUp += OnFirstNoteHasBeenPickedUp;
    }

    private void OnDisable()
    {
        InventoryObject.FirstNoteHasBeenPickedUp -= OnFirstNoteHasBeenPickedUp;
    }

    IEnumerator WaitForTutorialText()
    {
        yield return new WaitForSeconds(tutorialTextDelay);
        tutorialText.text = "";
    }
}
