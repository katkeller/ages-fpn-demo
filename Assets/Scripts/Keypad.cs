using System;
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

    [SerializeField]
    private Text clearButtonText;

    private string input;
    private string currentInput;
    private bool isEntered;
    private bool clipShouldPlay;
    private AudioSource audioSource;
    private CanvasGroup canvasGroup;
    private RigidbodyFirstPersonController rigidbodyFirstPersonController;

    public static event Action InputEntered;
    public static event Action KeypadClosed;

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

    private void Update()
    {
        if (currentInput == "")
        {
            clearButtonText.text = "Exit";
        }
        else
        {
            clearButtonText.text = "Clear";
        }
    }

    public void AddNumberToInput(string number)
    {
        currentInput = currentInput + number;
        inputText.text = currentInput;
        audioSource.Play();
    }

    public void EnterInput()
    {
        input = currentInput;
        audioSource.Play();
        InputEntered?.Invoke();
        currentInput = "";
        inputText.text = currentInput;
        HideMenu();
    }

    public void ClearInput()
    {
        if (currentInput == "")
        {
            HideMenu();
            KeypadClosed?.Invoke();
        }
        else
        {
            currentInput = "";
            inputText.text = currentInput;
            audioSource.Play();
        }
    }

    public void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        rigidbodyFirstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource.Play();
    }

    public void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
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
