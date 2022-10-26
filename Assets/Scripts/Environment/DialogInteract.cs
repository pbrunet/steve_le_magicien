using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogInteract : Interactable
{

    public void Start()
    {
    }

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        GetComponent<Speech>().enabled = true;
    }
}
