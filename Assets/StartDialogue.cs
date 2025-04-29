using UnityEngine;
using DialogueGraph.Runtime;

public class StartDialogue : MonoBehaviour, IInteractable
{
    public RuntimeDialogueGraph DialogueSystem;
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

        // isInConversation = true;
     //   Debug.LogWarning("Dialog on start testing This is from the StartDialogue. GetCurrentActor" + DialogueSystem.GetCurrentActor());

        BulkManager = GetComponentInParent<BulkManager>();
        if (BulkManager == null)
        {
            Debug.LogWarning("Help you didn't put this under the big boy bulk manager");
        }
//        Debug.LogWarning("Dialog on start testing. GetCurrentActor" + DialogueSystem.GetCurrentActor());
    }

    public BulkManager BulkManager; // Reference to the DialogComponent
    public GameObject SoundEffect;

    void StartDialog()
    {
        BulkManager.MainTextReference.SoundEffect = SoundEffect;
        // Set the TextString property
        if (DialogueSystem != null)
        {
            BulkManager.MainTextReference.DialogueSystem = DialogueSystem;
            BulkManager.MainTextReference.StartDialogue();
        }
        else
            Debug.LogWarning("DialogueSystemIsFuckedReference and not assigned");
    }


    private void OnTriggerExit(Collider other)
    {
        // Check if the other object has a Rigidbody and the MovementController script
        if (other.attachedRigidbody != null && other.attachedRigidbody.GetComponent<MovementController>() != null)
        {
            // Ensure the DialogComponent is assigned
            if (BulkManager.MainTextReference != null)
            {
                BulkManager.MainTextReference.EndDialogue();
            }
            else
            {
                Debug.LogWarning("DialogComponent reference is not assigned!");
            }
        }
    }
}
