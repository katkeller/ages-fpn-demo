using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    private static InventoryMenu instance;

    public static InventoryMenu Instance
    {
        get
        {
            if (instance = null)
                throw new System.Exception("There is currently no InventoryMenu instance, but you are trying to access it.");
            return instance;
        }

        private set { instance = value; }
    }

    private void Awake()
    {
        if (instance = null)
            instance = this;
        else
            throw new System.Exception("There is already an instance of InventoryMenu, and there can only be one.");
    }
}
