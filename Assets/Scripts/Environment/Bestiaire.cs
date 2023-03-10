using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bestiaire : Interactable
{

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        base.DoInteract(cb);
       UIManager.Instance.OpenBestiaire();
    }
}
