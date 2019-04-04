using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickUp : InteractiveObject
{
    [Tooltip("The GameObject Note to toggle")]
    [SerializeField]
    private GameObject noteToToggle;

    //This is a placeholder for the info about the next clip to be played from the recording device.
    [SerializeField]
    private string ClipToPlay;

    public override void InteractWith()
    {
        base.InteractWith();
        noteToToggle.SetActive(!noteToToggle.activeSelf);
    }
}
