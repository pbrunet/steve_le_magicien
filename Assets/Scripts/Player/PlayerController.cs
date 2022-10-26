using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    public float speed = 1000;
    [SerializeField] private float jumpSpeed = 50;

    public float JumpSpeed { get { return jumpSpeed; } }

    public event System.Action<float> OnMoveCB;
    public event System.Action OnJumpCB;

    // Start is called before the first frame update
    void Start()
    {

        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();

        // Handle move
        InputAction move = actionMap.FindAction("Move");
        move.Enable();
        move.performed += ctx =>
        {
            if (OnMoveCB != null)
            {
                OnMoveCB(speed * ctx.ReadValue<float>());
                Debug.Log("Move is " + speed * ctx.ReadValue<float>());
            }
        };
        move.canceled += ctx =>
        {
            if (OnMoveCB != null)
            {
                OnMoveCB(0);
                Debug.Log("Move is " + 0);
            }
        };

        // Handle jump
        InputAction jump = actionMap.FindAction("Jump");
        jump.Enable();
        jump.started += ctx =>
        {
            if (OnJumpCB != null)
            {
                OnJumpCB();
            }
        };
    }

    public float GetCurrentSpeed()
    {
        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        InputAction move = actionMap.FindAction("Move");
        return speed * move.ReadValue<float>();
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
