using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvertEffector : MonoBehaviour
{
    private bool inCollider;
    private bool shouldExit;
    void Start()
    {
        inCollider = false;
        shouldExit = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
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

    private void Update()
    {
        Vector2 dir = PlayerController.Instance.GetCurrentSpeed().normalized;

        if(dir.y < -0.5)
        {
            GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        } else
        {
            if (!inCollider)
            {
                GetComponent<PlatformEffector2D>().rotationalOffset = 0;
            }
            else
            {
                shouldExit = true;
            }
        }

    }
}
