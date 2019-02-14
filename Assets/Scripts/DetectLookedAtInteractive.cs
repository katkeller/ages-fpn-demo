using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Detects interactiv elements the player is looking at
/// </summary>

public class DetectLookedAtInteractive : MonoBehaviour
{
    [Tooltip ("Starting point of raycast used to detect interactive elements.")]
    [SerializeField]
    private Transform raycastOrigin;

    [Tooltip("Max distance from player/origin the raycast will search for interactive elements.")]
    [SerializeField]
    private float maxRange = 5.0f;

    private Vector3 raycastDirection;

    private void FixedUpdate()
    {
        Physics.Raycast(raycastOrigin.position, raycastDirection, maxRange);
    }
}
