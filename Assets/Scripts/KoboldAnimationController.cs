using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldAnimationController : MonoBehaviour
{
    public float walkSpeed = 2f; // Speed when walking
    public float runSpeed = 6f; // Speed when running
    public float transitionThreshold = 4f; // Velocity at which the character starts running
    public Animator animator; // Reference to the Animator component
    private Rigidbody rb;

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
    }
    public void SetWeaponType(string WeaponType)
    {
        //WE LOVE HARD CODING THINGS
        animator.SetBool("Sword", false);
        animator.SetBool("2H Weapon", false);
        animator.SetBool(WeaponType, true);
    }
}