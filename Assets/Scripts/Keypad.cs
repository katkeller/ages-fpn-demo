﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Keypad : MonoBehaviour
{
    [SerializeField]
    private string phoneNumber1, phoneNumber2, phoneNumber3;

    [SerializeField]
    private Text inputText;

    private string input;
    private string currentInput;
    private bool isEntered;
    private bool clipShouldPlay;
    private AudioSource audioSource;
    private CanvasGroup canvasGroup;
    private RigidbodyFirstPersonController rigidbodyFirstPersonController;

    public static event Action InputEntered;

    private bool IsVisible => canvasGroup.alpha > 0;

    public string Input => input;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rigidbodyFirstPersonController = FindObjectOfType<RigidbodyFirstPersonController>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        ClearInput();
        HideMenu();
        StartCoroutine(WaitForAudioClip());
    }

    public void AddZeroToInput(string number)
    {
        currentInput = currentInput + number;
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddOneToInput()
    {
        currentInput = currentInput + "1";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddTwoToInput()
    {
        currentInput = currentInput + "2";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddThreeToInput()
    {
        currentInput = currentInput + "3";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddFourToInput()
    {
        currentInput = currentInput + "4";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddFiveToInput()
    {
        currentInput = currentInput + "5";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddSixToInput()
    {
        currentInput = currentInput + "6";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddSevenToInput()
    {
        currentInput = currentInput + "7";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddEightToInput()
    {
        currentInput = currentInput + "8";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void AddNineToInput()
    {
        currentInput = currentInput + "9";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void EnterInput()
    {
        input = currentInput;
        audioSource.Play();
        InputEntered?.Invoke();
        HideMenu();
    }

    public void ClearInput()
    {
        currentInput = "";
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        rigidbodyFirstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource.Play();
    }

    public void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigidbodyFirstPersonController.enabled = true;
        audioSource.Play();
    }

    private IEnumerator WaitForAudioClip()
    {
        float originalVolume = audioSource.volume;
        audioSource.volume = 0;
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.volume = originalVolume;
    }
}
