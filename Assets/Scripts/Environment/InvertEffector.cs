using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvertEffector : MonoBehaviour
{
    public Collider2D toActiveOnEnter;

    void Start()
    {
    }

    private void Update()
    {
        if (PlayerController.Instance.GetCurrentSpeed().y < -0.5)
        {
            toActiveOnEnter.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFoot"))
        {
            toActiveOnEnter.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFoot"))
        {
            toActiveOnEnter.enabled = false;
        }
    }
}
