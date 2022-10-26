using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkBench : Interactable
{

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        UIManager.Instance.OpenWorkBench();
    }
}
