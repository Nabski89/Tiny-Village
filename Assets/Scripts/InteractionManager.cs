using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    //    public TextMeshPro ContextButtonText;
    //   public GameObject ContextButton;
    public IInteractable InteractionTarget;
    public Transform CharacterInteracting;
    //Replace this this with context button stuff later
    public GameObject UIElements;

    public LayerMask interactableLayerMask; // Layer mask for filtering raycast targets
    public float rayDistance = 5f; // Distance of the raycast
    public BulkManager BulkManager;
    private void Start()
    {
        BulkManager = GetComponent<BulkManager>();
    }
    void Update()
    {
        // Perform a raycast forward from the GameObject
        RaycastHit hit;
        Debug.DrawRay(CharacterInteracting.position + CharacterInteracting.up / 2, CharacterInteracting.forward * rayDistance, Color.red);
        if (Physics.Raycast(CharacterInteracting.position + CharacterInteracting.up / 2, CharacterInteracting.forward, out hit, rayDistance, interactableLayerMask))
        {
            // Attempt to retrieve the IInteractable component on the hit object
            InteractionTarget = hit.collider.GetComponent<IInteractable>();

            if (InteractionTarget != null)
            {
                UIElements.SetActive(true);
                Debug.Log($"Interactable found: {hit.collider.gameObject.name}");
            }
        }
        else
        {
            // No interactable object in range
            UIElements.SetActive(false);
            InteractionTarget = null;
        }
        if (InteractionTarget != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && BulkManager.InDialogue == false)
            {
                InteractionTarget.Interact();
            }
        }
    }
}