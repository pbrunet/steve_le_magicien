using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TV : Interactable
{

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        UIManager.Instance.OpenSelfImprovement();
    }
}
