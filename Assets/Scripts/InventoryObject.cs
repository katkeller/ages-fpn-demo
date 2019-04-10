using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : InteractiveObject
{
    // TODO: Add long description field
    // TODO: Add Icon field

    private new Renderer renderer;
    private new Collider collider;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    public InventoryObject()
    {
        displayText = nameof(InventoryObject);
    }

    /// <summary>
    /// When the player interacts with an inventory object, we must:
    /// 1. Add inventory object to PlayerInventory list
    /// 2. Remove the object from the game world and makle sure the player cannot interact with it while still being able to access it in inventory
    /// </summary>
    public override void InteractWith()
    {
        base.InteractWith();
        PlayerInventory.InventoryObjects.Add(this);
        collider.enabled = false;
        renderer.enabled = false;

    }
}
