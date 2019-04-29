using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryMenuItemTogglePrefab;

    [Tooltip("The content of the scrollview for the list of inventory items.")]
    [SerializeField]
    private Transform inventoryListContentArea;

    [Tooltip("The place in the UI for displaying the name of the selected inventory item.")]
    [SerializeField]
    private Text itemLabelText;

    [Tooltip("The place in the UI for displaying info about the selected inventory item.")]
    [SerializeField]
    private Text descriptionAreaText;

    [Tooltip("The button that serves as both an icon of the item selected, and a button for bringing up a larger version of the icon.")]
    [SerializeField]
    private Button itemImageButton;

    [SerializeField]
    private Image itemEnlargedImage, greyOutImage;

    [SerializeField]
    private Button hideEnlargedImageButton;

    [SerializeField]
    private Button exitButton;

    private static InventoryMenu instance;
    private CanvasGroup canvasGroup;
    private RigidbodyFirstPersonController rigidbodyFirstPersonController;
    private AudioSource audioSource;

    public static InventoryMenu Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("There is currently no InventoryMenu instance, but you are trying to access it.");
            return instance;
        }

        private set { instance = value; }
    }

    private bool IsVisible => canvasGroup.alpha > 0;

    public void ExitMenuButtonClicked()
    {
        HideMenu();
    }

    public void HideEnlargedImageButtonClicked()
    {
        exitButton.gameObject.SetActive(true);
        itemEnlargedImage.gameObject.SetActive(false);
        hideEnlargedImageButton.gameObject.SetActive(false);
        greyOutImage.gameObject.SetActive(false);
    }

    public void ItemImageButtonClicked()
    {
        exitButton.gameObject.SetActive(false);
        itemEnlargedImage.gameObject.SetActive(true);
        hideEnlargedImageButton.gameObject.SetActive(true);
        greyOutImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// Instantiates a new InventoryItemToggle prefab and adds it to the menu.
    /// </summary>
    /// <param name="inventoryObjectToAdd"></param>
    public void AddItemToMenu(InventoryObject inventoryObjectToAdd)
    {
        GameObject clone = Instantiate(inventoryMenuItemTogglePrefab, inventoryListContentArea);
        InventoryMenuItemToggle toggle = clone.GetComponent<InventoryMenuItemToggle>();
        toggle.AssociatedInventoryObject = inventoryObjectToAdd;

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("There is already an instance of InventoryMenu, and there can only be one.");
        }

        canvasGroup = GetComponent<CanvasGroup>();
        rigidbodyFirstPersonController = FindObjectOfType<RigidbodyFirstPersonController>();
        audioSource = GetComponent<AudioSource>();
        itemEnlargedImage.gameObject.SetActive(false);
        hideEnlargedImageButton.gameObject.SetActive(false);
        greyOutImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        HideMenu();
        StartCoroutine(WaitForAudioClip());
    }

    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// The event handler for InventoryMenuItemSelected
    /// </summary>
    private void OnInventoryMenuItemSelected(InventoryObject inventoryObjectThatWasSelected)
    {
        itemLabelText.text = inventoryObjectThatWasSelected.ObjectName;
        descriptionAreaText.text = inventoryObjectThatWasSelected.Decription;
        itemImageButton.image.sprite = inventoryObjectThatWasSelected.Icon;
        itemEnlargedImage.sprite = inventoryObjectThatWasSelected.LargeImage;
    }

    private void OnEnable()
    {
        InventoryMenuItemToggle.InventoryMenuItemSelected += OnInventoryMenuItemSelected;
    }

    private void OnDisable()
    {
        InventoryMenuItemToggle.InventoryMenuItemSelected -= OnInventoryMenuItemSelected;
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("InventoryMenu"))
        {
            if (IsVisible)
                HideMenu();
            else
                ShowMenu();
        }
    }

    private void ShowMenu()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        rigidbodyFirstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource.Play();
    }

    private void HideMenu()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigidbodyFirstPersonController.enabled = true;
        audioSource.Play();
    }

    private IEnumerator WaitForAudioClip()
    {
        float originalVolume = audioSource.volume;
        audioSource.volume = 0;
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.volume = originalVolume;
    }
}
