using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Assigning a key here will lock the door. If the player has teh key in their inventory, they can open the door.")]
    [SerializeField]
    private InventoryObject key;

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

    private bool HasKey => PlayerInventory.InventoryObjects.Contains(key);
    private Animator animator;
    private bool isOpen = false;
    private bool isLocked = false;
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
        InitializeIsLocked();
    }

    private void InitializeIsLocked()
    {
        if (key != null)
            isLocked = true;
    }

    public override void InteractWith()
    {
        if (!isOpen)
        {
            if (isLocked && !HasKey)
            {
                audioSource.clip = lockedAudioClip;
            }
            else //if it's not locked or it is locked and the player has the key
            {
                audioSource.clip = openAudioClip;
                animator.SetBool(shouldOpenAnimParameter, true);
                displayText = string.Empty;
                isOpen = true;
            }
            base.InteractWith(); // Plays sound effect from the audio source
        }
    }
}
