using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponUpgrade : Interactable
{
    public override void DoInteract(InputAction.CallbackContext cb)
    {
        UIManager.Instance.OpenWeaponUpgrade();
    }
}
