﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PhoneRecorder : MonoBehaviour, IInteractive
{
    [SerializeField]
    protected string displayText = nameof(InteractiveObject);

    [SerializeField]
    private AudioClip buttonClip, introClip, clip1, clip2, clip3;

    [SerializeField]
    private Door doorToAnimate1, doorToAnimate2, doorToAnimate3;

    [SerializeField]
    private float clip1AnimationDelay = 2.0f, clip2AnimationDelay = 2.0f, clip2SecondAnimationDelay = 0.0f;

    [SerializeField]
    private Keypad keypad;

    [SerializeField]
    private string phoneNumber1, phoneNumber2, phoneNumber3;

    [SerializeField]
    private GameObject ghostLight1, ghostLight2;

    [SerializeField]
    private Light light1, light2, light3, light4;

    public static event Action<Door> DoorAnimationShouldPlay;

    public string DisplayText => displayText;
    private AudioSource audioSource;
    private int shouldOpenAnimParameter = Animator.StringToHash("shouldOpen");
    private AudioClip clipToPlay;
    private float clipToPlayAnimationDelay= 0.0f;
    private Door currentDoorToAnimate;
    private bool isInteractible = true;
    private float light1StartingIntensity;
    private float light2StartingIntensity;
    private float light3StartingIntensity;
    private float forwardSpeedStartingValue;
    private float backwardSpeedStartingValue;
    private float strafeSpeedStartingValue;
    private RigidbodyFirstPersonController rigidbodyFirstPersonController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbodyFirstPersonController = FindObjectOfType<RigidbodyFirstPersonController>();
    }

    public void InteractWith()
    {
        if (isInteractible)
        {
            keypad.ShowMenu();
            audioSource.PlayOneShot(introClip, 1.0f);
            isInteractible = false;
            Debug.Log($"Player just interacted with {gameObject.name}!");
        }
    }

    private void OnInputEntered()
    {
        audioSource.Stop();

        if (keypad.Input == phoneNumber1)
        {
            clipToPlay = clip1;
            currentDoorToAnimate = doorToAnimate1;
            clipToPlayAnimationDelay = clip1AnimationDelay;
            ghostLight1.gameObject.SetActive(true);
            PlayClip();
        }
        else if(keypad.Input == phoneNumber2)
        {
            clipToPlay = clip2;
            currentDoorToAnimate = doorToAnimate2;
            clipToPlayAnimationDelay = clip2AnimationDelay;
            ghostLight2.gameObject.SetActive(true);
            PlayClip();
        }
        else
        {
            isInteractible = true;
        }
    }

    private void OnKeypadClosed()
    {
        isInteractible = true;
        audioSource.Stop();
    }

    private void OnEnable()
    {
        Keypad.InputEntered += OnInputEntered;
        Keypad.KeypadClosed += OnKeypadClosed;
    }

    private void OnDisable()
    {
        Keypad.InputEntered -= OnInputEntered;
        Keypad.KeypadClosed -= OnKeypadClosed;
    }

    private void TurnOffLights()
    {
        light1StartingIntensity = light1.intensity;
        light2StartingIntensity = light2.intensity;
        light3StartingIntensity = light3.intensity;

        light1.intensity = Mathf.Lerp(light1StartingIntensity, 0.0f, 2.0f);
        light2.intensity = Mathf.Lerp(light2StartingIntensity, 0.0f, 2.0f);
        light3.intensity = Mathf.Lerp(light3StartingIntensity, 0.0f, 2.0f);
    }

    private void TurnOnLights()
    {
        light1.intensity = Mathf.Lerp(0.0f, light1StartingIntensity, 4.0f);
        light2.intensity = Mathf.Lerp(0.0f, light2StartingIntensity, 4.0f);
        light3.intensity = Mathf.Lerp(0.0f, light3StartingIntensity, 4.0f);
    }

    private void PlayClip()
    {
        TurnOffLights();

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
        DoorAnimationShouldPlay?.Invoke(currentDoorToAnimate);
    }

    IEnumerator PlayAudio()
    {
        float buttonClipLength = buttonClip.length;

        audioSource.PlayOneShot(buttonClip, 1.0f);
        yield return new WaitForSeconds(buttonClipLength);
        audioSource.PlayOneShot(clipToPlay, 1.0f);
        yield return new WaitForSeconds(clipToPlay.length);
        isInteractible = true;

        if (ghostLight1.gameObject.activeInHierarchy == true)
            ghostLight1.gameObject.SetActive(false);
        else if (ghostLight2.gameObject.activeInHierarchy == true)
            ghostLight2.gameObject.SetActive(false);

        TurnOnLights();
    }
}
