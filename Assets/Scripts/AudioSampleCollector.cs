using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSampleCollector : MonoBehaviour
{
    private AudioSource audioSource;
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];
    private float[] samples = new float[512];
    private float[] frequencyBand = new float[8];
    private float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];
    private float[] frequencyBandHighest = new float[8];
    private float highDecreaseSpeed = 1.2f;
    private float lowDecreaseSpeed = 0.005f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        FrequencyBandBuffer();
        CreateAudioBands();
    }

    private void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (frequencyBand[i] > frequencyBandHighest[i])
            {
                frequencyBandHighest[i] = frequencyBand[i];
            }

            audioBand[i] = (frequencyBand[i] / frequencyBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / frequencyBandHighest[i]);
        }
    }

    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void FrequencyBandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (frequencyBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = frequencyBand[g];
                bufferDecrease[g] = lowDecreaseSpeed;
            }
            else if (frequencyBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= highDecreaseSpeed;
            }
        }
    }

    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            frequencyBand[i] = average * 10;
        }
    }
}