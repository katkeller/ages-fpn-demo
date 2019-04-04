using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRecorder : MonoBehaviour, IInteractive
{
    [SerializeField]
    protected string displayText = nameof(InteractiveObject);

    [SerializeField]
    private AudioClip buttonClip, clip1, clip2, clip3;

    private AudioClip clipToPlayNext;

    public string DisplayText => displayText;
    private AudioSource audioSource;
    private int timesInteractedWith = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        clipToPlayNext = clip1;
    }

    public void InteractWith()
    {
        try
        {
            audioSource.PlayOneShot(buttonClip, 1.0f);
            audioSource.PlayOneShot(clipToPlayNext, 1.0f);
        }
        catch (System.Exception)
        {
            throw new System.Exception("Missing AudioSource componant or audio clip. InteractiveObject requires an AudioSource componant and an audio clip.");
        }
        Debug.Log($"Player just interacted with {gameObject.name}!");
    }
}
