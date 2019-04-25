using System;
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
    private Door doorToAnimate1, doorToAnimate2, doorToAnimate3;

    [SerializeField]
    private float clip1AnimationDelay = 2.0f, clip2AnimationDelay = 2.0f, clip2SecondAnimationDelay = 0.0f;

    [SerializeField]
    private Keypad keypad;

    [SerializeField]
    private string phoneNumber1, phoneNumber2, phoneNumber3;

    public static event Action<Door> DoorAnimationShouldPlay;

    public string DisplayText => displayText;
    private AudioSource audioSource;
    private int shouldOpenAnimParameter = Animator.StringToHash("shouldOpen");
    private AudioClip clipToPlay;
    private float clipToPlayAnimationDelay= 0.0f;
    private Animator otherObjectAnimator;
    private AudioSource otherObjectAudioSource;
    private Door currentDoorToAnimate;
   

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        otherObjectAnimator = otherObjectToAnimate.GetComponent<Animator>();
        otherObjectAudioSource = otherObjectToAnimate.GetComponent<AudioSource>();
    }

    private void Start()
    {
        //Placeholder initialization for code that chooses which clip to play based on number dialed
        //clipToPlay = clip1;
        //currentDoorToAnimate = doorToAnimate1;
    }

    public void InteractWith()
    {
        keypad.ShowMenu();

        //PlayClip();
        Debug.Log($"Player just interacted with {gameObject.name}!");
    }

    private void OnInputEntered()
    {
        if (keypad.Input == phoneNumber1)
        {
            clipToPlay = clip1;
            currentDoorToAnimate = doorToAnimate1;
            clipToPlayAnimationDelay = clip1AnimationDelay;

        }
        else if(keypad.Input == phoneNumber2)
        {
            clipToPlay = clip2;
            currentDoorToAnimate = doorToAnimate2;
        }

        PlayClip();
    }

    private void OnEnable()
    {
        Keypad.InputEntered += OnInputEntered;
    }

    private void OnDisable()
    {
        Keypad.InputEntered -= OnInputEntered;
    }

    private void PlayClip()
    {
        try
        {
            StartCoroutine(PlayAudio());
        }
        catch (System.Exception)
        {
            throw new System.Exception("Missing AudioSource componant or audio clip. InteractiveObject requires an AudioSource componant and an audio clip.");
        }
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(clipToPlayAnimationDelay);
        //otherObjectAnimator.SetBool(shouldOpenAnimParameter, true);
        //otherObjectAudioSource.Play();
        DoorAnimationShouldPlay?.Invoke(currentDoorToAnimate);

        //if(clip2SecondAnimationDelay != 0)
        //{
        //    yield return new WaitForSeconds(clip2SecondAnimationDelay);

        //}
    }

    IEnumerator PlayAudio()
    {
        float buttonClipLength = buttonClip.length;

        audioSource.PlayOneShot(buttonClip, 1.0f);
        yield return new WaitForSeconds(buttonClipLength);
        audioSource.PlayOneShot(clipToPlay, 1.0f);
    }
}
