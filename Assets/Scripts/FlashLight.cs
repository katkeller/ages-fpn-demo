using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private GameObject spotLight;

    private AudioSource audioSource;
    private bool isOn;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Flashlight") && !isOn)
        {
            spotLight.SetActive(true);
            isOn = true;
            audioSource.Play();
        }
        else if (Input.GetButtonDown("Flashlight") && isOn)
        {
            spotLight.SetActive(false);
            isOn = false;
            audioSource.Play();
        }
    }
}
