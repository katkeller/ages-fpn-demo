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

    [SerializeField]
    private float animationDelay = 2.0f;

    public string DisplayText => displayText;
    private AudioSource audioSource;
    private int shouldOpenAnimParameter = Animator.StringToHash("shouldOpen");
    private AudioClip clipToPlayNext;
    private Animator otherObjectAnimator;
    private AudioSource otherObjectAudioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        clipToPlayNext = clip1;
        otherObjectAnimator = otherObjectToAnimate.GetComponent<Animator>();
        otherObjectAudioSource = otherObjectToAnimate.GetComponent<AudioSource>();
    }

    public void InteractWith()
    {
        try
        {
            audioSource.PlayOneShot(buttonClip, 1.0f);
            audioSource.PlayOneShot(clipToPlayNext, 1.0f);
            StartCoroutine(PlayAnimation());
            //firstClipAnimation.Play();
        }
        catch (System.Exception)
        {
            throw new System.Exception("Missing AudioSource componant or audio clip. InteractiveObject requires an AudioSource componant and an audio clip.");
        }
        Debug.Log($"Player just interacted with {gameObject.name}!");
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(animationDelay);
        otherObjectAnimator.SetBool(shouldOpenAnimParameter, true);
        otherObjectAudioSource.Play();
    }
}
