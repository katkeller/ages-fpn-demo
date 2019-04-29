using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Assigning a key here will lock the door. If the player has teh key in their inventory, they can open the door.")]
    [SerializeField]
    private InventoryObject key;

    [Tooltip("If this is checked, the associated key will be removed from player's inventory when the door is unlocked.")]
    [SerializeField]
    private bool consumesKey;

    [Tooltip("Text that is displayed when the player looks at a locked door.")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [Tooltip("This plays when the player interacts with a locked door without having a key")]
    [SerializeField]
    private AudioClip lockedAudioClip;

    [Tooltip("This plays when the player opens the door")]
    [SerializeField]
    private AudioClip openAudioClip;

    //public override string DisplayText => isLocked ? lockedDisplayText : base.DisplayText;

    public override string DisplayText
    {
        get
        {
            string toReturn;

            if (isLocked)
                toReturn = HasKey ? $"Use {key.ObjectName}" : lockedDisplayText;
            else
                toReturn = base.DisplayText;

            return toReturn;
        }
    }

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

    /// <summary>
    /// Event handler for the PhoneRecorder script's DoorAnimationShouldPlay event. Plays animation and sound for door opening, as well as sets it's isOpen bool to true.
    /// </summary>
    /// <param name="doorToAnimate"></param>
    private void OnDoorAnimationShouldPlay(Door doorToAnimate)
    {
        if (doorToAnimate == this)
        {
            audioSource.clip = openAudioClip;
            animator.SetBool(shouldOpenAnimParameter, true);
            displayText = string.Empty;
            isOpen = true;
            UnlockDoor();
            base.InteractWith();
        }
    }

    private void OnEnable()
    {
        PhoneRecorder.DoorAnimationShouldPlay += OnDoorAnimationShouldPlay;
    }

    private void OnDisable()
    {
        PhoneRecorder.DoorAnimationShouldPlay -= OnDoorAnimationShouldPlay;
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
                UnlockDoor();
            }
            base.InteractWith(); // Plays sound effect from the audio source
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        if (key != null && consumesKey)
            PlayerInventory.InventoryObjects.Remove(key);
    }
}
