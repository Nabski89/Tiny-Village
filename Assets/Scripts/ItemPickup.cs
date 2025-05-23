using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour, IInteractable
{
    //  public GameObject targetObject; // The GameObject that contains the bone
    public string boneName = "hand.R"; // The name of the bone to attach to
    public string WeaponNameForBool;
    public bool RightHanded;
    public bool LeftHanded;
    //this is probably hand.R or hand.L
    public EquipController CharacterToHold;
    BoxCollider boxCollider;
    GameObject WeaponHitbox;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        InteractableManager = GetComponentInParent<InteractionManager>();
        WeaponHitbox = GetComponentInChildren<WeaponHitbox>().transform.gameObject;
        WeaponHitbox.SetActive(false);
    }
    public InteractionManager InteractableManager { get; set; }
    public void ActiveInteractable()
    {
        InteractableManager.InteractionTarget = this;
    }
    public void Interact()
    {
        Equip();
    }
    void Equip()
    {
        // Get the bone Transform from the target GameObject
        Transform bone = InteractableManager.CharacterInteracting.GetComponent<EquipController>().RightHand;
        if (bone != null)
        {
            //Remove existing held items
            foreach (Transform child in bone)
            {
                // Try to get the ItemPickup component
                ItemPickup itemPickup = child.GetComponent<ItemPickup>();
                if (itemPickup != null)
                {
                    // Call the Drop() method
                    itemPickup.Drop();
                    Debug.Log($"Called Drop() on {child.name}.");
                }
            }

            //set the parent and scale
            transform.SetParent(bone); // The second parameter ensures local position/rotation/scale remains consistent
            transform.position = bone.position;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            Debug.Log($"{transform.name} is now attached to {boneName}.");
            //haha our scale is fucked up
            transform.localScale = Vector3.one / 100f;

            //set our boolean to known what type of weapon we are holding
            KoboldAnimationController KAC = GetComponentInParent<KoboldAnimationController>();
            KAC.SetWeaponType(WeaponNameForBool);
            KAC.WeaponHitbox = WeaponHitbox;
            //clean up for the next thing
            boxCollider.enabled = false;
            CharacterToHold = null;
            InteractableManager.InteractionTarget = null;

        }
    }
    public void Drop()
    {
        transform.SetParent(null);

        // Position the detached child 1 unit to the left or right
        float direction = RightHanded ? 1f : -1f; // 1 for right, -1 for left
        transform.position += new Vector3(direction * 2, 0, 0);
        transform.localScale = Vector3.one;
        boxCollider.enabled = true;

    }
}