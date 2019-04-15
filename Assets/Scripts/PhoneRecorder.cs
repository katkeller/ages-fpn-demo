using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRecorder : MonoBehaviour, IInteractive
{
    [SerializeField]
    protected string displayText = nameof(InteractiveObject);

    [SerializeField]
    private AudioClip buttonClip, clip1, clip2, clip3;

    [SerializeField]
    private GameObject otherObjectToAnimate;

    private Animator otherObjectAnimator;

    //[Tooltip("The animation that will play during the simularly numbered clip after the delay time has elapsed.")]
    //[SerializeField]
    //private Animation firstClipAnimation, secondClipAnimation, thirdClipAnimation;

    private AudioClip clipToPlayNext;

    public string DisplayText => displayText;
    private AudioSource audioSource;
    //private int timesInteractedWith = 0;
    private int shouldOpenAnimParameter = Animator.StringToHash("shouldOpen");

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        clipToPlayNext = clip1;
        otherObjectAnimator = otherObjectToAnimate.GetComponent<Animator>();
    }

    public void InteractWith()
    {
        try
        {
            audioSource.PlayOneShot(buttonClip, 1.0f);
            audioSource.PlayOneShot(clipToPlayNext, 1.0f);
            otherObjectAnimator.SetBool(shouldOpenAnimParameter, true);
            //firstClipAnimation.Play();
        }
        catch (System.Exception)
        {
            throw new System.Exception("Missing AudioSource componant or audio clip. InteractiveObject requires an AudioSource componant and an audio clip.");
        }
        Debug.Log($"Player just interacted with {gameObject.name}!");
    }
}
