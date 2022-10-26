using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvertEffector : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;

    private bool inCollider;
    private bool shouldExit;
    void Start()
    {


        InputActionMap actionMap = actionAsset.FindActionMap("Player");
        actionMap.Enable();

        InputAction goDown = actionMap.FindAction("Attack");
        goDown.started += GoDown_started;
        goDown.canceled += GoDown_canceled;

        inCollider = false;
        shouldExit = false;
    }

    private void GoDown_canceled(InputAction.CallbackContext obj)
    {
        if (!inCollider)
        {
            GetComponent<PlatformEffector2D>().rotationalOffset = 0;
        } else
        {
            shouldExit = true;
        }
    }

    private void GoDown_started(InputAction.CallbackContext obj)
    {
        GetComponent<PlatformEffector2D>().rotationalOffset = 180;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inCollider = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        inCollider = false;
        if(shouldExit)
        {
            GetComponent<PlatformEffector2D>().rotationalOffset = 0;
            shouldExit = false;
        }
    }
}
