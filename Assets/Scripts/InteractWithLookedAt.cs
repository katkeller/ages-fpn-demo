using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Detects when the player presses the interact button while looking at an IInteractive.
/// Calls that IInteractive's InteractWith method.
/// </summary>
public class InteractWithLookedAt : MonoBehaviour
{
    private IInteractive lookedAtInteractive;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && lookedAtInteractive != null)
        {
            Debug.Log("Player pressed the interact button!");
            lookedAtInteractive.InteractWith();
        }
    }

    /// <summary>
    /// Event handler for DetectLookedAtInteractive.LookedAtInteractiveChanged
    /// </summary>
    /// <param name="newLookedAtInteractive">Reference to the new IInteractive the player is looking at.</param>
    private void OnLookedAtInteractiveChanged(IInteractive newLookedAtInteractive)
    {
        lookedAtInteractive = newLookedAtInteractive;
    }

    #region Event subscription / unsubscription
    private void OnEnable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged += OnLookedAtInteractiveChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtInteractiveChanged;
    }
    #endregion
}
