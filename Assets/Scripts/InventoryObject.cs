﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryObject : InteractiveObject
{
    [Tooltip("The name of the object as it will appear in the inventory UI")]
    [SerializeField]
    private string objectName = nameof(InventoryObject);

    [Tooltip("The text that will display when the player selects this object in the inventory menu.")]
    [TextArea(3, 6)]
    [SerializeField]
    private string description;

    [Tooltip("Icon to display for this item in the inventory menu.")]
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private Sprite largeImage;

    public Sprite Icon => icon;
    public string Decription => description;
    public string ObjectName => objectName;
    public Sprite LargeImage => largeImage;

    private new Renderer renderer;
    private new Collider collider;

    public static event Action FlashlightHasBeenPickedUp;
    public static event Action FirstNoteHasBeenPickedUp;

    protected virtual void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    public InventoryObject()
    {
        displayText = $"Take {objectName}";
    }

    /// <summary>
    /// When the player interacts with an inventory object, we must:
    /// 1. Add inventory object to PlayerInventory list
    /// 2. Remove the object from the game world and makle sure the player cannot interact with it while still being able to access it in inventory
    /// </summary>
    public override void InteractWith()
    {
        if (objectName == "Flashlight")
            FlashlightHasBeenPickedUp?.Invoke();
        else if (objectName == "Ed's Note")
            FirstNoteHasBeenPickedUp?.Invoke();

        base.InteractWith();
        PlayerInventory.InventoryObjects.Add(this);
        InventoryMenu.Instance.AddItemToMenu(this);
        collider.enabled = false;
        renderer.enabled = false;
    }
}
