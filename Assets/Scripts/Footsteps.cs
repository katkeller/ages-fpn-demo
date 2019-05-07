using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Footsteps : MonoBehaviour
{
    private CharacterController characterController;
    private RigidbodyFirstPersonController rigidbodyFirstPersonController;
    private AudioSource audioSource;

    void Start()
    {
        rigidbodyFirstPersonController = GetComponent<RigidbodyFirstPersonController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(rigidbodyFirstPersonController.Grounded == true && rigidbodyFirstPersonController.Velocity.magnitude > 2.0f && audioSource.isPlaying == false)
        {
            audioSource.volume = Random.Range(0.8f, 1.0f);
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
        }
    }
}
