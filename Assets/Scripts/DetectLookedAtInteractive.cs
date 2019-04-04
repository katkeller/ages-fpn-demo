using System;
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

    /// <summary>
    /// Event raised when the player looks at a differnt IInteractive.
    /// </summary>
    public static event Action<IInteractive> LookedAtInteractiveChanged;

    public IInteractive LookedAtInteractive
    {
        get { return lookedAtInteractive; }
        private set
        {
            bool isInteractiveChanged = value != lookedAtInteractive;
            if (isInteractiveChanged)
            {
                lookedAtInteractive = value;
                LookedAtInteractiveChanged?.Invoke(lookedAtInteractive);
            }
        }
    }

    private IInteractive lookedAtInteractive;

    private void FixedUpdate()
    {
        LookedAtInteractive = GetLookedAtInteractive();
    }

    /// <summary>
    /// Raycasts forward from the camera to look for IInteractives
    /// </summary>
    /// <returns>The first IInteractive detected, or null if none are found. </returns>
    private IInteractive GetLookedAtInteractive()
    {
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * maxRange, Color.cyan);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, maxRange);

        IInteractive interactive = null;

        LookedAtInteractive = interactive;

        if (objectWasDetected)
        {
            //Debug.Log($"Player is looking at: {hitInfo.collider.gameObject.name}");
            interactive = hitInfo.collider.gameObject.GetComponent<IInteractive>();
        }

        return interactive;
    }
}
