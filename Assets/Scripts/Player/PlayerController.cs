using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput;

    TouchingDirections touchingDirections;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                return IsMoving ? walkSpeed : 0;
            }
            else
            {
                // movement lock
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool _isFacingRight = true;

    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.Rotate(0f, 180f, 0f);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool IsAlive
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }
    public bool IsStunned
    {
        get { return animator.GetBool(AnimationStrings.isStunned); }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        rb.gravityScale = 0; // Disable gravity for the player
    }

    private void FixedUpdate()
    {
        Vector2 newVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, 0);

        // Handle wall collision
        if (touchingDirections.isOnWall)
        {
            if (moveInput.x > 0 && !isFacingRight)
            {
                // Prevent moving right if hitting the wall on the right
                newVelocity.x = 0;
            }
            else if (moveInput.x < 0 && isFacingRight)
            {
                // Prevent moving left if hitting the wall on the left
                newVelocity.x = 0;
            }
        }

        rb.velocity = newVelocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive && !IsStunned)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            // Face the right
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            // Face the left
            isFacingRight = false;
        }
    }
}
