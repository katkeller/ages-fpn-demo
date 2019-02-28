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

    private void FixedUpdate()
    {
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * maxRange, Color.cyan);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, maxRange);

        if (objectWasDetected)
        {
            Debug.Log($"Player is looking at: {hitInfo.collider.gameObject.name}");
        }
    }
}
