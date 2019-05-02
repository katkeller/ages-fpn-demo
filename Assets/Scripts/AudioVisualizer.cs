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

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    private void Update()
    {
        UpdateLightIntensity();
        //UpdateMaterial();
    }

    //private void UpdateMaterial()
    //{
    //    if (useBuffer)
    //    {
    //        Color color = new Color(AudioSampleCollector.audioBandBuffer[frequencyBandToSample], AudioSampleCollector.audioBandBuffer[frequencyBandToSample], AudioSampleCollector.audioBandBuffer[frequencyBandToSample]);
    //        material.SetColor("_EmissionColor", color);
    //    }
    //    else if (!useBuffer)
    //    {
    //        Color color = new Color(AudioSampleCollector.audioBand[frequencyBandToSample], AudioSampleCollector.audioBand[frequencyBandToSample], AudioSampleCollector.audioBand[frequencyBandToSample]);
    //        material.SetColor("_EmissionColor", color);
    //    }
    //}

    private void UpdateLightIntensity()
    {
        lightSource.intensity = (AudioSampleCollector.audioBandBuffer[frequencyBandToSample] * (maxLightIntensity - minLightIntensity)) + minLightIntensity;
    }
}
