using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ClearInput();
    }

    public void AddZeroToInput()
    {
        currentInput = currentInput + "0";
        inputText.text = currentInput;
    }

    public void AddOneToInput()
    {
        currentInput = currentInput + "1";
        inputText.text = currentInput;
    }

    public void AddTwoToInput()
    {
        currentInput = currentInput + "2";
        inputText.text = currentInput;
    }

    public void AddThreeToInput()
    {
        currentInput = currentInput + "3";
        inputText.text = currentInput;
    }

    public void AddFourToInput()
    {
        currentInput = currentInput + "4";
        inputText.text = currentInput;
    }

    public void AddFiveToInput()
    {
        currentInput = currentInput + "5";
        inputText.text = currentInput;
    }

    public void AddSixToInput()
    {
        currentInput = currentInput + "6";
        inputText.text = currentInput;
    }

    public void AddSevenToInput()
    {
        currentInput = currentInput + "7";
        inputText.text = currentInput;
    }

    public void AddEightToInput()
    {
        currentInput = currentInput + "8";
        inputText.text = currentInput;
    }

    public void AddNineToInput()
    {
        currentInput = currentInput + "9";
        inputText.text = currentInput;
    }

    public void EnterInput()
    {
        input = currentInput;
    }

    public void ClearInput()
    {
        currentInput = "";
        inputText.text = currentInput;
    }
}
