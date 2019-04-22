using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuItemToggle : MonoBehaviour
{
    [Tooltip("The image componant used to show the associated inventory object's icon.")]
    [SerializeField]
    private Image iconImage;

    private InventoryObject associatedInventoryObject;

    public InventoryObject AssociatedInventoryObject
    {
        get { return associatedInventoryObject; }
        set
        {
            associatedInventoryObject = value;
            iconImage.sprite = associatedInventoryObject.Icon;
        }
    }

    /// <summary>
    /// This will be plugged into the toggle's OnValueChanged property in the editor and called whenever the toggle is selected/active.
    /// </summary>
    public void InventoryMenuItemWasToggled(bool isOn)
    {
        if (isOn)
        {

        }

        Debug.Log($"Toggled: {isOn}");
    }

    private void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
        ToggleGroup toggleGroup = GetComponentInParent<ToggleGroup>();
        toggle.group = toggleGroup;
    }
}
