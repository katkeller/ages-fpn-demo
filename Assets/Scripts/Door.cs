using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Check this box to lock the door.")]
    [SerializeField]
    private bool isLocked = false;

    [Tooltip("Text that is displayed when the player looks at a locked door.")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [Tooltip("This plays when the player interacts with a locked door without having a key")]
    [SerializeField]
    private AudioClip lockedAudioClip;

    [Tooltip("This plays when the player opens the door")]
    [SerializeField]
    private AudioClip openAudioClip;

    public override string DisplayText => isLocked ? lockedDisplayText : base.DisplayText;

    private Animator animator;
    private bool isOpen = false;
    private int shouldOpenAnimParameter = Animator.StringToHash("shouldOpen");
    
    /// <summary>
    /// Using a constructor to initialize display text in the editor.
    /// </summary>
    public Door()
    {
        displayText = nameof(Door);
    }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void InteractWith()
    {
        if (!isOpen)
        {
            if (!isLocked)
            {
                audioSource.clip = openAudioClip;
                animator.SetBool(shouldOpenAnimParameter, true);
                displayText = string.Empty;
                isOpen = true;
            }
            else //if the door is locked
            {
                audioSource.clip = lockedAudioClip;
            }
            base.InteractWith(); // Plays sound effect from the audio source
        }
    }
}
