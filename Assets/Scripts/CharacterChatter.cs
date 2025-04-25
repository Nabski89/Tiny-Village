using UnityEngine;

public class CharacterChatter : MonoBehaviour
{

    public GameObject SoundEffect;
    public string WhatDoTheySay; // Public string to set what they say
    public BulkManager BulkManager; // Reference to the DialogComponent
    private void Start()
    {
        BulkManager = GetComponentInParent<BulkManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has a Rigidbody and the MovementController script
        if (other.attachedRigidbody != null && other.attachedRigidbody.GetComponent<MovementController>() != null)
        {
            // Ensure the DialogComponent is assigned
            if (BulkManager.MainTextReference != null)
            {
                BulkManager.MainTextReference.SoundEffect = SoundEffect;
                // Set the TextString property
                BulkManager.MainTextReference.TEXTBOX = WhatDoTheySay;

                // Call the NewText method
                BulkManager.MainTextReference.NewChatter();
            }
            else
            {
                Debug.LogWarning("DialogComponent reference is not assigned!");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Check if the other object has a Rigidbody and the MovementController script
        if (other.attachedRigidbody != null && other.attachedRigidbody.GetComponent<MovementController>() != null)
        {
            // Ensure the DialogComponent is assigned
            if (BulkManager.MainTextReference != null)
            {
                BulkManager.MainTextReference.EndChatter();
            }
            else
            {
                Debug.LogWarning("DialogComponent reference is not assigned!");
            }
        }
    }
}