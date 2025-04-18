using UnityEngine;

public class StartDialogue : MonoBehaviour, IInteractable
{
    public InteractionManager InteractableManager { get; set; }
    public void ActiveInteractable()
    {
        InteractableManager.InteractionTarget = this;
    }
    public void Interact()
    {
        StartDialog();
    }


    void Start()
    {
        BulkManager = GetComponentInParent<BulkManager>();
        if (BulkManager == null)
        {
            Debug.LogWarning("Help you didn't put this under the big boy bulk manager");
        }
    }

    public BulkManager BulkManager; // Reference to the DialogComponent
    public string WhatDoTheySay; // Public string to set what they say
    public GameObject SoundEffect;

    void StartDialog()
    {
        BulkManager.MainTextReference.SoundEffect = SoundEffect;
        // Set the TextString property
        BulkManager.MainTextReference.TEXTBOX = WhatDoTheySay;
        BulkManager.MainTextReference.NewText();
    }


    private void OnTriggerExit(Collider other)
    {
        // Check if the other object has a Rigidbody and the MovementController script
        if (other.attachedRigidbody != null && other.attachedRigidbody.GetComponent<MovementController>() != null)
        {
            // Ensure the DialogComponent is assigned
            if (BulkManager.MainTextReference != null)
            {
                BulkManager.MainTextReference.EndText();
            }
            else
            {
                Debug.LogWarning("DialogComponent reference is not assigned!");
            }
        }
    }
}
