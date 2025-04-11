using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldAnimationController : MonoBehaviour
{
    public float walkSpeed = 4; // Speed when walking
    public float runSpeed = 7; // Speed when running
    public float transitionThreshold = 6.5f; // Velocity at which the character starts running
    public Animator animator; // Reference to the Animator component
    private Rigidbody rb;
    public float CharVelocity;
    private Vector3 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator not assigned!");
        }
    }

    void Update()
    {
        // Get movement input from WASD/Arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementInput = new Vector3(horizontal, 0, vertical).normalized;

        // Update animation states based on velocity magnitude
        float velocity = rb.linearVelocity.magnitude;
        CharVelocity = rb.linearVelocity.magnitude;
        if (velocity > 0.1f && velocity <= transitionThreshold)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if (velocity > transitionThreshold)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        //if we press the mouse button, then try to attack attack
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Attack");

        WeaponHitboxFunction();
    }
    public void SetWeaponType(string WeaponType)
    {
        //WE LOVE HARD CODING THINGS
        animator.SetBool("Sword", false);
        animator.SetBool("2H Weapon", false);
        animator.SetBool(WeaponType, true);
    }

    public string holdStateName = "Hold";
    public string swingStateName = "Swing";
    public int attackLayerIndex = 1; // Index of the Attack layer
    public GameObject WeaponHitbox; // GameObject to enable/disable

    void WeaponHitboxFunction()
    {
        if (animator != null && WeaponHitbox != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(attackLayerIndex);

            if (stateInfo.IsName(holdStateName))
            {
                WeaponHitbox.SetActive(false); // Disable when in Hold state
            }
            else if (stateInfo.IsName(swingStateName))
            {
                WeaponHitbox.SetActive(true); // Enable when in Swing state
            }
        }
    }

}