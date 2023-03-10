using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

class GoToLevel : Interactable
{
    [SerializeField] private string sceneName;

    public void SetNextLevel(string levelName) { sceneName= levelName; }

    public override void DoInteract(InputAction.CallbackContext cb)
    {
        base.DoInteract(cb);
        GameManager.Instance.TransitionToLevel(sceneName);
    }
}
