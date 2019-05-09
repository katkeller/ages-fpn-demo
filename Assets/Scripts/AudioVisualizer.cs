using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [SerializeField]
    private float minLightIntensity = 0.0f, maxLightIntensity = 5.0f;

    [Tooltip("Enter a number between 0-7 to choose which of the 8 frequency bands will be sampled from the AudioSampleCollector script.")]
    [SerializeField]
    private int frequencyBandToSample;

    [SerializeField]
    private bool useBuffer;

    [SerializeField]
    private Light lightSource;

    //[SerializeField]
    //private Keypad keypad;

    //[SerializeField]
    //private string phoneNumber1, phoneNumber2;

    //private Light lightToUse;

    //private void OnInputEntered()
    //{
    //    if (keypad.Input == phoneNumber1)
    //    {
    //        lightToUse = lightSource1;
    //    }
    //    else if (keypad.Input == phoneNumber2)
    //    {
    //        lightToUse = lightSource2;
    //    }
    //}

    private void Update()
    {
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        lightSource.intensity = (AudioSampleCollector.audioBandBuffer[frequencyBandToSample] * (maxLightIntensity - minLightIntensity)) + minLightIntensity;
    }
}
