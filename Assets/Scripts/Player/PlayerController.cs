using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.U2D;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private InputActionAsset actionAsset;
    public float speed = 1000;
    [SerializeField] private float jumpSpeed = 50;

    public float JumpSpeed { get { return jumpSpeed; } }

    public event System.Action OnJumpCB;
    public event System.Action OnDashCB;

    private void OnEnable()
    {
        OnEnablePlayer();
    }
    private void OnDisable()
    {
        OnDisablePlayer();
    }

    public void OnDisablePlayer()
    {
        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        actionMap.Disable();

        // Handle move
        InputAction move = actionMap.FindAction("Move");
        move.Disable();

        // Handle jump
        InputAction jump = actionMap.FindAction("Jump");
        jump.Disable();
        jump.started -= OnJump;

        // Handle Dash
        InputAction dash = actionMap.FindAction("Dash");
        dash.Disable();
        dash.started -= OnDash;
    }

    public void OnEnablePlayer() {
        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();

        // Handle move
        InputAction move = actionMap.FindAction("Move");
        move.Enable();

        // Handle jump
        InputAction jump = actionMap.FindAction("Jump");
        jump.Enable();
        jump.started += OnJump;

        // Handle dash
        InputAction dash = actionMap.FindAction("Dash");
        dash.Enable();
        dash.performed += OnDash;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (OnJumpCB != null)
        {
            OnJumpCB();
        }
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is MultiTapInteraction)
        {
            if (OnDashCB != null)
            {
                OnDashCB();
            }
        }
    }

    public Vector2 GetCurrentSpeed()
    {
        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        InputAction move = actionMap.FindAction("Move");
        return speed * move.ReadValue<Vector2>();
    }

    public int GetDirection()
    {
        if(transform.localScale.x < 0)
        {
            return -1;
        } else
        {
            return 1;
        }
    }
}
